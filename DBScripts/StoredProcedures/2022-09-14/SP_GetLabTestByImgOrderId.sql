/****** Object:  StoredProcedure [dbo].[SP_GetLabTestByOrderId]    Script Date: 9/28/2022 10:04:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022, Sep 28
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_GetLabTestByImgOrderId]
	-- Add the parameters for the stored procedure here
	@ImagingOrderId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from LabTest where Id in (select LabTestId from ImagingOrderTest where ImagingOrderId=@ImagingOrderId)
END
