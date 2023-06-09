USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabOrderTests]    Script Date: 4/4/2023 8:46:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aung Naing OO
-- Create Date: May 13, 2022
-- Description: Get All LabOrderTests
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabOrderTests]
    @LabOrderId INT = NULL,
    @LabTestId INT = NULL,
    @ReferrerId INT = NULL,
	@CollectionId INT = NULL,
    @IsFOC BIT = NULL,
	@CollectionGroupId INT=NULL
AS
BEGIN

    SET NOCOUNT ON
	--SELECT *, S.Name AS [LabTestName], R.Name AS [ReferrerName],C.Name AS [CollectionName] FROM LabOrderTest OS
 --   LEFT JOIN [LabTest] S ON OS.LabTestId=S.Id
 --   LEFT JOIN Referrer R ON OS.ReferrerId=R.Id
	--LEFT JOIN Collection C ON OS.CollectionId=C.Id

    SELECT *, S.Name AS [LabTestName],CG.Name As [CollectionGroupName], R.Name AS [ReferrerName],C.Name AS [CollectionName] FROM LabOrderTest OS
    LEFT JOIN [LabTest] S ON OS.LabTestId=S.Id
    LEFT JOIN Referrer R ON OS.ReferrerId=R.Id
	LEFT JOIN Collection C ON OS.CollectionId=C.Id
	LEFT JOIN CollectionGroup CG ON S.CollectionGroupId=CG.Id
	--LEFT JOIN LabOrder O ON OS.LabOrderId=O.Id
        WHERE 
            (@LabOrderId IS NULL OR LabOrderId=@LabOrderId) AND
            (@LabTestId IS NULL OR LabTestId=@LabTestId) AND
            (@ReferrerId IS NULL OR ReferrerId=@ReferrerId)AND
			(@CollectionId IS NULL OR CollectionId=@CollectionId)AND
			(@CollectionGroupId IS NULL OR S.CollectionGroupId=@CollectionGroupId) 
        ORDER BY SortOrder
END
