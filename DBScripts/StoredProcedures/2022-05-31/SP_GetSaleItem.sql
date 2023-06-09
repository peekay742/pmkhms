/****** Object:  StoredProcedure [dbo].[SP_GetSaleItem]    Script Date: 6/1/2022 9:12:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,May 31
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetSaleItem] 
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@FromDate datetime=null,
	@ToDate datetime=null,
	@OutletId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select I.Id,I.Name As ItemName,I.Code ItemCode,Sum(OI.QtyInSmallestUnit) as Qty,Sum(OI.Qty*OI.UnitPrice) as Amount,Ol.Name as OutletName from [Order] O 
join OrderItem OI on OI.OrderId=O.Id
join Outlet Ol on Ol.Id=O.OutletId
join Item I on I.Id=OI.ItemId


where
(@BranchId IS NULL Or O.BranchId=@BranchId) AND
(@FromDate IS NULL Or O.Date>=@FromDate) AND
(@ToDate Is NULL Or O.Date<=@ToDate) AND
(@OutletId IS NULL Or O.OutletId=@OutletId) AND
O.IsDelete=0
group by I.Id,I.Name,I.Code,Ol.Name
END
