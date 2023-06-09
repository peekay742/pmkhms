USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetItemWithExpirationRemindDate]    Script Date: 3/22/2022 11:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022 Mar 18
-- Description:	Get Item with Expiry Date
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetItemWithExpirationRemindDate]
	-- Add the parameters for the stored procedure here
	@BranchId Int=null,
	@WarehouseId Int=null,
	@ItemId Int=null,
	@LocationId Int=null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  select Sum(WI.Qty) as Qty, I.Id as ItemId,I.Name as ItemName,I.Code as ItemCode,W.Name as WarehouseName,I.ExpirationRemindDay from Item I
	join Batch B on B.ItemId=I.Id
	join WarehouseItem WI on WI.ItemId=I.Id and WI.ItemId=B.ItemId and B.Id=WI.BatchId
	join Warehouse W on W.Id=WI.WarehouseId
	where (DateDiff(Day,GetDate(),B.ExpiryDate)<I.ExpirationRemindDay and I.ExpirationRemindDay is not null)
	and (@WarehouseId is null or WI.WarehouseId=@WarehouseId)
	and (@ItemId is null or I.Id=@ItemId)
	and (@BranchId is null or I.BranchId=@BranchId)
	and WI.Qty <>0
	group by  I.Id,I.Name,I.Code,W.Name,I.ExpirationRemindDay

END
