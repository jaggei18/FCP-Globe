USE [FirstCommand]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('Select_PolicyList_AWS'))
BEGIN
	DROP PROC [dbo].[Select_PolicyList_AWS] 
END

/****** Object:  StoredProcedure [dbo].[Select_PolicyList_AWS]    Script Date: 03/22/2023 2:26:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Select_PolicyList_AWS]
 -- @SessionGUID uniqueidentifier,
  @ID_User int,
  @PolicyNumber varchar(10),
  @InquiryType varchar(1) = Null, -- This may not be valid as this field is not captured in inquiry page ....???
  @Policy_SSNO varchar(9) = Null  -- This may not be valid as this field is not captured in inquiry page ....???
  
AS
------------------------------------------------------------------------------
-- @InquiryType Valid Values -- 
--   'G' -- Government Allotment 
--   'B' -- Bank Draft
--   'P' -- Policy Inquiry
------------------------------------------------------------------------------

  SET NOCOUNT ON

  DECLARE @SQL varchar(8000),
          @WhereInd bit,
          @ErrMessage varchar(255),
          @Message varchar(255),
          @Today datetime,
          @SPParams varchar(8000),
          @ReturnedErrMessage varchar(255),
          @Error int,
          @SessionGUID_Converted varchar(255),
          @AdminLevelInd varchar(1) = 'N',
		  @Policy_Suffix varchar(9)
   
  SELECT @Today = getdate()
  SELECT @WhereInd = 0

  IF @ID_User is Null OR @PolicyNumber is NULL 
  BEGIN
	SELECT @ErrMessage = 'Search failed...No search criteria received!'
	GOTO ReturnErrorFromServerAndExit
  END

    -- Get User Admin Security Level Indicator --
  /*EXEC IsUserAdminLevel_By_SessionGUID @SessionGUID = @SessionGUID,
                                       @AdminLevelInd = @AdminLevelInd OUTPUT,
                                       @ErrMessage = @ReturnedErrMessage OUTPUT */

  SELECT @AdminLevelInd = CASE SecurityLevel
                          WHEN 255 THEN 'Y'
							ELSE 'N'
                          END
  FROM Users u (NOLOCK)
  WHERE ID_User = @ID_User
  
 /* -- Unique Identifiers (GUIDs) drop the brackets({}) when converting to varchars --
  -- therefore, need to add back manually                                         --
  SELECT @SessionGUID_Converted = @SessionGUID
  IF LEFT(@SessionGUID_Converted, 1) <> '{' 
    SELECT @SessionGUID_Converted = '{' + @SessionGUID_Converted
  IF RIGHT(@SessionGUID_Converted, 1) <> '}' 
    SELECT @SessionGUID_Converted = @SessionGUID_Converted + '}'
      
  SELECT @SPParams =                   '@Session_GUID=' + ISNULL(@SessionGUID_Converted, '')
  SELECT @SPParams = @SPParams + '|' + '@InquiryType=' + ISNULL(@InquiryType, '')
  SELECT @SPParams = @SPParams + '|' + '@Policy_Suffix=' + ISNULL(@Policy_Suffix, '')
  SELECT @SPParams = @SPParams + '|' + '@Policy_SSNO=' + ISNULL(@Policy_SSNO, '') 
  */

  --Extract @Policy_Suffix from Policy Number entered by User
  SELECT @PolicyNumber = LTRIM(RTRIM(@PolicyNumber))

   IF Len(@PolicyNumber) = 10  SELECT @Policy_Suffix = Right(@PolicyNumber, 9)
   IF Len(@PolicyNumber) = 9 
			IF Upper(Left(@PolicyNumber,1)) = 'G' OR Upper(Left(@PolicyNumber,1)) = 'A' 
				SELECT @Policy_Suffix =  '0' + Right(@PolicyNumber, 8)
			Else
				SELECT @Policy_Suffix = Right(@PolicyNumber, 9)
   IF Len(@PolicyNumber) = 8
		IF Upper(Left(@PolicyNumber,1)) = 'G' OR Upper(Left(@PolicyNumber,1)) = 'A' 
			SELECT @Policy_Suffix = '00' + Right(@PolicyNumber, 7)
		Else
			IF (isnumeric(@PolicyNumber) = 1) SELECT @Policy_Suffix = '0' + @PolicyNumber
			
   IF Len(@PolicyNumber) = 7
			IF (isnumeric(@PolicyNumber) = 1) SELECT @Policy_Suffix = '00' + @PolicyNumber
              
   IF Len(@PolicyNumber) < 7 OR @Policy_Suffix IS NULL
   BEGIN
		SELECT @ErrMessage = 'Warning: Policy Number should contain at least 7 digits. Please rekey'
		RAISERROR(@ErrMessage, 16, 1)
		RETURN 1 
   END
						  
  -- Build Dynamic SQL Statement --
  SELECT @SQL = 'SELECT b.PolicyNumber, ' 
  SELECT @SQL = @SQL + 'b.Company, '
  SELECT @SQL = @SQL + 'b.SSNO, '
  SELECT @SQL = @SQL + 'b.Status, '
  SELECT @SQL = @SQL + 'b.Bill_Type, '
  SELECT @SQL = @SQL + 'b.Writing_Agent, '
  SELECT @SQL = @SQL + 'CASE '
  SELECT @SQL = @SQL +    'WHEN ' + CHAR(39) + @AdminLevelInd + CHAR(39) + ' = ' + CHAR(39) + 'Y' + CHAR(39) + ' THEN ' + CHAR(39) + 'Y' + CHAR(39) + ' '
  SELECT @SQL = @SQL +    'WHEN uan.Status = ' + CHAR(39) + 'A' + CHAR(39) + ' THEN ' + CHAR(39) + 'Y' + CHAR(39) + ' '
  SELECT @SQL = @SQL +    'ELSE ' + CHAR(39) + 'N' + CHAR(39) + ' '
  SELECT @SQL = @SQL + 'END ServicingAgentInd '
  SELECT @SQL = @SQL + 'FROM Basic b (NOLOCK) '
  SELECT @SQL = @SQL + 'LEFT JOIN UserAgentNumbers uan (NOLOCK) '
  --SELECT @SQL = @SQL +    'JOIN UserSession us (NOLOCK) '
  SELECT @SQL = @SQL +      'ON uan.ID_User = ' +  CONVERT(CHAR, @ID_User) + ' '
 -- SELECT @SQL = @SQL +     'AND us.SessionGUID = ' + CHAR(39) + @SessionGUID_Converted + CHAR(39) + ' '
  SELECT @SQL = @SQL +      'AND b.Writing_Agent = uan.AgentNumber '

  -- Append Policy Number to the Where clause --
  IF LEN(RTRIM(ISNULL(@Policy_Suffix, ''))) > 0
  BEGIN
    IF @WhereInd = 0 
    BEGIN
      SELECT @SQL = @SQL + 'WHERE '
      SELECT @WhereInd = 1
    END
    ELSE
    BEGIN
      SELECT @SQL = @SQL + 'AND '
    END
    SELECT @SQL = @SQL + 'b.Policy_Suffix = ' + CHAR(39) + @Policy_Suffix + CHAR(39) + ' '
  END
     
  -- Append Social Security Number to the Where clause --
  IF LEN(RTRIM(ISNULL(@Policy_SSNO, ''))) > 0
  BEGIN
    IF @WhereInd = 0 
    BEGIN
      SELECT @SQL = @SQL + 'WHERE '
      SELECT @WhereInd = 1
    END
    ELSE
    BEGIN
      SELECT @SQL = @SQL + 'AND '
    END
    SELECT @SQL = @SQL + 'b.SSNO = ' + CHAR(39) + @Policy_SSNO + CHAR(39) + ' '
  END

  -- Verify that atleast one of the Critera was received --
  IF @WhereInd = 0
  BEGIN
    SELECT @ErrMessage = 'Search failed...No search criteria received!'
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1
  END

  -- Append BillType --
  IF @InquiryType = 'B'
  BEGIN
    SELECT @SQL = @SQL + 'AND b.Bill_Type = ' + CHAR(39) + 'BB' + CHAR(39) + ' '
  END
  ELSE IF @InquiryType = 'G'
  BEGIN
    SELECT @SQL = @SQL + 'AND b.Bill_Type = ' + CHAR(39) + 'GA' + CHAR(39) + ' '
  END

  -- Execute Query <Return Results to Client> --
--PRINT @SQL
  EXEC (@SQL)  
    
  SELECT @Error = @@ERROR
  IF @Error <> 0 GOTO ReturnErrorFromServerAndExit

RETURN 0
   
---------------------------------------------------------------------------
-- Rollback Tran Raise Error and Exit
---------------------------------------------------------------------------
ReturnErrorFromServerAndExit:
    --IF @@TRANCOUNT = 1 ROLLBACK TRAN
    SELECT @ErrMessage = 'Error while selecting Policy List - ' + Text FROM Sys.Messages WHERE message_id = @Error
    RAISERROR(@ErrMessage, 16, 1)
    RETURN 1

GO


/*
exec Select_PolicyList_AWS 673, '5605066'

*/