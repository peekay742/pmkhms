USE [thirisandar_hms_demolab]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabTestByOrderId]    Script Date: 08/02/2023 11:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Phyo Min Khant>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabTestByOrderId]
	-- Add the parameters for the stored procedure here
	@LabOrderId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select *,LP.Name as LabProfileName from LabTest LT join LabProfile LP on LT.LabProfileId=LP.Id where LT.Id in (select LabTestId from LabOrderTest where LabOrderId=@LabOrderId)
END
