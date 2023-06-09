USE [FirstCommand]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('GetLastGACycleDate_AWS'))
BEGIN
	DROP PROC [dbo].[GetLastGACycleDate_AWS] 
END

/****** Object:  StoredProcedure [dbo].[GetLastGACycleDate_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetLastGACycleDate_AWS] 
	@CycleDate datetime = null output
AS

DECLARE   @ErrMessage varchar(255),
          @Error int

-- J.Young - 06/24/2004 -- Per Jeff Alexander, changed the MAX(Allotment_Cycle_Date) to MIN(...).
SELECT @CycleDate = MIN(Allotment_Cycle_Date)
  FROM Basic (NOLOCK)
 WHERE Allotment_Cycle_Date is not null

  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while fetching Government Allotment Cycle date - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END 
RETURN 0

GO
