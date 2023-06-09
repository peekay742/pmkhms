/****** Object:  StoredProcedure [dbo].[SP_GetOutletOrderIncome]    Script Date: 6/6/2022 11:33:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 03
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetOutletOrderIncome]
	-- Add the parameters for the stored procedure here
	@BranchId int=null
AS
BEGIN
	Create Table #tmpTable(OutletId int,OutletName nvarchar(100),OrderQty int,DailyIncome decimal(18,2),MonthlyIncome decimal(18,2)) 
	SET NOCOUNT ON;

    insert into #tmpTable (OutletId,OutletName,DailyIncome)
   (SELECT Ot.Id,Ot.Name, Sum(O.Total) FROM [Order] O
   Join Outlet Ot on Ot.Id=O.OutletId
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND    
			( Convert(date,O.PaidDate)>=Convert(date,GETDATE())) AND 
            ( Convert(date,O.PaidDate)<=Convert(date,GETDATE())) AND
            O.IsDelete = 0 and O.IsPaid=1  group by Ot.Id,Ot.Name)
			
if((select count(*) from #tmpTable)>0)		
begin
update #tmpTable set MonthlyIncome=( SELECT Sum(O.Total) FROM [Order] O
   Join Outlet Ot on Ot.Id=O.OutletId
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND    
			(Convert(date,O.PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,O.PaidDate)<=Convert(date, DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))) AND
            O.IsDelete = 0 and O.IsPaid=1 )
end
else
begin
insert into #tmpTable (OutletId,OutletName,MonthlyIncome)
( SELECT O.OutletId,Ot.Name, Sum(O.Total) FROM [Order] O
   Join Outlet Ot on Ot.Id=O.OutletId
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND    
			(Convert(date,O.PaidDate)>=Convert(date,DATEADD(DD,-(DAY(GETDATE() -1)), GETDATE()))) AND 
            (Convert(date,O.PaidDate)<=Convert(date, DATEADD(DD,-(DAY(GETDATE())), DATEADD(MM, 1, GETDATE())))) AND
            O.IsDelete = 0 and O.IsPaid=1 group by O.OutletId,Ot.Name)
end



			select * from #tmpTable
			drop table #tmpTable
END
