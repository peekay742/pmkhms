/****** Object:  StoredProcedure [dbo].[SP_GetLabTestByLabOrderId]    Script Date: 6/2/2022 2:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,May 30
-- Description:	Get LabTest By lab order
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabTestByLabOrderId]
	-- Add the parameters for the stored procedure here
	@laborderId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select LOT.LabOrderId,LT.Name as LabTestName from LabTest LT 
	join LabOrderTest LOT on LT.Id=LOT.LabTestId
	where LOT.LabOrderId=@laborderId
End
