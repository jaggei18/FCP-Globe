USE [FirstCommand]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Insert_Users_AWS'))
BEGIN
	DROP PROC [dbo].[Insert_Users_AWS] 
END

/****** Object:  StoredProcedure [dbo].[Insert_Users_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insert_Users_AWS]
  @PrimaryUserID varchar(20),
  @UserType varchar(1),
  @SecurityLevel tinyint,
  @BirthDate datetime,
  @PrimaryAgentNumber varchar(10),
  @UserName varchar(50),
  @UpdatedBy varchar(30),
  @BypassedLoginInd bit = 0,
  @BypassedAgentNumber varchar(10) = NULL,
  @BypassedAllAccessInd bit = 0,
  @New_ID_User int = NULL OUTPUT, 
  @ErrMessage varchar(255) = NULL OUTPUT
AS
  SET NOCOUNT ON
  SET XACT_ABORT ON
  DECLARE @Today datetime,
          @New_ID_UserSession int, 
          @SPParams varchar(8000),
          @Message varchar(255),
          @Error int, 
          @ReturnedErrMessage varchar(255),
          @UserMsg varchar(10),
          @AdminLevelInd varchar(1)

BEGIN TRAN
  
  SELECT @Today = getdate()
  SELECT @New_ID_User = NULL
  SELECT @ErrMessage = NULL
    
  -- Validate User Type -- 
  --   'A' - Agent    --
  --   'C' - Client   --
  IF @UserType NOT IN ('A', 'C')
  BEGIN
    IF @@TRANCOUNT = 1 ROLLBACK TRAN
    SELECT @ErrMessage = 'Error Inserting User...User Type must be "A" or "C"!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
 
  SELECT @UserMsg = CASE @UserType
                       WHEN 'A' THEN 'Agent' 
                       WHEN 'C' THEN 'Client'
                       ELSE 'Unknown'
                    END

    -- Validate that there are no User records matching the PrimaryUserID --
  IF EXISTS(SELECT TOP 1 ID_User
              FROM Users (NOLOCK)
             WHERE PrimaryUserID = @PrimaryUserID
               AND UserType = @UserType)
  BEGIN
    IF @@TRANCOUNT = 1 ROLLBACK TRAN
    SELECT @ErrMessage = 'Error Inserting User...' + @UserMsg + ' already exists!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
  -- Get User Admin Level Indicator --

  SELECT @AdminLevelInd = CASE SecurityLevel
                             WHEN 255 THEN 'Y'
                             ELSE 'N'
                          END 
  FROM UserAgentNumbers UA
  INNER JOIN Users U ON U.ID_USER = UA.ID_User
  WHERE PrimaryUserID = @PrimaryUserID AND UA.Status = 'A'
  
     
  -- If User Type = "Agent" and non "Admin Level" and non "Bypassed Login" ...validate against CELS --
  IF  @UserType = 'A' 
  AND @AdminLevelInd = 'N'
  AND @BypassedLoginInd = 0
  BEGIN
    -- Validate that Agent exists in CELS --
    EXEC ValidateAgentToCELS_AWS  @PrimaryAgentNumber OUTPUT,
                              @PrimaryUserID,
                              @BirthDate,
                              @ReturnedErrMessage OUTPUT
     
    SELECT @Error = @@Error
    IF @Error <> 0 GOTO ReturnErrorFromServerAndExit
  END
  
  -- Insert User --
  INSERT INTO Users
        (PrimaryUserID,
         UserType,
         SecurityLevel,
         BirthDate,
         PrimaryAgentNumber,
         UserName,
         AddedDate,
         AddedBy,
         UpdatedDate,
         UpdatedBy)
  VALUES(@PrimaryUserID,
         @UserType,         
         @SecurityLevel,
         @BirthDate,
         @PrimaryAgentNumber,
         @UserName,
         @Today,
         @UpdatedBy,
         @Today,
         @UpdatedBy)
 
  SELECT @New_ID_User = @@IDENTITY 

  IF NOT EXISTS (SELECT TOP 1 ID_User  
                   FROM Users (NOLOCK)
                  WHERE ID_User = @New_ID_User
                    AND PrimaryUserID = @PrimaryUserID
                    AND UserType = @UserType
                    AND AddedDate = @Today)
  BEGIN
    IF @@TRANCOUNT = 1 ROLLBACK TRAN
    SELECT @ErrMessage = 'Error Inserting ' + @UserMsg + '...Undetermined System Error!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
   
  -- If User Type = "Agent" and non "Admin Level"...Populate UsersAgentNumbers Table --
  IF  @UserType = 'A' 
  AND @AdminLevelInd = 'N'  
  BEGIN
    EXEC Rebuild_UserAgentNumbers_By_ID_User_AWS @ID_User = @New_ID_User,
                                             @AgentNumber = @PrimaryAgentNumber,
								             @ErrMessage = @ErrMessage OUTPUT
    SELECT @Error = @@Error
    IF @Error <> 0 GOTO ReturnErrorFromServerAndExit
     
    -- Verify that the Agent has atleast one Agent Number and it is 'Active' --
    IF (SELECT COUNT(ID_User)
          FROM UserAgentNumbers (NOLOCK)
         WHERE ID_User = @New_ID_User) = 0
    BEGIN
      IF @@TRANCOUNT = 1 ROLLBACK TRAN
      IF @BypassedLoginInd = 0
         SELECT @Message = 'You are unable to set up an Account because Agent is not in our Database!'      
      ELSE
         SELECT @Message = 'Unable to access Torchmark OnLine...Agent is not in our Database!'      
     
      RAISERROR(@Message, 16, 1)
      RETURN 1
    END
  END
   
  SELECT @Message = 'Inserted new Users Record'
    
COMMIT TRAN
RETURN 0
---------------------------------------------------------------------------
-- Rollback Tran Raise Error and Exit
---------------------------------------------------------------------------
ReturnErrorFromServerAndExit:
    IF @@TRANCOUNT = 1 ROLLBACK TRAN
    SELECT @ErrMessage = Text FROM sys.Messages WHERE Message_ID = @Error
    IF RTRIM(ISNULL(@ReturnedErrMessage, '')) <> ''  Select @ErrMessage = @ReturnedErrMessage + ' - ' + @ErrMessage
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1




GO

