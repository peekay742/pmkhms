/****** Object:  StoredProcedure [dbo].[SP_CalculateDailyPayment]    Script Date: 10/26/2022 11:14:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,Sep 14
-- Description:	Calculate Deposit Amount for Admission Patient
-- =============================================
ALTER PROCEDURE [dbo].[SP_CalculateDailyPayment]
	-- Add the parameters for the stored procedure here
	@IPDRecordId int,
    @date datetime
AS
BEGIN
Declare @amount decimal(18,2)=0.00
Declare @Room decimal(18,2)=0.00
Declare @Service decimal(18,2)=0.00
Declare @Medication decimal(18,2)=0.00
Declare @Food decimal(18,2)=0.00
Declare @Round decimal(18,2)=0.00
Declare @Staff decimal(18,2)=0.00
Declare @Lab decimal(18,2)=0.00
Declare @Img decimal(18,2)=0.00
Declare @Payment decimal(18,2)=0.00
	SET NOCOUNT ON;


set @Room=( select IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, sum(R.Price),sum(B.price)),sum(IA.UnitPrice)) from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join Room R on R.Id=IA.ToRoomId
	left join Bed B on B.Id=IA.ToBedId
	where Convert(date,IA.Date)=convert(date,@date) and IA.IPDRecordId=@IPDRecordId and IA.IsDelete=0 and R.IsDelete=0 group by IA.UnitPrice,R.Price,IA.ToBedId)
	if(@Room is null)
	begin
	set @Room=(select IIF(IA.UnitPrice=0.00,IIF(IA.ToBedId is null, sum(R.Price),sum(B.price)),sum(IA.UnitPrice)) from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join Room R on R.Id=IA.ToRoomId
	left join Bed B on B.Id=IA.ToBedId
	where IA.IPDRecordId=@IPDRecordId and IA.IsDelete=0 and R.IsDelete=0 group by IA.UnitPrice,R.Price,IA.ToBedId)
	end
	
set @Service=(select sum(S.Qty*S.UnitPrice) as Services from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join IPDOrderService S on S.IPDRecordId=IR.Id and Convert(date,S.Date)=convert(date,@date)
	where  IA.IPDRecordId=@IPDRecordId and S.IsDelete=0)

set @Food=(select Convert(decimal,sum(F.Qty*F.UnitPrice)) as Food from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join IPDFood F on F.IPDRecordId=IR.Id and Convert(date,F.Date)=convert(date,@date)
	where  IA.IPDRecordId=@IPDRecordId and F.IsDelete=0)

set @Medication=(select sum(IOi.Qty*IOi.UnitPrice) as Medications from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join IPDOrderItem IOi on IOi.IPDRecordId=IR.Id and Convert(date,IOi.Date)=convert(date,@date)
	where  IA.IPDRecordId=@IPDRecordId and IOi.IsDelete=0)

set @Lab=(select sum(LOT.UnitPrice) from IPDLab IL join IPDRecord IR on IR.Id=IL.IPDRecordId
	join LabOrderTest LOT on LOT.LabOrderId=IL.LabOrderId and Convert(date,IL.Date)=Convert(date,@date)
	where IL.IPDRecordId=@IPDRecordId and IL.IsDelete=0) 

set @Lab=(select sum(LOT.UnitPrice) from IPDImaging IL join IPDRecord IR on IR.Id=IL.IPDRecordId
	join ImagingOrderTest LOT on LOT.ImagingOrderId=IL.ImagingOrderId and Convert(date,IL.Date)=Convert(date,@date)
	where IL.IPDRecordId=@IPDRecordId and IL.IsDelete=0) 

set @Staff=(select sum(St.Fee) as Fees from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join IPDStaff St on St.IPDRecordId=IR.Id and Convert(date,St.Date)=convert(date,@date)
	where  IA.IPDRecordId=@IPDRecordId and St.IsDelete=0)

set @Round=(select sum(R.Fee) as Fees from IPDAllotment IA
	join IPDRecord IR on IR.Id=IA.IPDRecordId
	left join IPDRound R on R.IPDRecordId=IR.Id and Convert(date,R.Date)=convert(date,@date)
	where  IA.IPDRecordId=@IPDRecordId and R.IsDelete=0)

set @amount=@Room+IIF(@Service is null,0.00,@Service)+IIF(@Food is null,0.00,@Food)+IIF(@Medication is null,0.00,@Medication)+IIF(@Lab is null,0.00,@Lab)+IIF(@Img is null,0.00,@Img)+IIF(@Staff is null,0.00,@Staff)+IIF(@Round is null,0.00,@Round)



set @payment=(select top(1) Amount from IPDPayment where IPDRecordId=@IPDRecordId Order by CreatedAt Desc)
if(@Payment>=@amount)
begin
update IPDPayment set Amount=@Payment-@amount where IPDRecordId=@IPDRecordId and PaymentType=3 and Id=(select top(1) Id from IPDPayment where IPDRecordId=@IPDRecordId order by CreatedAt Desc)

select top(1) P.*,Pa.Name as PatientName,'Enough Amount' as PaymentStatus from IPdPayment P join IPDRecord R on P.IPDRecordId=R.Id join Patient Pa on Pa.Id=R.PatientId where IPDRecordId=@IPDRecordId Order by CreatedAt Desc
end
else
begin
select top(1) P.*,Pa.Name as PatientName,'Not Enough Amount' as PaymentStatus from IPdPayment P join IPDRecord R on P.IPDRecordId=R.Id join Patient Pa on Pa.Id=R.PatientId where IPDRecordId=@IPDRecordId Order by CreatedAt Desc
end
END
