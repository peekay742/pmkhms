/****** Object:  StoredProcedure [dbo].[SP_GetOutlets]    Script Date: 2/11/2022 1:12:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 05_Feb_2022
-- Description:	Get Outlets
-- =============================================
Create PROCEDURE [dbo].[SP_GetOutlets] 
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
		Select O.*,W.Name as Warehouse from Outlet O join Warehouse W on O.WarehouseId=W.Id where O.IsDelete=0 
		and ((@OutletId is not null and O.Id=@OutletId) or (@OutletId is null and O.Id is not null))
		and ((@BranchId is not null and O.BranchId=@BranchId) or (@OutletId is null and O.Id is not null))
		and ((@OutletName is not null and O.Name like '%'+@OutletName+'%') or (@OutletName is null and O.Name is not null))
		and ((@OutletCode is not null and O.Code like '%'+@OutletCode+'%') or (@OutletCode is null and O.Code is not null))
		and ((@WarehouseId is not null and O.WarehouseId=@WarehouseId) or (@WarehouseId is null and O.WarehouseId is not null))

END
