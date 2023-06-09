USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('ValidateAgentToCELS_AWS'))
BEGIN
	DROP PROC [dbo].[ValidateAgentToCELS_AWS] 
END

/****** Object:  StoredProcedure [dbo].[ValidateAgentToCELS_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[ValidateAgentToCELS_AWS] 
  @AgentNumber varchar(10) OUTPUT,
  @SSN varchar(9),
  @BirthDate datetime,
  @ErrMessage varchar(255) OUTPUT
AS
  SET NOCOUNT ON
  --Added on 1/15/21 by Jason Coffee, cross-reference any 5 digit agent number that doesn't start with 8
  IF LEN(@AgentNumber)=5 AND LEFT(@AgentNumber,1) <> '8' 
  BEGIN
    SELECT @AgentNumber = mfAgtNum FROM dbo.LNXREF WHERE fcpAgtNum = @AgentNumber;
  END

  DECLARE @CELS_BasicRecord_Seqno int,
          @CELS_SSN varchar(9),
          @CELS_BirthDate datetime    
    
  IF LEN(RTRIM(ISNULL(@SSN, ''))) = 0
  BEGIN
    SELECT @ErrMessage = 'Agent Validation Failed...Missing Agent Social Security Number!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
  IF LEN(RTRIM(ISNULL(@BirthDate, ''))) = 0
  BEGIN
    SELECT @ErrMessage = 'Agent Validation Failed...Missing Agent Birth Date!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
  SELECT @CELS_BasicRecord_Seqno = basic_Seqno,
         @CELS_SSN               = basic_SSN,
         @CELS_BirthDate         = basic_DOB 
    FROM CELS_BasicRecord
   WHERE basic_Agent_Number = @AgentNumber
  IF @@ROWCOUNT = 0
  BEGIN
    SELECT @ErrMessage = 'Agent Validation Failed...Agent Account Number not found!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
      
  -- CELS may not have valid SSN or Birth Date...validate CELS first --
  IF ISNULL(@CELS_SSN, '') IN ('', '000000000')
  BEGIN
    SELECT @ErrMessage = 'SSN in Agent Database is invalid...Please Contact Us!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
  IF ISDATE(@CELS_BirthDate) = 0
  BEGIN
    SELECT @ErrMessage = 'Birth Date in Agent Database is invalid...Please Contact Us!  '
                       + 'We will add your Birth Date to our Agent Database and notify you via E-mail '
                       + 'once you can Register successfully.'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
  -- Validate received SSN and BirthDate to CELS --
  IF @CELS_SSN <> @SSN
  BEGIN
    SELECT @ErrMessage = 'Agent Validation Failed...Agent Account Number/Social Security Number mismatch!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
    
  IF @CELS_BirthDate <> @BirthDate
  BEGIN
    SELECT @ErrMessage = 'Agent Validation Failed...Agent Account Number/Birth Date mismatch!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END
  RETURN 0         

GO
