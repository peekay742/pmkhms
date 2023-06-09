USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetReturnItems]    Script Date: 2/25/2022 10:06:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Feb 24, 2022
-- Description: Get All SP_GetReturnItems
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetReturnItems]
    @ReturnId INT = NULL,
    @ItemId INT = NULL,
    @UnitId INT = NULL,
    @BatchId INT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT * FROM ReturnItem 
        WHERE 
            ((@ReturnId IS NOT NULL AND ReturnId=@ReturnId) OR 
            (@ReturnId IS NULL AND ReturnId IS NOT NULL)) AND
            ((@ItemId IS NOT NULL AND ItemId=@ItemId) OR 
            (@ItemId IS NULL AND ItemId IS NOT NULL)) AND
            ((@UnitId IS NOT NULL AND UnitId=@UnitId) OR 
            (@UnitId IS NULL AND UnitId IS NOT NULL)) AND
            ((@BatchId IS NOT NULL AND BatchId=@BatchId) OR 
            (@BatchId IS NULL AND BatchId IS NOT NULL))
        ORDER BY SortOrder
END