USE [FirstCommand]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_BankDraft_AWS'))
BEGIN
	DROP PROC [dbo].[Select_BankDraft_AWS] 
END

/****** Object:  StoredProcedure [dbo].[Select_BankDraft_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Select_BankDraft_AWS]
(@Stype int,
@PolNo varchar(10), 
@Company char(1))
AS
Declare @ErrMessage Varchar(255), @error int

if @stype = 1
 SELECT PolicyNumber, Company, A.CompanyName
 FROM Basic 
 LEFT OUTER JOIN FCP_Company A ON CompanyCode = Company
 WHERE Bill_Type = 'BB' and  PolicyNumber Like '__@PolNo'

if @stype = 2
 SELECT PolicyNumber, Company, A.CompanyName, Status, Bill_Type, B.BillTypeDescription, Frequency, C.FrequencyDescription, Bank_Draft_Billing_PolicyNumber, Draft_Day, Draft_Payor, Draft_Bank, Writing_Agent 
 FROM Basic 
 LEFT OUTER JOIN FCP_Company A ON CompanyCode = Company
 LEFT OUTER JOIN FCP_BillType B ON BillCode = Bill_Type
 LEFT OUTER JOIN FCP_Frequency C ON FrequencyCode = Frequency
 WHERE Bill_Type = 'BB' and  PolicyNumber = @PolNo

if @stype = 3
 SELECT Status, Company, A.CompanyName, Bill_Type, B.BillTypeDescription, Writing_Agent, PolicyNumber, PaidTo_Date, Modal_Premium 
 FROM Basic 
 LEFT OUTER JOIN FCP_Company A ON CompanyCode = Company
 LEFT OUTER JOIN FCP_BillType B ON BillCode = Bill_Type
 WHERE Bill_Type = 'BB' and  Bank_Draft_Billing_PolicyNumber = @PolNo and Company = @Company

 select @error = @@error

 IF @Error <> 0 
	BEGIN
		Select @ErrMessage = 'Failed to fetch Bank Draft details' + ' - ' + Text from sys.messages where message_id = @Error
		RAISERROR(@ErrMessage, 16, 1) 
		RETURN 1
	END
Return 0

GO
