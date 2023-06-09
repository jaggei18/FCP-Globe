USE [FirstCommand]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_GovAllotment_AWS'))
BEGIN
	DROP PROC [dbo].[Select_GovAllotment_AWS] 
END


/****** Object:  StoredProcedure [dbo].[Select_GovAllotment]    Script Date: 04/04/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[Select_GovAllotment_AWS]
(@Stype int, @PolNo varchar(10), @SSN char(9), @Company char(1))
AS

DECLARE   @ErrMessage varchar(255),
          @Error int

if @Stype = 3
 SELECT PolicyNumber, Company, SSNO, Writing_Agent, Basic.Status, Status_Codes.Description AS StatusDesc, A.CompanyName 
 FROM Basic 
  INNER JOIN Status_Codes ON Basic.Status = Status_Codes.Status 
  LEFT OUTER JOIN FCP_Company A ON CompanyCode = Company
 where Bill_Type = 'GA' and  SSNO = @SSN ORDER BY Company, PolicyNumber

if @Stype = 4
 SELECT PolicyNumber, Bill_type, Basic.Status, Company, SSNO, Allotment_Amount, Allotment_Cycle_Date, Allotment_Payor, Status_Codes.Description AS StatusDesc, A.CompanyName, B.BillTypeDescription
 FROM Basic 
  INNER JOIN Status_Codes ON Basic.Status = Status_Codes.Status 
  LEFT OUTER JOIN FCP_Company A ON CompanyCode = Company
  LEFT OUTER JOIN FCP_BillType B ON BillCode = Bill_Type
 where Bill_Type = 'GA' and  PolicyNumber = @PolNo

if @Stype = 5
 SELECT Basic.Status, SSNO, Company, Bill_Type, Writing_Agent, PolicyNumber, PaidTo_Date, Modal_Premium, Status_Codes.Description AS StatusDesc, A.CompanyName, B.BillTypeDescription 
 FROM Basic 
  INNER JOIN Status_Codes ON Basic.Status = Status_Codes.Status 
  LEFT OUTER JOIN FCP_Company A ON CompanyCode = Company
  LEFT OUTER JOIN FCP_BillType B ON BillCode = Bill_Type
 where Bill_Type = 'GA' and  SSNO = @SSN and Company =  @Company

if @Stype = 6
 SELECT PolicyNumber, SSNO FROM Basic where Bill_Type = 'GA' and  PolicyNumber = @PolNo 
           
  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while fetching Government Allotment Info - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END 
RETURN 0

GO
