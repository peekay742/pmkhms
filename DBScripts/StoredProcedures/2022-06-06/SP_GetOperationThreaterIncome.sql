/****** Object:  StoredProcedure [dbo].[SP_GetOperationThreaterIncome]    Script Date: 6/8/2022 10:36:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 08
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetOperationThreaterIncome] 
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@StartDate datetime2(7)=null,
	@EndDate datetime2(7)=null
AS
BEGIN
	Create Table #tmpTable(OrderIncome decimal(18,2),LabOrderIncome decimal(18,2),IPDIncome decimal(18,2),OTIncome decimal(18,2)) 
	SET NOCOUNT ON;

    -- Insert statements for procedure here
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
