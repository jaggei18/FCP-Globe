USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('PendingAgtValid_AWS'))
BEGIN
	DROP PROC [dbo].[PendingAgtValid_AWS] 
END


/****** Object:  StoredProcedure [dbo].[PendingAgtValid]    Script Date: 02/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE 	[dbo].[PendingAgtValid_AWS]
  @AgentNumber varchar(10),
  @UserID int = NULL OUTPUT,
  @ErrMessage varchar(255) = NULL OUTPUT
AS
  SET NOCOUNT ON

  DECLARE @CELS_SSN varchar(9), 
          @CELS_DOB datetime,
          @CELS_BRSeqno int, 
          @Error int,
          @ReturnedErrMessage varchar(255)

  
SELECT @UserID = ID_User
    FROM UserAgentNumbers u
   JOIN dbo.LNXREF l ON l.fcpAgtNum = u.AgentNumber
   WHERE l.mfAgtNum = CASE WHEN LEN(@AgentNumber) < 5 OR (LEN(@AgentNumber)=5 AND LEFT(@AgentNumber,1) = '8' ) THEN '0' + RIGHT(@AgentNumber,4) ELSE @AgentNumber END;
--print @userid      

  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while validating pending Agent - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END 
  
  -- If @ID_User is Null...User not found in Users table..Add Dummy User record
  IF @UserID IS NULL
  BEGIN
    SELECT @UserID = basic_Seqno
          FROM CELS_BasicRecord (NOLOCK)
     WHERE basic_Agent_Number = CASE WHEN LEN(@AgentNumber) = 4 THEN '8'+ RIGHT(@AgentNumber,4) ELSE @AgentNumber END
  END

  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while validating pending Agent - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END 

RETURN 0         
		 
GO
