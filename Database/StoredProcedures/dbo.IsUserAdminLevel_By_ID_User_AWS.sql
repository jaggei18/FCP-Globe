USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('IsUserAdminLevel_By_ID_User_AWS'))
BEGIN
	DROP PROC [dbo].[IsUserAdminLevel_By_ID_User_AWS] 
END

/****** Object:  StoredProcedure [dbo].[IsUserAdminLevel_By_ID_User_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[IsUserAdminLevel_By_ID_User_AWS] 
  @ID_User int,
  @AdminLevelInd varchar(1) OUTPUT,
  @ErrMessage varchar(255) = NULL OUTPUT
AS
  SET NOCOUNT ON
  DECLARE @SecurityLevel tinyint, @Error Int

  SELECT @SecurityLevel = u.SecurityLevel
    FROM Users u(NOLOCK)
	WHERE u.ID_User = @ID_User
  
  IF @@ROWCOUNT = 0 OR @@ERROR <> 0
  BEGIN
    SELECT @ErrMessage = 'User not found!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END

  EXEC IsUserAdminLevel_By_SecurityLevel_AWS @SecurityLevel = @SecurityLevel,
                                         @AdminLevelInd = @AdminLevelInd OUTPUT
										 
  IF @@ERROR <> 0
  BEGIN
    SELECT @ErrMessage = 'Error while fetching Security level!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END

  RETURN 0
GO

/*
Declare @AdminLevelInd varchar(1) ,
  @ErrMessage varchar(255)
exec IsUserAdminLevel_By_ID_User_AWS 666, @AdminLevelInd OUTPUT, @ErrMessage OUTPUT

select @AdminLevelInd , @ErrMessage
574
666

*/