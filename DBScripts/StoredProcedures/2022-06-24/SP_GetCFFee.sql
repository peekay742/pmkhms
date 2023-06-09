/****** Object:  StoredProcedure [dbo].[SP_GetCFFee]    Script Date: 6/24/2022 4:22:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 07
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCFFee]
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@Status int =null,
	@StartDate datetime2(7)=null,
	@EndDate datetime2(7)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select sum(D.CFFEE-(D.CFFee*D.CFFeeForHospital)) as Amount from Visit V join Doctor D on D.Id=V.DoctorId
join Branch B on B.Id=D.BranchId
where 
(@BranchId IS NULL OR V.BranchId=@BranchId) AND
(@Status IS NULL OR V.Status=@Status) And
(@StartDate Is NULL OR Convert(date,V.Date)>=Convert(date,@StartDate))And
(@EndDate Is NULL OR Convert(date,V.Date)<=Convert(date,@EndDate))And
V.IsDelete=0
END
