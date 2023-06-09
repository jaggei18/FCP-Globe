USE [FirstCommand]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_PendingRec_AWS'))
BEGIN
	DROP PROC [dbo].[Select_PendingRec_AWS] 
END


/****** Object:  StoredProcedure [dbo].[Select_PendingRec]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Select_PendingRec_AWS]
@PendAgentNo int
as

DECLARE @SQL varchar(8000),
          @WhereInd bit,
          @ErrMessage varchar(255),
          @Message varchar(255),
          @Today datetime,
          @SPParams varchar(8000),
          @ReturnedErrMessage varchar(255),
          @Error int


SELECT PolicyNumber, Insured, Last_Payment_Date,  Allotment_Payor, Writing_Agent 
FROM Basic 
WHERE (status = 'PP' or status = 'CC' or status = 'PO' or status = 'DD') and Writing_Agent = @PendAgentNo order by  Last_Payment_Date Asc

SELECT @Error = @@ERROR
IF @Error <> 0 GOTO ReturnErrorFromServerAndExit
   
Return 0
   
-------------------------------------------------------------------------
-- Rollback Tran Raise Error and Exit
---------------------------------------------------------------------------
ReturnErrorFromServerAndExit:
    SELECT @ErrMessage = 'Error while fetching Pending Record - ' + Text FROM dbo.Messages WHERE Message_ID = @Error
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
GO
