/****** Object:  StoredProcedure [dbo].[SP_GetLabOrderbyIPDRecord]    Script Date: 9/5/2022 3:36:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabOrderbyIPDRecord]
	-- Add the parameters for the stored procedure here
	@IPDRecordId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Distinct O.*,B.Name as BranchName,B.Phone as BranchPhone,B.Address as BranchAddress,P.Name as PatientName,P.Guardian as PatientGuardian,LR.IsCompleted as isCompleteResult,LR.Id as labResultId from IPDLab IL join LabOrder O on IL.LabOrderId=O.Id
	join Patient P on P.Id=O.PatientId 
	join Branch B on B.Id=O.BranchId
	left join LabOrderTest LOT on LOT.LabOrderId=O.Id
	left join LabResult LR on LR.Id=LOT.LabResultId
where IL.IPDRecordId=@IPDRecordId
END
