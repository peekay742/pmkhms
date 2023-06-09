USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOutletTransferItems]    Script Date: 2/24/2022 9:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Feb 21, 2022
-- Description: Get All SP_GetOutletTransferItems
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetOutletTransferItems]
    @OutletTransferId INT = NULL,
    @ItemId INT = NULL,
    @UnitId INT = NULL,
    @BatchId INT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT * FROM OutletTransferItem 
        WHERE 
            ((@OutletTransferId IS NOT NULL AND OutletTransferId=@OutletTransferId) OR 
            (@OutletTransferId IS NULL AND OutletTransferId IS NOT NULL)) AND
            ((@ItemId IS NOT NULL AND ItemId=@ItemId) OR 
            (@ItemId IS NULL AND ItemId IS NOT NULL)) AND
            ((@UnitId IS NOT NULL AND UnitId=@UnitId) OR 
            (@UnitId IS NULL AND UnitId IS NOT NULL)) AND
            ((@BatchId IS NOT NULL AND BatchId=@BatchId) OR 
            (@BatchId IS NULL AND BatchId IS NOT NULL))
        ORDER BY SortOrder
END