USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabTestByLabOrderId]    Script Date: 3/31/2023 3:33:39 PM ******/
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
	select LOT.LabOrderId,LT.Name as LabTestName,CG.Name as CollectionGroupName from LabTest LT 
	JOIN CollectionGroup AS CG ON LT.CollectionGroupId=CG.Id
	join LabOrderTest LOT on LT.Id=LOT.LabTestId
	--select LOT.LabOrderId,LT.Name as LabTestName from LabTest LT 
	--join LabOrderTest LOT on LT.Id=LOT.LabTestId
	where LOT.LabOrderId=@laborderId 

	--select LT.Id,LT.Name
	--	FROM LabTest as LT
	--	INNER JOIN LabOrderTest As LOT
	--	ON LT.Id= LOT.LabTestId AND LOT.LabOrderId=@LabOrderId
End
