
/****** Object:  StoredProcedure [dbo].[SP_GetOutlets]    Script Date: 4/21/2022 1:19:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 05_Feb_2022
-- Description:	Get Outlets
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetOutlets] 
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@OutletName nvarchar(MAX)=null,
	@OutletCode nvarchar(MAX)=null,
	@WarehouseId int=null,
	@OutletId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT O.*,W.Name as [WarehouseName] FROM Outlet O JOIN Warehouse W ON O.WarehouseId=W.Id 
		WHERE 
		(@OutletId IS NULL OR O.Id=@OutletId)
		AND (@BranchId IS NULL OR O.BranchId=@BranchId)
		AND (@OutletName IS NULL OR O.Name like '%'+@OutletName+'%')
		AND (@OutletCode IS NULL OR O.Code like '%'+@OutletCode+'%')
		AND (@WarehouseId IS NULL OR O.WarehouseId=@WarehouseId)
		AND O.IsDelete=0 

END