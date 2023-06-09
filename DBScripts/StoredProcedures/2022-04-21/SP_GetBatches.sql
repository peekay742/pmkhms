
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBatches]    Script Date: 4/21/2022 8:43:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aung Naing OO
-- Create Date: Feb 05, 2022
-- Description: Get All Batches
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetBatches]
    @BranchId INT = NULL,
    @ItemId INT = NULL,
    @BatchId INT = NULL,
    @BatchName NVARCHAR(MAX) = NULL,
    @BatchCode NVARCHAR(MAX) = NULL,
    @BatchNumber NVARCHAR(MAX) = NULL,
    @ExpiryDate DATETIME2(7) = NULL,
    @StartExpiryDate DATETIME2(7) = NULL,
    @EndExpiryDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT B.*,I.Name As [ItemName] FROM Batch B JOIN Item I ON B.ItemId=I.Id
        WHERE 
            ((@BranchId IS NOT NULL AND B.BranchId=@BranchId) OR 
            (@BranchId IS NULL AND B.Id IS NOT NULL)) AND
            ((@ItemId IS NOT NULL AND ItemId=@ItemId) OR 
            (@ItemId IS NULL AND B.Id IS NOT NULL)) AND
            ((@BatchId IS NOT NULL AND B.Id=@BatchId) OR 
            (@BatchId IS NULL AND B.Id IS NOT NULL)) AND
            ((@BatchName IS NOT NULL AND (B.Name LIKE '%' + @BatchName + '%' OR @BatchName LIKE '%' + B.Name + '%')) OR 
            (@BatchName IS NULL AND B.Id IS NOT NULL)) AND
            ((@BatchCode IS NOT NULL AND (B.Code LIKE '%' + @BatchCode + '%' OR @BatchCode LIKE '%' + B.Code + '%')) OR 
            (@BatchCode IS NULL AND B.Id IS NOT NULL)) AND
            ((@BatchNumber IS NOT NULL AND (B.BatchNumber LIKE '%' + @BatchNumber + '%' OR @BatchNumber LIKE '%' + B.BatchNumber + '%')) OR 
            (@BatchNumber IS NULL AND B.Id IS NOT NULL)) AND
            ((@ExpiryDate IS NOT NULL AND ExpiryDate=@ExpiryDate) OR 
            (@ExpiryDate IS NULL AND B.Id IS NOT NULL)) AND
            ((@StartExpiryDate IS NOT NULL AND ExpiryDate>=@StartExpiryDate) OR 
            (@StartExpiryDate IS NULL AND B.Id IS NOT NULL)) AND
            ((@EndExpiryDate IS NOT NULL AND ExpiryDate<=@EndExpiryDate) OR 
            (@EndExpiryDate IS NULL AND B.Id IS NOT NULL)) AND
            B.IsDelete = 0
        ORDER BY [Name]
END