USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('XREFAgentNumber_AWS'))
BEGIN
	DROP PROC [dbo].[XREFAgentNumber_AWS] 
END


/****** Object:  StoredProcedure [dbo].[XREFAgentNumber]    Script Date: 02/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE 	[dbo].[XREFAgentNumber_AWS]
  @AgentNumber varchar(10) OUTPUT,
  @ErrMessage varchar(255) = NULL OUTPUT
AS
  SET NOCOUNT ON

  DECLARE @Error int,
          @Message varchar(255),
          @ReturnedErrMessage varchar(255)

 SELECT @AgentNumber = l.mfAgtNum
    FROM UserAgentNumbers u
   JOIN dbo.LNXREF l ON l.mfAgtNum = u.AgentNumber
   WHERE l.fcpAgtNum = CASE WHEN LEN(@AgentNumber) < 5 OR (LEN(@AgentNumber)=5 AND LEFT(@AgentNumber,1) = '8' ) THEN '0' + RIGHT(@AgentNumber,4) ELSE @AgentNumber END;
 
 SELECT @Error = @@Error
 IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while fetching Xref Agent Number - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  -- SELECT @ErrMessage = 'Test Error'
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END

RETURN 0

GO
