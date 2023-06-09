USE [thirisandar_hms_demolab]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabOrderTests]    Script Date: 27/01/2023 16:20:49 ******/
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
    @IsFOC BIT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT *, S.Name AS [LabTestName], R.Name AS [ReferrerName],C.Name AS [CollectionName] FROM LabOrderTest OS
    LEFT JOIN [LabTest] S ON OS.LabTestId=S.Id
    LEFT JOIN Referrer R ON OS.ReferrerId=R.Id
	LEFT JOIN Collection C ON OS.CollectionId=C.Id
        WHERE 
            (@LabOrderId IS NULL OR LabOrderId=@LabOrderId) AND
            (@LabTestId IS NULL OR LabTestId=@LabTestId) AND
            (@ReferrerId IS NULL OR ReferrerId=@ReferrerId)AND
			(@CollectionId IS NULL OR CollectionId=@CollectionId)
        ORDER BY SortOrder
END
