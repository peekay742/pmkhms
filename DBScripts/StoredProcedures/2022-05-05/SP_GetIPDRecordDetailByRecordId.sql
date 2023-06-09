/****** Object:  StoredProcedure [dbo].[SP_GetIPDRecordDetailByRecordId]    Script Date: 5/5/2022 1:40:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,May 02
-- Description:	Get IPDRecord Detail
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetIPDRecordDetailByRecordId]
	-- Add the parameters for the stored procedure here
	@RecordId int,
	@date datetime
AS
BEGIN
	Create Table #tblRoom([No] int,RoomName nvarchar(max),BedName nvarchar(max),RoomPrice decimal(18,2),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2))
Create Table #tblMedication([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,UnitName nvarchar(max),Amount decimal(18,2),SubTotal decimal(18,2))
Create Table #tblService([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2))
Create Table #tblStaff([No] int,FeesName nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2))
Create Table #tblFood([No] int,Name nvarchar(max),UnitPrice decimal(18,2),Qty int,Amount decimal(18,2),SubTotal decimal(18,2))
	SET NOCOUNT ON;
	insert into #tblRoom select null as [No], R.RoomNo,B.No,R.Price,R.Price as UnitPrice,1 as Qty,R.price as Amount,null as SubTotal from IPDAllotment IA 
join Room R on R.Id=IA.ToRoomId
left join Bed B on B.Id=IA.ToBedId
where IA.IPDRecordId=@RecordId and Convert(date,IA.[Date])=Convert(date,@date) and IA.IsDelete=0

insert into #tblMedication select null as [No],I.Name,IOi.UnitPrice,IOi.Qty,U.ShortForm,(IOi.UnitPrice*IOi.Qty) as Amount,null as SubTotal from IPDRecord IR
left join IPDOrderItem IOi on IOi.IPDRecordId=IR.Id
join Item I on I.Id=IOi.ItemId 
join Unit U on U.Id=IOi.UnitId
where IOi.IPDRecordId=@RecordId and Convert(date,IOi.[Date])=Convert(date,@date) and IOi.IsDelete=0

insert into #tblService select null as [No],S.Name,IOS.UnitPrice,IOS.Qty,(IOS.UnitPrice*IOS.Qty) as Amount,null as SubTotal from IPDRecord IR
left join IPDOrderService IOS on IOS.IPDRecordId=IR.Id
join [Service]  S on S.Id=IOS.ServiceId
where IOS.IPDRecordId=@RecordId and Convert(date,IOS.[Date])=Convert(date,@date) and IOS.IsDelete=0

insert into #tblStaff select null as [No],P.Name,St.Fee,1 as Qty,St.Fee as Amount,null as SubTotal from IPDRecord IR 
left join IPDStaff St on st.IPDRecordId=IR.Id
join Staff S on S.Id=St.StaffId
join Position P on P.Id=S.PositionId
where St.IPDRecordId=@RecordId and Convert(date,St.[Date])=Convert(date,@date) and St.IsDelete=0

insert into #tblStaff select null as [No],D.Name,R.Fee,1 as Qty,R.Fee as Amount,null as SubTotal from IPDRecord IR 
left join IPDRound R on R.IPDRecordId=IR.Id
join Doctor d on d.Id=R.DoctorId
where R.IPDRecordId=@RecordId and Convert(date,R.[Date])=Convert(date,@date) and R.IsDelete=0

insert into #tblFood select null as [No],F.Name,IPDF.UnitPrice,IPDF.Qty,(IPDF.UnitPrice*IPDF.Qty) as Amount,null as SubTotal from IPDRecord IR 
left join IPDFood IPDF on IPDF.IPDRecordId=IR.Id
join Food F on F.Id=IPDF.FoodId
where IPDF.IPDRecordId=@RecordId and Convert(date,IPDF.[Date])=Convert(date,@date) and IPDF.IsDelete=0

select * from #tblRoom
select * from #tblMedication
select * from #tblService
select * from #tblStaff
select * from #tblFood

drop table  #tblRoom
drop table  #tblMedication
drop table  #tblService
drop table  #tblStaff
drop table  #tblFood

   
END
