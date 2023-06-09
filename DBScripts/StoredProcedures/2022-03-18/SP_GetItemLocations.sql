USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetItemLocations]    Script Date: 3/18/2022 10:28:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_GetItemLocations]
    @BranchId INT = NULL,
    @ItemId INT = NULL,
    @LocationId INT = NULL,
    @ItemLocationId INT =NULL,
	@BatchId INT=NULL,
	@WarehouseId INT=NULL

AS
BEGIN

    SET NOCOUNT ON

    SELECT *,I.Name as ItemName,B.Name as BatchName,W.Name as WarehouseName,L.Name as LocationName FROM ItemLocation IL
	JOIN Item I on I.Id=IL.ItemId
	JOIN Batch B on B.Id=IL.BatchId
	JOIN Warehouse W on W.Id=IL.WarehouseId
	JOIN [Location] L on L.Id=IL.LocationId
        WHERE 
            (@BranchId IS NULL OR IL.BranchId=@BranchId) AND
            (@ItemId IS NULL OR IL.ItemId=@ItemId) AND
            (@LocationId IS NULL OR IL.LocationId=@LocationId) AND
            (@ItemLocationId IS NULL OR IL.Id=@ItemLocationId) AND
			(@BatchId IS NULL OR IL.BatchId=@BatchId) AND
			(@WarehouseId IS NULL OR IL.WarehouseId=@WarehouseId) AND
            IL.IsDelete = 0
END