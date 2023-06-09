/****** Object:  StoredProcedure [dbo].[SP_GetDailyIncome]    Script Date: 8/1/2022 3:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 02
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetDailyIncome]
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL
AS
BEGIN
	Create Table #tmpTable(OrderIncome decimal(18,2),OPDIncome decimal(18,2),LabOrderIncome decimal(18,2),IPDIncome decimal(18,2),OTIncome decimal(18,2)) 
	Declare @ipdAmt decimal(18,2)
	SET NOCOUNT ON;


    -- Insert statements for procedure here
	insert into #tmpTable (OrderIncome)
	( SELECT Sum(O.Total) FROM [Order] O
    join Branch B on B.Id=O.BranchId
	left join Doctor D on D.Id=O.DoctorId
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND    
			(@StartDate IS NULL OR Convert(date,O.PaidDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,O.PaidDate)<=Convert(date,@EndDate)) AND
            O.IsDelete = 0 and O.IsPaid=1)
if((select count(*) from #tmpTable)>0)
begin
 update #tmpTable set OPDIncome=( SELECT sum(B.ClinicFee) FROM Visit V
        LEFT JOIN Patient P ON V.PatientId=P.Id
		LEFT JOIN Branch B ON V.BranchId=B.Id
        WHERE 
            (@BranchId IS NULL OR V.BranchId=@BranchId) AND           
            (@StartDate IS NULL OR Convert(date,V.Date)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,V.Date)<=Convert(date,@EndDate)) AND 
            V.IsDelete = 0 and V.Completed=1)
end
else
begin
insert into #tmpTable (OPDIncome)
( SELECT sum(B.ClinicFee) FROM Visit V
        LEFT JOIN Patient P ON V.PatientId=P.Id
		LEFT JOIN Branch B ON V.BranchId=B.Id
		
        WHERE 
            (@BranchId IS NULL OR V.BranchId=@BranchId) AND           
            (@StartDate IS NULL OR Convert(date,V.Date)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,V.Date)<=Convert(date,@EndDate)) AND 
            V.IsDelete = 0 and V.Completed=1)
end
if((select count(*) from #tmpTable)>0)
begin
			 update #tmpTable set LabOrderIncome=( SELECT sum(O.Total) FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND           
            (@StartDate IS NULL OR Convert(date,O.PaidDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,O.PaidDate)<=Convert(date,@EndDate)) AND 
            O.IsDelete = 0 and O.IsPaid=1)
end
else
begin
insert into #tmpTable (LabOrderIncome)
( SELECT sum(O.Total) FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND           
            (@StartDate IS NULL OR Convert(date,O.PaidDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,O.PaidDate)<=Convert(date,@EndDate)) AND 
            O.IsDelete = 0 and O.IsPaid=1)
end
if((select count(*) from #tmpTable)>0)
begin
update #tmpTable set IPDIncome=(select Sum(IPDP.Amount) from IPDRecord IR left join IPDPayment IPDP on IR.Id=IPDP.IPDRecordId where (@BranchId IS NULL OR IR.BranchId=@BranchId) AND 
 (@StartDate IS NULL OR Convert(date,IR.PaidDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,IR.PaidDate)<=Convert(date,@EndDate)) AND 
            IR.IsDelete = 0 and IR.IsPaid=1)
end
else
begin
insert into #tmpTable (IPDIncome)
(select Sum(IPDP.Amount) from IPDRecord IR left join IPDPayment IPDP on IR.Id=IPDP.IPDRecordId where (@BranchId IS NULL OR IR.BranchId=@BranchId) AND 
 (@StartDate IS NULL OR Convert(date,IR.PaidDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,IR.PaidDate)<=Convert(date,@EndDate)) AND 
            IR.IsDelete = 0 and IR.IsPaid=1)
end
if((select count(*) from #tmpTable)>0)
begin
update #tmpTable set OTIncome=( SELECT sum(O.Total) FROM OperationTreater O
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND            
            (@StartDate IS NULL OR Convert(date,PaidDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,PaidDate)<=Convert(date,@EndDate)) AND 
            O.IsDelete = 0 and IsPaid=1)
end
else
begin
insert into #tmpTable (OTIncome)
( SELECT sum(O.Total) FROM OperationTreater O
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND            
            (@StartDate IS NULL OR Convert(date,PaidDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,PaidDate)<=Convert(date,@EndDate)) AND 
            O.IsDelete = 0 and IsPaid=1)
end


select * from #tmpTable
drop table #tmpTable
END