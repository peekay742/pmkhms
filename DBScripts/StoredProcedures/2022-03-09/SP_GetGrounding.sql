USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetGrounding]    Script Date: 3/10/2022 10:49:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: Mar 09 2022
-- Description: Get Grounding
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetGrounding]
   @BranchId int=null,
   @WarehouseId int=null,
   @StartDate datetime=null,
   @EndDate datetime=null,
   @GroundingId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   SELECT G.*, W.Name AS [WarehouseName], B.Name AS [BranchName]
    FROM Grounding G
        JOIN Branch B ON G.BranchId = B.Id
        JOIN Warehouse W ON G.WarehouseId = W.Id
       
    WHERE 
		((@GroundingId IS  NULL AND G.Id=@GroundingId) OR
        (@GroundingId IS NULL AND G.Id IS NOT NULL)) AND
        ((@BranchId IS NOT NULL AND G.BranchId=@BranchId) OR
        (@BranchId IS NULL AND G.Id IS NOT NULL)) AND
        ((@WarehouseId IS NOT NULL AND G.WarehouseId=@WarehouseId) OR
        (@WarehouseId IS NULL AND G.Id IS NOT NULL)) AND        
        ((@StartDate IS NOT NULL AND [Date]>=@StartDate) OR
        (@StartDate IS NULL AND G.Id IS NOT NULL)) AND
        ((@EndDate IS NOT NULL AND [Date]<=@EndDate) OR
        (@EndDate IS NULL AND G.Id IS NOT NULL)) AND
        G.IsDelete = 0
    ORDER BY [Date] DESC 
END
