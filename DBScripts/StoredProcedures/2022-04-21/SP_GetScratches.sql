
/****** Object:  StoredProcedure [dbo].[SP_GetScratches]    Script Date: 4/21/2022 1:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: Feb 25,2022
-- Description: Get Scratchs
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetScratches] 
    @BranchId INT = NULL, ---
    @ScratchId INT = NULL, ---
    @WarehouseId INT = NULL, ---
    @ItemId INT = NULL, ---
    @StartDate DATETIME2(7) = NULL, 
    @EndDate DATETIME2(7) = NULL 
AS 
BEGIN 
 
    SET NOCOUNT ON 
 
    SELECT D.*, W.Name As [WarehouseName] FROM Scratch D JOIN Warehouse W ON D.WarehouseId=W.Id
        WHERE  
            ((@BranchId IS NOT NULL AND D.BranchId=@BranchId) OR  
            (@BranchId IS NULL AND D.Id IS NOT NULL)) AND 

            ((@ScratchId IS NOT NULL AND D.Id=@ScratchId) OR  
            (@ScratchId IS NULL AND D.Id IS NOT NULL)) AND 

            ((@WarehouseId IS NOT NULL AND D.WarehouseId=@WarehouseId) OR  
            (@WarehouseId IS NULL AND D.Id IS NOT NULL)) AND 
            ((@ItemId IS NOT NULL AND EXISTS  
            ( SELECT * 
                FROM ScratchItem AS PI 
                WHERE PI.ScratchId = D.Id 
                AND PI.ItemId=@ItemId 
            )) OR  
            (@ItemId IS NULL AND D.Id IS NOT NULL)) AND            
            ((@StartDate IS NOT NULL AND D.Date>=@StartDate) OR  
            (@StartDate IS NULL AND D.Id IS NOT NULL)) AND 
            ((@EndDate IS NOT NULL AND D.Date<=@EndDate) OR  
            (@EndDate IS NULL AND D.Id IS NOT NULL)) AND 

            D.IsDelete = 0 
        ORDER BY CreatedAt DESC 
END