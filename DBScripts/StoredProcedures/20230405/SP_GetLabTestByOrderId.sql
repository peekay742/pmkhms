USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabTestByOrderId]    Script Date: 4/5/2023 8:41:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
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
	select *,LP.Name As LabProfileName,CG.Name As CollectionGroupName From LabTest LT 
	JOIN LabProfile LP ON LT.LabProfileId=LP.Id 
	JOIN CollectionGroup CG ON LT.CollectionGroupId=CG.Id
	where LT.Id in (select LabTestId from LabOrderTest where LabOrderId=@LabOrderId) 
	--select LabTest.Id as id ,LabTest.Name as name from LabTest JOIN LabOrderTest on LabTest.Id = LabOrderTest.LabTestId 
	--where LabOrderTest.LabOrderId=@LabOrderId
	--SELECT LOT.*,LT.Name As [LabTestName]
	--FROM LabOrderTest LOT
	--	LEFT JOIN LabTest LT ON LT.Id = LOT.LabTestId
	--	LEFT JOIN LabOrder LO ON LO.Id = LOT.LabOrderId
	--WHERE 
	--	(@LabOrderId = NULL Or LabOrderId=@LabOrderId) AND
	--	(@LabTestId = NULL OR LabTestId=@LabTestId) AND 
	--	(@LabOrderTestId = NULL OR LOT.Id=@LabOrderTestId)
	--ORDER BY SortOrder
		
END
