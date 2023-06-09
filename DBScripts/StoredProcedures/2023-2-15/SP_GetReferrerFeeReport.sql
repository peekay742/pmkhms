USE [thirisandar_hms_]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetReferrerFeeReport]    Script Date: 2/15/2023 9:57:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetReferrerFeeReport]  
	-- Add the parameters for the stored procedure here
		@ReferrerId int=null,  
		@StartDate DATETIME2(7) = NULL,  
		@EndDate DATETIME2(7) = NULL  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET NOCOUNT ON;
	CREATE TABLE #Temp (Id INT,Name VARCHAR(255), LabFee decimal(18,2),OTFee decimal(18,2),OPDFee decimal(18,2));   
    --Lab        
      Insert into #Temp(Id,Name,LabFee) select r.Id as id,r.Name ,SUM(r.Fee) as LabFee  from Referrer r  
      join LabOrderTest lt on r.id=lt.ReferrerId  
      join LabOrder lo on lo.id=lt.LabOrderId  
      where   
      (@StartDate IS NULL OR Convert(date,lo.PaidDate)>=Convert(date,@StartDate)) AND   
      (@EndDate IS NULL OR Convert(date,lo.PaidDate)<=Convert(date,@EndDate)) AND  
         (@ReferrerId IS NULL OR r.Id=@ReferrerId) AND
		 lo.Discount=0--adding
		 group by r.Id,r.Name  
  
    --OT  
     Insert into #Temp(Id,Name,OTFee) select r.Id,r.Name,SUM(ot.ReferrerFee) as OTReferrerFee from OperationTreater ot  
      join Referrer r on ot.ReferrerId=r.Id   
      where (@StartDate IS NULL OR Convert(date,ot.PaidDate)>=Convert(date,@StartDate)) AND   
      (@EndDate IS NULL OR Convert(date,ot.PaidDate)<=Convert(date,@EndDate)) AND (@ReferrerId IS NULL OR r.Id=@ReferrerId) group by r.Id,ot.ReferrerFee,r.Name;  
  
    --OPD  
     Insert into #Temp(Id,Name,OPDFee) select r.Id,r.Name,SUM(r.Fee) as OPDFee from Referrer r  
      join OrderService os on os.ReferrerId=r.Id  
      join [Order] o on o.Id=os.OrderId  
      where (@StartDate IS NULL OR Convert(date,o.PaidDate)>=Convert(date,@StartDate)) AND   
      (@EndDate IS NULL OR Convert(date,o.PaidDate)<=Convert(date,@EndDate)) AND (@ReferrerId IS NULL OR r.Id=@ReferrerId) group by r.Id,r.Name;  
  
     Select * from #Temp order by Id;
END
