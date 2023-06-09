USE [FirstCommand]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_Coverage_AWS'))
BEGIN
	DROP PROC [dbo].[Select_Coverage_AWS] 
END


/****** Object:  StoredProcedure [dbo].[Select_Coverage]    Script Date: 03/27/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Select_Coverage_AWS]
(@PolNo varchar(10))
 AS

 DECLARE   @ErrMessage varchar(255),
          @Error int

SELECT Coverage.*, Coverage_Codes.Description AS CovDesc FROM Coverage INNER JOIN Coverage_Codes ON Coverage.Type = Coverage_Codes.Type where PolicyNumber = @PolNo order by sequence



  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while fetching coverage info - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END
  
RETURN 0

GO


/*
Exec Select_Coverage_AWS 'A005605066'

*/