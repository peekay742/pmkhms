/****** Object:  StoredProcedure [dbo].[SP_LabOrderResultComplete]    Script Date: 9/5/2022 10:51:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,June 06
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_LabOrderResultComplete]
	-- Add the parameters for the stored procedure here
	@BranchId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select O.*,P.Name AS [PatientName], P.Guardian AS [PatientGuardian], B.Name as BranchName,B.Address AS [BranchAddress],B.Phone AS [BranchPhone],null as isCompleteResult,null as labResultId from LabOrder O 
	LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
left join LabOrderTest OT on O.Id=OT.LabOrderId
left join LabResult R on R.Id=OT.LabResultId
where R.IsCompleted=1
and (@BranchId is null Or O.BranchId=@BranchId)
and O.IsDelete=0
END
