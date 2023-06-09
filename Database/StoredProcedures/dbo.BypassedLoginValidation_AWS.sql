USE [FirstCommand]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('BypassedLoginValidation_AWS'))
BEGIN
	DROP PROC [dbo].[BypassedLoginValidation_AWS] 
END

/****** Object:  StoredProcedure [dbo].[BypassedLoginValidation_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[BypassedLoginValidation_AWS]
  @AgentNumber varchar(10),
  @AllAccessInd bit,
  @ID_User int = NULL OUTPUT,
  @LoginType varchar(1) = NULL OUTPUT,
  @ErrMessage varchar(255) = NULL OUTPUT
AS
  SET NOCOUNT ON
  SET XACT_ABORT ON
  DECLARE @DBPassword varchar(255),
          @UserType varchar(1),
          @SecurityLevel tinyint,
          @SPParams varchar(8000),
          @AdminLevelInd varchar(1),
          @CELS_SSN varchar(9), 
          @CELS_DOB datetime,
          @CELS_BRSeqno int, 
          @OrigAgentNumber varchar(10),
          @Error int,
          @ReturnedErrMessage varchar(255)
    
  SELECT @ErrMessage = NULL, @ReturnedErrMessage = NULL
  SELECT @LoginType = NULL
  SELECT @ID_User = NULL
             
SELECT @OrigAgentNumber = @AgentNumber

SELECT @AgentNumber = LTRIM(RTRIM(@AgentNumber))

IF LEFT(@AgentNumber, 1) <> '8' 
-- 1/11/21 - Jason Coffee -- added logic to use new cross=reference table for agent number
BEGIN
    SELECT @AgentNumber = mfAgtNum
    FROM dbo.LNXREF 
    WHERE fcpAgtNum = @AgentNumber;
END
   
--------------------------------------------------------------------------------------
-- If AllAccessInd = True...Set Agent Number to Admin Level Agent Number (Home Office)
--------------------------------------------------------------------------------------
  IF @AllAccessInd = 1 
  BEGIN
    SELECT @AgentNumber = '99999'
  END

---------------------------------------------------------------------------
-- Try to get user information from the database
---------------------------------------------------------------------------
  SELECT @ID_User = u.ID_User,
         @UserType = u.UserType,
         @LoginType = 'P', -- Primary Login
         @SecurityLevel = ISNULL(u.SecurityLevel, 0)
    FROM Users u (NOLOCK)
    LEFT JOIN UserAgentNumbers ua (NOLOCK)
      ON u.ID_User = ua.ID_User
   WHERE u.PrimaryAgentNumber = @AgentNumber  ---This may not be valid now...check on this???
      OR ua.AgentNumber = @AgentNumber
  
--print 'first check'  
--print @ID_User
  -- If @ID_User is Null...User not found in Users table..Add Dummy User record
  IF @ID_User IS NULL
  BEGIN
    SELECT @CELS_BRSeqno = basic_Seqno,
           @CELS_SSN = basic_SSN,
           @CELS_DOB = basic_DOB
      FROM CELS_BasicRecord (NOLOCK)
     WHERE basic_Agent_Number = @AgentNumber
--print 'second check'  
--print @ID_User
    IF @CELS_SSN IS NULL
    BEGIN
      SELECT @ErrMessage = 'Unable to access Torchmark OnLine...Agent is not in our Database!'
  
      -- Record Error in Log File and Exit --
      GOTO LogErrorAndExit
    END
    
    SELECT @ID_User = u.ID_User,
           @UserType = u.UserType,
           @LoginType = 'P', -- Primary Login
           @SecurityLevel = ISNULL(u.SecurityLevel, 0)
      FROM Users u (NOLOCK)
      JOIN UserAgentNumbers ua (NOLOCK)
        ON u.ID_User = ua.ID_User
     WHERE u.PrimaryUserID = @CELS_SSN
    
    IF @ID_User IS NULL -- Existing User record under different Agent Number not found
    BEGIN
      -- Add Dummy User Record
      EXEC Insert_Users_AWS
           @PrimaryUserID = @CELS_SSN,
           @UserType = 'A',
           @SecurityLevel = 0,
           @BirthDate = @CELS_DOB,
           @PrimaryAgentNumber = @AgentNumber,
           @UserName = '',
           @UpdatedBy = 'SYSTEM',
           @BypassedLoginInd = 1,
           @BypassedAgentNumber = @AgentNumber,
           @BypassedAllAccessInd = @AllAccessInd,
           @New_ID_User = @ID_User OUTPUT, 
           @ErrMessage = @ErrMessage OUTPUT
              
		--print 'zero'
		--print @ID_User
      SELECT @Error = @@Error
      IF @Error <> 0 GOTO ReturnErrorFromServerAndExit
       
      IF @ID_User IS NULL
      BEGIN
        SELECT @ErrMessage = 'Unable to access Torchmark OnLine...Undetermined error while Adding User!'
    
        -- Record Error in Log File and Exit --
        GOTO LogErrorAndExit
      END
    END
  ELSE -- Existing User record under different Agent Number found
    BEGIN
	--  print 'first ' 
	--  print  @ID_User
      GOTO StartUserSession
    END
  END      
 ELSE -- Existing User Record Found
  BEGIN
 -- print 'second' 
 -- print @ID_User
      GOTO StartUserSession
 END
--COMMIT TRAN
--print 'end'
RETURN 0
---------------------------------------------------------------------------
-- Start new User Session...Return SessionGUID to caller
---------------------------------------------------------------------------
StartUserSession:
          
  -- Rebuild User Agent Numbers
  EXEC Rebuild_UserAgentNumbers_By_ID_User_AWS @ID_User = @ID_User,
                                           @AgentNumber = @AgentNumber,
										   @ErrMessage = @ErrMessage OUTPUT
  SELECT @Error = @@Error
  IF @Error <> 0 GOTO ReturnErrorFromServerAndExit
          
  -- Get User Admin Level Indicator -- 
 -- EXEC IsUserAdminLevel_By_SecurityLevel_AWS @SecurityLevel = @SecurityLevel,
  --                                       @AdminLevelInd = @AdminLevelInd OUTPUT
 -- SELECT @Error = @@Error
 -- IF @Error <> 0 GOTO ReturnErrorFromServerAndExit
                 
  -- If Security Level <> 255 (Administrator)...Atleast 1 Agent must be 'Active'
 -- print 'last'
--  print @id_user
  IF @SecurityLevel <> 255 -- Administrator
  BEGIN
    IF (SELECT COUNT(*)
          FROM UserAgentNumbers (NOLOCK)
         WHERE ID_User = @ID_User
           AND Status = 'A') = 0
    BEGIN
      SELECT @ErrMessage = 'Unable to access Torchmark OnLine...Agent does not have an Active Account!'
      -- Record Error in Log File and Exit --
      GOTO LogErrorAndExit
    END
  END
--COMMIT TRAN
RETURN 0
---------------------------------------------------------------------------
-- ReturnError Raise Error and Exit
---------------------------------------------------------------------------
ReturnErrorFromServerAndExit:
  SELECT @ErrMessage = Text FROM sys.Messages WHERE Message_ID = @Error
  IF RTRIM(ISNULL(@ReturnedErrMessage, '')) <> ''  Select @ErrMessage =  @ReturnedErrMessage + ' - ' + @ErrMessage
  GOTO LogErrorAndExit
RETURN 99
  
---------------------------------------------------------------------------
-- Log Error and Exit...Rollback Transaction, then Log Error
---------------------------------------------------------------------------
LogErrorAndExit:
     
  -- Rollback Transaction
  IF @@TRANCOUNT = 1 ROLLBACK TRAN
  -- Record Error in Log File --
  RAISERROR(@ErrMessage, 16, 1)
  RETURN 1

GO

/*
  Declare @ID_USER int, @LoginType varchar(1), @ErrMessage varchar(255)
  Exec BypassedLoginValidation_AWS '82111', 1, @ID_USER OUTPUT, @LoginType OUTPUT, @ErrMessage OUTPUT
  SElect @ID_User, @LoginType, @ErrMessage

*/

/*
update [dbo].[CELS_BasicRecord]
set basic_Agency_Code ='82111',
basic_Agent_Number = '82111',
basic_SSN = '123456111',
basic_Status = 'A'
where basic_Seqno = 1 
*/

--Error while rebuilding User Agent Numbers By ID_User