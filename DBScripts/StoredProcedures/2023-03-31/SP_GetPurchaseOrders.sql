USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPurchaseOrders]    Script Date: 3/31/2023 3:24:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPurchaseOrders]
	-- Add the parameters for the stored procedure here
	 @BranchId INT = NULL,
    @PurchaseOrderId INT = NULL,
    @PurchaseItemId INT= NULL,
    @ItemId INT = NULL,
    @PurchaseOrderNo NVARCHAR(MAX) = NULL,
    @Supplier NVARCHAR(MAX) = NULL,
    @PurchaseOrderDate DATETIME2(7) = NULL,
    @StartPurchaseOrderDate DATETIME2(7) = NULL,
    @EndPurchaseOrderDate DATETIME2(7) = NULL


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT distinct PO.*,B.Name as BranchName,B.Address AS [BranchAddress],null as purchaseId  FROM [PurchaseOrder] PO
         
		LEFT JOIN Branch B ON PO.BranchId=B.Id
		--LEFT JOIN PurchaseItem PI ON PI.PurchaseOrderId=PO.Id
		WHERE 
            (@BranchId IS NULL OR PO.BranchId=@BranchId) AND
            (@PurchaseOrderId IS NULL OR PO.Id=@PurchaseOrderId) AND
			((@PurchaseItemId IS NOT NULL AND PO.Id=@PurchaseItemId) OR 
            (@PurchaseItemId IS NULL AND PO.Id IS NOT NULL)) AND
            --(@WarehouseId IS NULL OR @WarehouseId=@WarehouseId) AND
			((@ItemId IS NOT NULL AND EXISTS 
            ( SELECT *
                FROM PurchaseItem AS PI
                WHERE PI.PurchaseId = PO.Id
                AND PI.ItemId=@ItemId
            )) OR 
            (@ItemId IS NULL AND PO.Id IS NOT NULL)) AND
			(@ItemId IS NULL OR @ItemId=@ItemId) AND
            (@PurchaseOrderNo IS NULL OR (PurchaseOrderNO LIKE '%' + @PurchaseOrderNo + '%' OR @PurchaseOrderNo LIKE '%' + PurchaseOrderNo + '%')) AND
			(@Supplier IS NULL OR @Supplier=@Supplier) AND
			(@PurchaseOrderDate IS NULL OR [PurchaseOrderDate]=@PurchaseOrderDate)AND
            (@StartPurchaseOrderDate IS NULL OR [PurchaseOrderDate]>=@StartPurchaseOrderDate) AND 
			(@EndPurchaseOrderDate IS NULL OR [PurchaseOrderDate]<=@EndPurchaseOrderDate) AND 
            PO.IsDelete = 0
        ORDER BY [PurchaseOrderNO] DESC
END
