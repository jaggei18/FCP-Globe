USE [FirstCommand]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Rebuild_UserAgentNumbers_All_AWS'))
BEGIN
	DROP PROC [dbo].[Rebuild_UserAgentNumbers_All_AWS] 
END

/****** Object:  StoredProcedure [dbo].[Rebuild_UserAgentNumbers_All]    Script Date: 03/2/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Rebuild_UserAgentNumbers_All_AWS] 
AS
  SET NOCOUNT ON
  SET XACT_ABORT ON
  
  DECLARE @Today datetime,
          @ErrMessage varchar(255),
          @Error int
  
BEGIN TRAN
  SELECT @Today = GETDATE()
  -- Delete all UserAgentNumbers records --
  DELETE FROM UserAgentNumbers 
  -- Rebuild All UserAgentNumbers records -- 
  INSERT INTO UserAgentNumbers
        (ID_User,
         AgentNumber,
         Status,
         BasicRecord_Seqno,
         AddedDate)
  SELECT u.ID_User,
         cb.basic_Agent_Number,
         cb.basic_Status,
         cb.basic_Seqno,
         @Today
    FROM CELS_BasicRecord cb (NOLOCK)
    JOIN Users u (NOLOCK)
      ON cb.basic_SSN = u.PrimaryUserID

 SELECT @Error = @@Error
 IF @Error <> 0     
	BEGIN
	  -- Rollback Transaction
	  IF @@TRANCOUNT = 1 ROLLBACK TRAN
	  SELECT @ErrMessage = 'Rebuild User Agent Numbers Failed!' + Text FROM sys.Messages WHERE Message_ID = @Error
	  RAISERROR(@ErrMessage, 16, 1)
	  RETURN 1
	END
    
COMMIT TRAN
RETURN 0
GO
