USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_Beneficiary_AWS'))
BEGIN
	DROP PROC [dbo].[Select_Beneficiary_AWS] 
END

/****** Object:  StoredProcedure [dbo].[Select_Beneficiary]    Script Date: 03/31/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Select_Beneficiary_AWS]
(@PolNo varchar(10))
AS

DECLARE   @ErrMessage varchar(255),
          @Error int

SELECT Beneficiary.*, Beneficiary_Relation_Codes.Description AS BeneDesc, Beneficiary_Type_Codes.Description AS BeneType 
FROM Beneficiary 
INNER JOIN Beneficiary_Relation_Codes ON Beneficiary.Relation = Beneficiary_Relation_Codes.relation 
INNER JOIN Beneficiary_Type_Codes ON Beneficiary.Type = Beneficiary_Type_Codes.Type 
WHERE PolicyNumber = @PolNo order by Sequence


  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while fetching Beneficiary info - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END
  
RETURN 0


GO
