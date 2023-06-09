/****** Object:  StoredProcedure [dbo].[SP_GetOutletTransfers]    Script Date: 12/16/2022 3:16:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Feb 21, 2022
-- Description: Get All GetOutletTransfers
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetOutletTransfers]
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
       (@BranchId IS NULL OR OT.BranchId=@BranchId)  And
        (@OutletTransferId IS NULL OR OT.Id=@OutletTransferId) and
        
        (@FromWarehouseId IS NULL Or OT.WarehouseId=@FromWarehouseId) AND
        (@ToOutletId IS  NULL Or OT.OutletId=@ToOutletId) AND
        ((@ItemId IS NULL Or EXISTS 
            ( SELECT *
        FROM OutletTransferItem AS WI
        WHERE WI.OutletTransferId = OT.Id
            AND WI.ItemId=@ItemId
            )))  AND
        (@Remark IS  NULL Or (Remark LIKE '%' + @Remark + '%' OR @Remark LIKE '%' + Remark + '%')) AND
        (@Date IS NULL Or [Date]=@Date) AND
        (@StartDate IS NULL Or [Date]>=@StartDate) AND
        (@EndDate IS NULL Or [Date]<=@EndDate) AND
        OT.IsDelete = 0 and ToOut.IsDelete=0
    ORDER BY [Date] DESC
END