/****** Object:  StoredProcedure [dbo].[SP_GetPaymentAmountBydate]    Script Date: 5/5/2022 1:42:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,04 28
-- Description:	Get IPDRecord Detail Amount By Date
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPaymentAmountBydate] 
	-- Add the parameters for the stored procedure here
	@date datetime,
	@IPDRecordId int
AS
BEGIN
	create table #tmp1(RoomCharges decimal(18,2),Services decimal(18,2),Food decimal(18,2),Medications decimal(18,2),Fees decimal(18,2))
    create table #tmp2(Fees decimal(18,2))

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into #tmp1(RoomCharges)  select IIF(IA.UnitPrice=0.00,sum(R.Price),sum(IA.UnitPrice)) from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join Room R on R.Id=IA.ToRoomId	
	where Convert(date,IA.Date)=convert(date,@date) and IA.IPDRecordId=@IPDRecordId and IA.IsDelete=0 and R.IsDelete=0 group by IA.UnitPrice,R.Price

	update #tmp1 set Services=(select sum(S.Qty*S.Fee) as Services from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	join IPDOrder IOr on IOr.IPDRecordId=IR.Id
	left join IPDOrderService S on S.IPDRecordId=IR.Id and Convert(date,S.Date)=convert(date,@date)
	where Convert(date,IA.Date)=convert(date,@date) and IA.IPDRecordId=@IPDRecordId and S.IsDelete=0)

	update #tmp1 set Food=(select Convert(decimal,sum(F.Qty*F.UnitPrice)) as Food from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	join IPDOrder IOr on IOr.IPDRecordId=IR.Id
	left join IPDFood F on F.IPDRecordId=IR.Id and Convert(date,F.Date)=convert(date,@date)
	where Convert(date,IA.Date)=convert(date,@date) and IA.IPDRecordId=@IPDRecordId and F.IsDelete=0)

	update #tmp1 set Medications=(select sum(IOi.Qty*IOi.UnitPrice) as Medications from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	join IPDOrder IOr on IOr.IPDRecordId=IR.Id
	left join IPDOrderItem IOi on IOi.IPDRecordId=IR.Id and Convert(date,IOi.Date)=convert(date,@date)
	where Convert(date,IA.Date)=convert(date,@date) and IA.IPDRecordId=@IPDRecordId and IOi.IsDelete=0)


	insert into #tmp2 select sum(St.Fee) as Fees from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	join IPDOrder IOr on IOr.IPDRecordId=IR.Id
	left join IPDStaff St on St.IPDRecordId=IR.Id and Convert(date,St.Date)=convert(date,@date)
	where Convert(date,IA.Date)=convert(date,@date) and IA.IPDRecordId=@IPDRecordId and St.IsDelete=0

	insert into #tmp2 select sum(R.Fee) as Fees from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	join IPDOrder IOr on IOr.IPDRecordId=IR.Id
	left join IPDRound R on R.IPDRecordId=IR.Id and Convert(date,R.Date)=convert(date,@date)
	where Convert(date,IA.Date)=convert(date,@date) and IA.IPDRecordId=@IPDRecordId and R.IsDelete=0

	update #tmp1 set Fees=(select sum(Fees) from #tmp2)

	select *,null as [Day],null as [Date] from #tmp1

	drop table #tmp1

END
