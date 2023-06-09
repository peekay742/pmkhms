
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDeliverOrders]    Script Date: 4/21/2022 2:08:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_GetDeliverOrders] 
    @BranchId INT = NULL, ---
    @DeliverOrderId INT = NULL, ---
    @WarehouseId INT = NULL, ---
    @ItemId INT = NULL, ---
    @VoucherNo NVARCHAR(MAX) = NULL, ---
    @Customer NVARCHAR(MAX) = NULL, ---
    @Date DATETIME2(7) = NULL, 
    @StartDate DATETIME2(7) = NULL, 
    @EndDate DATETIME2(7) = NULL 
AS 
BEGIN 
 
    SET NOCOUNT ON 
 
    SELECT D.*,W.Name As [WarehouseName]  FROM DeliverOrder D JOIN Warehouse W ON D.WarehouseId=W.Id
        WHERE  
            ((@BranchId IS NOT NULL AND D.BranchId=@BranchId) OR  
            (@BranchId IS NULL AND D.Id IS NOT NULL)) AND 

            ((@DeliverOrderId IS NOT NULL AND D.Id=@DeliverOrderId) OR  
            (@DeliverOrderId IS NULL AND D.Id IS NOT NULL)) AND 

            ((@WarehouseId IS NOT NULL AND D.WarehouseId=@WarehouseId) OR  
            (@WarehouseId IS NULL AND D.Id IS NOT NULL)) AND 
            ((@ItemId IS NOT NULL AND EXISTS  
            ( SELECT * 
                FROM DeliverOrderItem AS PI 
                WHERE PI.DeliverOrderId = D.Id 
                AND PI.ItemId=@ItemId 
            )) OR  
            (@ItemId IS NULL AND D.Id IS NOT NULL)) AND

            ((@VoucherNo IS NOT NULL AND (D.VoucherNo LIKE '%' + @VoucherNo + '%' OR @VoucherNo LIKE '%' + D.VoucherNo + '%')) OR  
            (@VoucherNo IS NULL AND D.Id IS NOT NULL)) AND

            ((@Customer IS NOT NULL AND (D.Customer LIKE '%' + @Customer + '%' OR @Customer LIKE '%' + D.Customer + '%')) OR  
            (@Customer IS NULL AND D.Id IS NOT NULL)) AND 

            ((@Date IS NOT NULL AND D.Date=@Date) OR  
            (@Date IS NULL AND D.Id IS NOT NULL)) AND 
            ((@StartDate IS NOT NULL AND D.Date>=@StartDate) OR  
            (@StartDate IS NULL AND D.Id IS NOT NULL)) AND 
            ((@EndDate IS NOT NULL AND D.Date<=@EndDate) OR  
            (@EndDate IS NULL AND D.Id IS NOT NULL)) AND 

            D.IsDelete = 0 
        ORDER BY [VoucherNo] DESC 
END