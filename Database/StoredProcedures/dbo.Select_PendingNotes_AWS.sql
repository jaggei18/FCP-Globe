USE [FirstCommand]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_PendingNotes_AWS'))
BEGIN
	DROP PROC [dbo].[Select_PendingNotes_AWS] 
END


/****** Object:  StoredProcedure [dbo].[Select_PendingNotes]    Script Date: 03/27/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[Select_PendingNotes_AWS]
(@PolNo varchar(10))

 AS
 DECLARE   @ErrMessage varchar(255),
          @Error int

SELECT * FROM PendingNote  where PolicyNumber = @PolNo order by type, noteno, noteseq


  SELECT @Error = @@Error
  IF @Error <> 0     
	BEGIN
	  SELECT @ErrMessage = 'Error while fecthing pending note info - ' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END
  

RETURN 0
GO
