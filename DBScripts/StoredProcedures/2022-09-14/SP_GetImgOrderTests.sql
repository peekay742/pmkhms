/****** Object:  StoredProcedure [dbo].[SP_GetLabOrderTests]    Script Date: 9/27/2022 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Sep 27, 2022
-- Description: Get All ImagingOrderTests
-- =============================================
Alter PROCEDURE [dbo].[SP_GetImgOrderTests]
    @ImagingOrderId INT = NULL,
    @LabTestId INT = NULL,
    @ReferrerId INT = NULL,
    @IsFOC BIT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT *, S.Name AS [LabTestName], R.Name AS [ReferrerName] FROM ImagingOrderTest OS
    LEFT JOIN [LabTest] S ON OS.LabTestId=S.Id
    LEFT JOIN Referrer R ON OS.ReferrerId=R.Id
        WHERE 
            (@ImagingOrderId  IS NULL OR ImagingOrderId=@ImagingOrderId ) AND
            (@LabTestId IS NULL OR LabTestId=@LabTestId) AND
            (@ReferrerId IS NULL OR ReferrerId=@ReferrerId)
        ORDER BY SortOrder
END