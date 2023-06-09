USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOutletTransfers]    Script Date: 2/25/2022 10:19:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Feb 21, 2022
-- Description: Get All GetOutletTransfers
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetOutletTransfers]
    @BranchId INT = NULL,
    @OutletTransferId INT = NULL,
    @FromWarehouseId INT = NULL,
    @ToOutletId INT = NULL,
    @ItemId INT = NULL,
    @Remark NVARCHAR(MAX) = NULL,
    @Date DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT OT.*, FromW.Name AS [WarehouseName], ToOut.Name AS [OutletName], B.Name AS [BranchName]
    FROM OutletTransfer OT
        JOIN Branch B ON OT.BranchId = B.Id
        JOIN Warehouse FromW ON OT.WarehouseId = FromW.Id
        JOIN Outlet ToOut ON OT.OutletId = ToOut.Id
    WHERE 
        ((@BranchId IS NOT NULL AND OT.BranchId=@BranchId) OR
        (@BranchId IS NULL AND OT.Id IS NOT NULL)) AND
        ((@OutletTransferId IS NOT NULL AND OT.Id=@OutletTransferId) OR
        (@OutletTransferId IS NULL AND OT.Id IS NOT NULL)) AND
        ((@FromWarehouseId IS NOT NULL AND OT.WarehouseId=@FromWarehouseId) OR
        (@FromWarehouseId IS NULL AND OT.Id IS NOT NULL)) AND
        ((@ToOutletId IS NOT NULL AND OT.OutletId=@ToOutletId) OR
        (@ToOutletId IS NULL AND OT.Id IS NOT NULL)) AND
        ((@ItemId IS NOT NULL AND EXISTS 
            ( SELECT *
        FROM OutletTransferItem AS WI
        WHERE WI.OutletTransferId = OT.Id
            AND WI.ItemId=@ItemId
            )) OR
        (@ItemId IS NULL AND OT.Id IS NOT NULL)) AND
        ((@Remark IS NOT NULL AND (Remark LIKE '%' + @Remark + '%' OR @Remark LIKE '%' + Remark + '%')) OR
        (@Remark IS NULL AND OT.Id IS NOT NULL)) AND
        ((@Date IS NOT NULL AND [Date]=@Date) OR
        (@Date IS NULL AND OT.Id IS NOT NULL)) AND
        ((@StartDate IS NOT NULL AND [Date]>=@StartDate) OR
        (@StartDate IS NULL AND OT.Id IS NOT NULL)) AND
        ((@EndDate IS NOT NULL AND [Date]<=@EndDate) OR
        (@EndDate IS NULL AND OT.Id IS NOT NULL)) AND
        OT.IsDelete = 0
    ORDER BY [Date] DESC
END