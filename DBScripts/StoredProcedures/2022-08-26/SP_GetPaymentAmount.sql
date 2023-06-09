/****** Object:  StoredProcedure [dbo].[SP_GetPaymentAmount]    Script Date: 8/29/2022 11:20:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,04 28
-- Description:	Get IPDRecord Detail Amount By Date
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetPaymentAmount] 
	-- Add the parameters for the stored procedure here
	@IPDRecordId int
AS
BEGIN
	create table #tmp1([Description] nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2))
	create table #tmp2([Description] nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2))

	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
	insert into #tmp1 select 'RoomCharges', IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, sum(R.Price),sum(B.price)),sum(IA.UnitPrice)),0 as Qty, IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, sum(R.Price),sum(B.price)),sum(IA.UnitPrice)) from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join Room R on R.Id=IA.ToRoomId	
	left join Bed B on B.Id=IA.ToBedId
	where IA.IPDRecordId=@IPDRecordId and IA.IsDelete=0 and R.IsDelete=0 group by IA.UnitPrice,R.Price,IA.ToBedId

	insert into #tmp1 select 'Services',S.Fee,sum(S.Qty),sum(S.Qty*S.UnitPrice) from  IPDRecord IR 
	left join IPDOrderService S on S.IPDRecordId=IR.Id 
	where IR.Id=@IPDRecordId and S.IsDelete=0 group by S.Fee

	insert into #tmp1 select 'Foods',F.UnitPrice,sum(F.Qty), Convert(decimal,sum(F.Qty*F.UnitPrice)) as Food from IPDRecord IR 
	left join IPDFood F on F.IPDRecordId=IR.Id
	where  IR.Id=@IPDRecordId and F.IsDelete=0 group by F.UnitPrice

	insert into #tmp1 select 'Medications',IOi.UnitPrice,sum(IOi.Qty), sum(IOi.Qty*IOi.UnitPrice) as Medications from IPDRecord IR 
	left join IPDOrderItem IOi on IOi.IPDRecordId=IR.Id 
	where IR.Id=@IPDRecordId and IOi.IsDelete=0 group by IOi.UnitPrice

	insert into #tmp2 select 'Fees', St.Fee as Fees,0 as Qty,sum(St.Fee) from IPDRecord IR 
	left join IPDStaff St on St.IPDRecordId=IR.Id 
	left join IPDRound R on R.IPDRecordId=IR.Id
	where IR.Id=@IPDRecordId and St.IsDelete=0 group by St.Fee

	insert into #tmp2 select 'Fees', R.Fee as Fees,0 as Qty,sum(R.Fee) from IPDRecord IR 
	left join IPDRound R on R.IPDRecordId=IR.Id
	where IR.Id=@IPDRecordId and R.IsDelete=0  group by R.Fee
	--update #tmp2 set Qty=(select count(*) from #tmp2)
	
	insert into #tmp1 select 'Fees', sum(UnitPrice),0 as Qty,sum(UnitPrice) from #tmp2 
	select *,null as [Day],null as [Date],null as SubTotal from #tmp1

	drop table #tmp1
	drop table #tmp2



END