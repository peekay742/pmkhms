/****** Object:  StoredProcedure [dbo].[SP_GetCFFeeReportPerDoctor]    Script Date: 6/24/2022 4:24:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 18
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCFFeeReportPerDoctor]
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@DoctorId int =null,
	@StartDate datetime2(7)=null,
	@EndDate datetime2(7)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Count(V.PatientId) as NoOfVisit,sum(D.CFFEE-(D.CFFee*D.CFFeeForHospital)) as CFFee, D.Name AS [DoctorName]

    FROM Visit V
	    Join Branch B on B.Id=V.BranchId
        LEFT JOIN VisitType VT ON V.VisitTypeId=VT.Id
        LEFT JOIN Patient P ON V.PatientId=P.Id
        LEFT JOIN Doctor D ON V.DoctorId=D.Id
    WHERE 
        (@BranchId IS NULL OR V.BranchId=@BranchId)AND  
		(@DoctorId IS NULL OR V.DoctorId=@DoctorId) AND
        (@StartDate IS NULL OR Convert(Date,[Date])>=Convert(Date,@StartDate))AND
        (@EndDate IS NULL OR Convert(Date,[Date])<=Convert(Date,@EndDate))AND		
        --(@Status IS NULL OR V.Status=@Status) AND
        V.IsDelete = 0
		group by D.Name
END
