USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('IsUserAdminLevel_By_SecurityLevel_AWS'))
BEGIN
	DROP PROC [dbo].[IsUserAdminLevel_By_SecurityLevel_AWS] 
END

/****** Object:  StoredProcedure [dbo].[IsUserAdminLevel_By_SecurityLevel_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[IsUserAdminLevel_By_SecurityLevel_AWS] 
  @SecurityLevel tinyint,
  @AdminLevelInd varchar(1) OUTPUT
AS
  SET NOCOUNT ON
  SELECT @AdminLevelInd = CASE @SecurityLevel
                             WHEN 255 THEN 'Y'
                             ELSE 'N'
                          END
  RETURN 0
GO
