
/****** Object:  StoredProcedure [dbo].[SP_GetPurchases]    Script Date: 4/21/2022 2:30:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aung Naing OO
-- Create Date: Feb 08, 2022
-- Description: Get All Purchases
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetPurchases]
    @BranchId INT = NULL,
    @PurchaseId INT = NULL,
    @WarehouseId INT = NULL,
    @ItemId INT = NULL,
    @VoucherNo NVARCHAR(MAX) = NULL,
    @Supplier NVARCHAR(MAX) = NULL,
    @PurchaseDate DATETIME2(7) = NULL,
    @StartPurchaseDate DATETIME2(7) = NULL,
    @EndPurchaseDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT P.*,W.Name As [WarehouseName] FROM Purchase P JOIN Warehouse W ON P.WarehouseId=W.Id
        WHERE 
            ((@BranchId IS NOT NULL AND P.BranchId=@BranchId) OR 
            (@BranchId IS NULL AND P.Id IS NOT NULL)) AND
            ((@PurchaseId IS NOT NULL AND P.Id=@PurchaseId) OR 
            (@PurchaseId IS NULL AND P.Id IS NOT NULL)) AND
            ((@WarehouseId IS NOT NULL AND P.WarehouseId=@WarehouseId) OR 
            (@WarehouseId IS NULL AND P.Id IS NOT NULL)) AND
            ((@ItemId IS NOT NULL AND EXISTS 
            ( SELECT *
                FROM PurchaseItem AS PI
                WHERE PI.PurchaseId = P.Id
                AND PI.ItemId=@ItemId
            )) OR 
            (@ItemId IS NULL AND P.Id IS NOT NULL)) AND
            ((@VoucherNo IS NOT NULL AND (P.VoucherNo LIKE '%' + @VoucherNo + '%' OR @VoucherNo LIKE '%' + P.VoucherNo + '%')) OR 
            (@VoucherNo IS NULL AND P.Id IS NOT NULL)) AND
            ((@Supplier IS NOT NULL AND (P.Supplier LIKE '%' + @Supplier + '%' OR @Supplier LIKE '%' + P.Supplier + '%')) OR 
            (@Supplier IS NULL AND P.Id IS NOT NULL)) AND
            ((@PurchaseDate IS NOT NULL AND P.PurchaseDate=@PurchaseDate) OR 
            (@PurchaseDate IS NULL AND P.Id IS NOT NULL)) AND
            ((@StartPurchaseDate IS NOT NULL AND P.PurchaseDate>=@StartPurchaseDate) OR 
            (@StartPurchaseDate IS NULL AND P.Id IS NOT NULL)) AND
            ((@EndPurchaseDate IS NOT NULL AND P.PurchaseDate<=@EndPurchaseDate) OR 
            (@EndPurchaseDate IS NULL AND P.Id IS NOT NULL)) AND
            P.IsDelete = 0
        ORDER BY [VoucherNo] DESC
END