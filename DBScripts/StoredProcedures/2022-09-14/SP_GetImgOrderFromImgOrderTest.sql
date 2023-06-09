/****** Object:  StoredProcedure [dbo].[SP_GetLabOrderFromLabOrderTest]    Script Date: 9/27/2022 1:34:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022 Sep,27
-- Description:	Get ImagingOrder from ImagingOrderTest
-- =============================================
Create PROCEDURE [dbo].[SP_GetImgOrderFromImgOrderTest]
	-- Add the parameters for the stored procedure here
	@BranchId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select distinct l.*,'' as PatientName,'' as PatientGuardian,'' as BranchName,'' as BranchAddress,'' as BranchPhone,null as isCompleteResult,null as imagingResultId from ImagingOrder l 
	join ImagingOrderTest lt on lt.ImagingOrderId=l.Id where lt.ImagingResultId is null and l.IsDelete=0 and
	(@BranchId is null or l.BranchId=@BranchId)
END
