USE [FirstCommand]
GO
/****** Object:  StoredProcedure [dbo].[IsAgentServicingAgent]    Script Date: 02/22/2023 2:26:27 PM ******/

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('IsAgentServicingAgent_AWS'))
BEGIN
	DROP PROC [dbo].[IsAgentServicingAgent_AWS] 
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[IsAgentServicingAgent_AWS] 
  @PolicyNumber varchar(10) = NULL,
  @AgentNumber varchar(10) = NULL,
  @ServicingAgentInd varchar(1) OUTPUT,
  @ErrMessage varchar(255) = NULL OUTPUT
AS
  ----------------------------------------------------------------------------
  -- Send PolicyNumber or AgentNumber                                       --
  ----------------------------------------------------------------------------
  SET NOCOUNT ON
  DECLARE @ID_User int,
          @Message varchar(255),
          @AdminLevelInd varchar(1),
          @Error int,
          @ReturnedErrMessage varchar(255),
		  @WritingAgentNumber varchar(10)
  SELECT @ServicingAgentInd = 'N'
  SELECT @ErrMessage = NULL

  -- Get User Admin Security Level Indicator --
  
  IF LEN(RTRIM(ISNULL(@AgentNumber, ''))) = 0 --AND LEN(RTRIM(ISNULL(@PolicyNumber, ''))) = 0
  BEGIN
  SELECT @ReturnedErrMessage = 'Agent Number is required'
  GOTO RaiseErrorFromServerAndExit
  END

  SELECT @AdminLevelInd = CASE SecurityLevel
                             WHEN 255 THEN 'Y'
                             ELSE 'N'
                          END , @ID_User = U.ID_User
  FROM UserAgentNumbers UA
  INNER JOIN Users U ON U.ID_USER = UA.ID_User
  WHERE AgentNumber = @AgentNumber AND UA.Status = 'A'
  
  SELECT @Error = @@ERROR
  IF @Error <> 0 
  BEGIN
	SELECT @ReturnedErrMessage = 'Error while servicing agent validation'
	GOTO RaiseErrorFromServerAndExit
  END 
  -- If User has Admin Level...Set Servicing Agent Ind = Yes --
  IF @AdminLevelInd = 'Y'
  BEGIN
     SELECT @ServicingAgentInd = 'Y'
     RETURN 0
  END
   
  IF @ID_User IS NULL
  BEGIN
    SELECT @ErrMessage = 'User not found!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END

  IF LEN(RTRIM(ISNULL(@PolicyNumber, ''))) > 0
    BEGIN
      -- Get @AgentNumber from Basic Table --
      SELECT @WritingAgentNumber = Writing_Agent
        FROM Basic (NOLOCK)
       WHERE PolicyNumber = @PolicyNumber
    END

  IF @WritingAgentNumber = @AgentNumber
  BEGIN
    SELECT @ServicingAgentInd = 'Y'
  END

  RETURN 0
---------------------------------------------------------------------------
-- Raise Error and Exit
---------------------------------------------------------------------------
RaiseErrorFromServerAndExit:
    IF RTRIM(ISNULL(@ErrMessage, '')) = ''  Select @ErrMessage = @ReturnedErrMessage
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
GO
