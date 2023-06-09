USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Rebuild_UserAgentNumbers_By_ID_User_AWS'))
BEGIN
	DROP PROC [dbo].[Rebuild_UserAgentNumbers_By_ID_User_AWS] 
END

/****** Object:  StoredProcedure [dbo].[Rebuild_UserAgentNumbers_By_ID_User_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Rebuild_UserAgentNumbers_By_ID_User_AWS] 
  @ID_User int,
  @AgentNumber varchar(10), 
  @ErrMessage varchar(255) = NULL OUTPUT
AS
  SET NOCOUNT ON
  SET XACT_ABORT ON
  
  DECLARE @Today datetime,
          @SPParams varchar(8000),
          @ReturnedErrMessage varchar(255),
          @Error int,
		  @RowCount int
  
BEGIN TRAN
  SELECT @Today = getdate()
  SELECT @ErrMessage = NULL
      
	 -- print 'rebuild id user and agent number' 
	--  print @ID_User 
	--  print @AgentNumber

  DELETE FROM UserAgentNumbers
   WHERE ID_User = @ID_User

  SELECT @Error = @@Error
  IF @Error <> 0 GOTO ReturnErrorFromServerAndExit

  INSERT INTO UserAgentNumbers
        (ID_User,
         AgentNumber,
         Status,
         BasicRecord_Seqno,
         AddedDate)
  SELECT @ID_User,
         cb.basic_Agent_Number,
         cb.basic_Status,
         cb.basic_Seqno,
         @Today
    FROM CELS_BasicRecord cb (NOLOCK)
    JOIN Users u (NOLOCK)
      ON cb.basic_SSN = u.PrimaryUserID
   WHERE u.ID_User = @ID_User

  SELECT @Error = @@Error --, @RowCount = @@RowCount  (removed this condition as existing logic is not restricting if no records. eg. 99999 agent #)
  IF @Error <> 0 GOTO ReturnErrorFromServerAndExit

  UPDATE u
     SET u.UserName = Agent_Full_Name
    FROM Users u
    JOIN CELS_BasicRecord cb
      ON u.PrimaryUserID = cb.basic_SSN
   WHERE u.ID_User = @ID_User

   SELECT @Error = @@Error --, @RowCount = @@RowCount (removed this condition as existing logic is not restricting if no records. eg. 99999 agent #)
   IF @Error <> 0 GOTO ReturnErrorFromServerAndExit

COMMIT TRAN
RETURN 0
---------------------------------------------------------------------------
-- Rollback Tran Raise Error and Exit
---------------------------------------------------------------------------
ReturnErrorFromServerAndExit:
--PRINT 'Error in Rebuild_UserAgentNumbers_By_ID_User]'
    IF @@TRANCOUNT = 1 ROLLBACK TRAN
	Select @ReturnedErrMessage =  'Error while rebuilding User Agent Numbers By ID_User'
	IF @Error <> 0 Select @ReturnedErrMessage = @ReturnedErrMessage + ' - ' + Text from sys.messages where message_id = @Error
    Select @ErrMessage = @ReturnedErrMessage
    RAISERROR(@ErrMessage, 16, 1) 
    RETURN 1
GO
