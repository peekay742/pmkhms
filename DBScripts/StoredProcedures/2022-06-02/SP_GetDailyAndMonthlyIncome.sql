/****** Object:  StoredProcedure [dbo].[SP_GetDailyAndMonthlyIncome]    Script Date: 6/3/2022 1:59:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 03
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetDailyAndMonthlyIncome]
	-- Add the parameters for the stored procedure here
	 @BranchId int=null
AS
BEGIN
	Create Table #tmpTable(Module nvarchar(100),DailyIncome decimal(18,2),MonthlyIncome decimal(18,2)) 
insert into #tmpTable (Module,DailyIncome)
	( SELECT 'Pharmacy',Sum(O.Total) FROM [Order] O
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND    
			(Convert(date,O.PaidDate)>=Convert(date,GETDATE())) AND 
            (Convert(date,O.PaidDate)<=Convert(date,GETDATE())) AND
            O.IsDelete = 0 and O.IsPaid=1)
update #tmpTable set MonthlyIncome=( SELECT Sum(O.Total) FROM [Order] O
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND    
			(Convert(date,O.PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,O.PaidDate)<=Convert(date, DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))) AND
            O.IsDelete = 0 and O.IsPaid=1 ) where Module='Pharmacy'

    insert into #tmpTable (Module,DailyIncome)
	( SELECT 'Lab', sum(O.Total) FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND           
            (Convert(date,O.PaidDate)>=Convert(date,GETDATE())) AND 
            (Convert(date,O.PaidDate)<=Convert(date,GETDATE())) AND 
            O.IsDelete = 0 and O.IsPaid=1)
	update #tmpTable set MonthlyIncome=( SELECT sum(O.Total) FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND           
            (Convert(date,O.PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,O.PaidDate)<=Convert(date,DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))) AND 
            O.IsDelete = 0 and O.IsPaid=1) where Module='Lab'
  
 insert into #tmpTable (Module,DailyIncome)
 (select 'In Patient', Sum(IPDP.Amount) from IPDRecord IR left join IPDPayment IPDP on IR.Id=IPDP.IPDRecordId where (@BranchId IS NULL OR IR.BranchId=@BranchId) AND 
 (Convert(date,IR.PaidDate)>=Convert(date,GETDATE())) AND 
            (Convert(date,IR.PaidDate)<=Convert(date,GETDATE())) AND 
            IR.IsDelete = 0 and IR.IsPaid=1)
update #tmpTable set MonthlyIncome= (select Sum(IPDP.Amount) from IPDRecord IR left join IPDPayment IPDP on IR.Id=IPDP.IPDRecordId where (@BranchId IS NULL OR IR.BranchId=@BranchId) AND 
 (Convert(date,IR.PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,IR.PaidDate)<=Convert(date,DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))) AND 
            IR.IsDelete = 0 and IR.IsPaid=1) where Module='In Patient'

insert into #tmpTable (Module,DailyIncome)
( SELECT 'Operation Threater', sum(O.Total) FROM OperationTreater O
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND            
            (Convert(date,PaidDate)>=Convert(date,GETDATE())) AND 
            (Convert(date,PaidDate)<=Convert(date,GETDATE())) AND 
            O.IsDelete = 0 and IsPaid=1)
update #tmpTable set MonthlyIncome=( SELECT sum(O.Total) FROM OperationTreater O
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND            
            (Convert(date,PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,PaidDate)<=Convert(date,DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))) AND 
            O.IsDelete = 0 and IsPaid=1) where Module='Operation Threater'

select * from #tmpTable
drop table #tmpTable
END
