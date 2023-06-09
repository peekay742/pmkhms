/****** Object:  StoredProcedure [dbo].[SP_GetPatientResultImage]    Script Date: 7/14/2022 11:17:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022 July,08
-- Description:	Get Patient Result Image
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetPatientResultImage]
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@StartDate datetime=null,
	@EndDate datetime=null,
	@PatientId int=null,
	@PatientResultImageId int=null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select pimg.*,p.Name as PatientName from patientResultImages pimg 
	join Patient p on p.Id=pimg.PatientId
	where 
	(@BranchId is null or pimg.BranchId=@BranchId) And
	(@PatientId is null or pimg.PatientId=@PatientId) And
	(@StartDate IS NULL OR Convert(date,pimg.CreatedAt)>=Convert(date,@StartDate))AND
    (@EndDate IS NULL OR Convert(date,pimg.CreatedAt)<=Convert(date,@EndDate))AND
	(@PatientResultImageId is null or pimg.Id=@PatientResultImageId)
	and pimg.IsDelete=0

END
