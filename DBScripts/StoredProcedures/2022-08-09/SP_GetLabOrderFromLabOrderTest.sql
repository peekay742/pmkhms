/****** Object:  StoredProcedure [dbo].[SP_GetLabOrderFromLabOrderTest]    Script Date: 8/9/2022 2:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022 July,28
-- Description:	Get LabOrder from LabOrderTest
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabOrderFromLabOrderTest]
	-- Add the parameters for the stored procedure here
	@BranchId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select distinct l.*,'' as PatientName,'' as PatientGuardian,'' as BranchName,'' as BranchAddress,'' as BranchPhone from LabOrder l 
	join LabOrderTest lt on lt.LabOrderId=l.Id where lt.LabResultId is null and l.IsDelete=0 and
	(@BranchId is null or l.BranchId=@BranchId)
END
