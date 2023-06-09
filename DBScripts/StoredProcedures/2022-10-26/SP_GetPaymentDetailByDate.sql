/****** Object:  StoredProcedure [dbo].[SP_GetPaymentDetailByDate]    Script Date: 10/26/2022 10:03:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,Oct 17
-- Description:	Get payment detial by Date
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetPaymentDetailByDate]
	-- Add the parameters for the stored procedure here
	 @RecordId int,
	 @FromDate datetime,
     @ToDate datetime

AS
BEGIN
	Create Table #tblRoom([No] int,RoomName nvarchar(max),BedName nvarchar(max),RoomPrice decimal(18,2),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2),BranchId int)
Create Table #tblMedication([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,UnitName nvarchar(max),Amount decimal(18,2),SubTotal decimal(18,2),BranchId int)
Create Table #tblService([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2),BranchId int)
Create Table #tblStaff([No] int,FeesName nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2),BranchId int)
Create Table #tblFood([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2),BranchId int)
Create Table #tblLab([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2),BranchId int)
Create Table #tblImg([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2),BranchId int)
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into #tblRoom select top 1 null as [No], R.RoomNo,B.No,IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, R.Price,B.price),IA.UnitPrice), IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, R.Price,B.price),IA.UnitPrice) as UnitPrice,1 as Qty,IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, R.Price,B.price),IA.UnitPrice) as Amount,null as SubTotal,IR.BranchId from IPDRecord IR
join IPDAllotment IA on IA.IPDRecordId=IR.Id
join Room R on R.Id=IA.ToRoomId
left join Bed B on B.Id=IA.ToBedId
where IA.IPDRecordId=@RecordId and Convert(date,IA.[Date])>=Convert(date,@FromDate) and Convert(date,IA.[Date])<=Convert(date,@FromDate) and IA.IsDelete=0
Order by IA.CreatedAt Desc

if((select count(*) from #tblRoom)=0)
begin
insert into #tblRoom select top 1 null as [No], R.RoomNo,B.No,IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, R.Price,B.price),IA.UnitPrice), IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, R.Price,B.price),IA.UnitPrice) as UnitPrice,1 as Qty,IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, R.Price,B.price),IA.UnitPrice) as Amount,null as SubTotal,IR.BranchId from IPDRecord IR
join IPDAllotment IA on IA.IPDRecordId=IR.Id
join Room R on R.Id=IA.ToRoomId
left join Bed B on B.Id=IA.ToBedId
where IA.IPDRecordId=@RecordId  and IA.IsDelete=0 and IR.DODC is null Order by IA.CreatedAt Desc
end

insert into #tblMedication select null as [No],I.Name,IOi.UnitPrice,IOi.Qty,U.ShortForm,(IOi.UnitPrice*IOi.Qty) as Amount,null as SubTotal,IR.BranchId from IPDRecord IR
left join IPDOrderItem IOi on IOi.IPDRecordId=IR.Id
join Item I on I.Id=IOi.ItemId 
join Unit U on U.Id=IOi.UnitId
where IOi.IPDRecordId=@RecordId and Convert(date,IOi.[Date])>=Convert(date,@FromDate) and Convert(date,IOi.[Date])<=Convert(date,@ToDate) and IOi.IsDelete=0

insert into #tblService select null as [No],S.Name,IOS.UnitPrice,IOS.Qty,(IOS.UnitPrice*IOS.Qty) as Amount,null as SubTotal,IR.BranchId from IPDRecord IR
left join IPDOrderService IOS on IOS.IPDRecordId=IR.Id
join [Service]  S on S.Id=IOS.ServiceId
where IOS.IPDRecordId=@RecordId and Convert(date,IOS.[Date])>=Convert(date,@FromDate) and Convert(date,IOS.[Date])<=Convert(date,@ToDate) and IOS.IsDelete=0

insert into #tblStaff select null as [No],P.Name,St.Fee,1 as Qty,St.Fee as Amount,null as SubTotal,IR.BranchId from IPDRecord IR 
left join IPDStaff St on st.IPDRecordId=IR.Id
join Staff S on S.Id=St.StaffId
join Position P on P.Id=S.PositionId
where St.IPDRecordId=@RecordId and Convert(date,St.[Date])>=Convert(date,@FromDate) and Convert(date,St.[Date])<=Convert(date,@ToDate) and St.IsDelete=0

insert into #tblStaff select null as [No],D.Name,R.Fee,1 as Qty,R.Fee as Amount,null as SubTotal,IR.BranchId from IPDRecord IR 
left join IPDRound R on R.IPDRecordId=IR.Id
join Doctor d on d.Id=R.DoctorId
where R.IPDRecordId=@RecordId and Convert(date,R.[Date])>=Convert(date,@FromDate) and Convert(date,R.[Date])<=Convert(date,@ToDate) and R.IsDelete=0

insert into #tblFood select null as [No],F.Name,IPDF.UnitPrice,IPDF.Qty,(IPDF.UnitPrice*IPDF.Qty) as Amount,null as SubTotal,IR.BranchId from IPDRecord IR 
left join IPDFood IPDF on IPDF.IPDRecordId=IR.Id
join Food F on F.Id=IPDF.FoodId
where IPDF.IPDRecordId=@RecordId and Convert(date,IPDF.[Date])>=Convert(date,@FromDate) and Convert(date,IPDF.[Date])<=Convert(date,@ToDate) and IPDF.IsDelete=0

insert into #tblLab select null as [No],LT.Name,LOT.UnitPrice,0 as Qty,LOT.UnitPrice as Amount,null as SubTotal,IR.BranchId from IPDRecord IR left join IPDLab IL on IR.Id=IL.IPDRecordId
join LabOrderTest LOT on LOT.LabOrderId=IL.LabOrderId
join LabTest LT on LT.Id=LOT.LabTestId 
where IL.IPDRecordId=@RecordId and Convert(date,IL.[Date])>=convert(date,@FromDate) and 
Convert(date,IL.[Date])<=convert(date,@ToDate) and IL.IsDelete=0

insert into #tblImg select null as [No],LT.Name,LOT.UnitPrice,0 as Qty,LOT.UnitPrice as Amount,null as SubTotal,IR.BranchId from IPDRecord IR left join IPDImaging IL on IR.Id=IL.IPDRecordId
join ImagingOrderTest LOT on LOT.ImagingOrderId=IL.ImagingOrderId
join LabTest LT on LT.Id=LOT.LabTestId 
where IL.IPDRecordId=@RecordId and Convert(date,IL.[Date])>=convert(date,@FromDate) and 
Convert(date,IL.[Date])<=convert(date,@ToDate) and IL.IsDelete=0

select * from #tblRoom
select * from #tblMedication
select * from #tblService
select * from #tblStaff
select * from #tblFood
select * from #tblLab
select * from #tblImg

drop table  #tblRoom
drop table  #tblMedication
drop table  #tblService
drop table  #tblStaff
drop table  #tblFood
drop table #tblLab
drop table #tblImg
END
