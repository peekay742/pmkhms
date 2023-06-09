/****** Object:  StoredProcedure [dbo].[SP_GetDailyAndMonthlyLabOrderIncome]    Script Date: 6/6/2022 11:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 03
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetDailyAndMonthlyLabOrderIncome] 
	-- Add the parameters for the stored procedure here
	@BranchId int=null
AS
BEGIN
	Create Table #tmpTable(DailyIncome decimal(18,2),MonthlyIncome decimal(18,2)) 
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into #tmpTable (DailyIncome)
   (SELECT  sum(O.Total) FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND           
            (Convert(date,O.PaidDate)>=Convert(date,GETDATE())) AND 
            (Convert(date,O.PaidDate)<=Convert(date,GETDATE())) AND 
            O.IsDelete = 0 and O.IsPaid=1)
if((select count(*) from #tmpTable)>0)
begin
			update #tmpTable set MonthlyIncome=( SELECT sum(O.Total) FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND           
            (Convert(date,O.PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,O.PaidDate)<=Convert(date,DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))) AND 
            O.IsDelete = 0 and O.IsPaid=1 )
end
else
begin
insert into #tmpTable (MonthlyIncome)
( SELECT sum(O.Total) FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND           
            (Convert(date,O.PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,O.PaidDate)<=Convert(date,DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))))
end


		select * from #tmpTable

		drop table #tmpTable
END
