USE [FirstCommand]
GO
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_PolicyInf_AWS'))
BEGIN
	DROP PROC [dbo].[Select_PolicyInf_AWS] 
END

/****** Object:  StoredProcedure [dbo].[Select_PolicyInf_AWS]    Script Date: 03/27/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[Select_PolicyInf_AWS]
(@Stype int, 
@PolNo varchar(10)
)
AS
DECLARE   @ErrMessage varchar(255),
          @Error int

if @stype = 1
 SELECT Basic.*, Status_Codes.Description AS StatusDesc, A.CompanyName, B.BillTypeDescription, C.FrequencyDescription
 FROM Basic 
 INNER JOIN Status_Codes ON Basic.Status = Status_Codes.Status 
  LEFT OUTER JOIN FCP_Company A ON CompanyCode = Company
  LEFT OUTER JOIN FCP_BillType B ON BillCode = Bill_Type
  LEFT OUTER JOIN FCP_Frequency C ON FrequencyCode = Frequency

 WHERE PolicyNumber = @PolNo

  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while fetching Policy Info - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END
  
RETURN 0

GO
