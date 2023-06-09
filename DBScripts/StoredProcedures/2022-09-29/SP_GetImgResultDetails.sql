/****** Object:  StoredProcedure [dbo].[SP_GetImgResultDetails]    Script Date: 9/29/2022 2:35:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Sep 28, 2022
-- Description: Get All ImagingResultDetails
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetImgResultDetails]
    @ImgResultId INT = NULL,
    @ImgResultDetailId INT = NULL,
    @IsTitle BIT = NULL,
    @IsPrinted BIT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT D.*, L.ResultNo AS [ImagingResultNo],T.Name as LabTestName,P.Name as TechnicianName,LP.Name as ConsultantName
    FROM ImagingResultDetail D
        LEFT JOIN [ImagingResult] L ON L.Id=D.ImagingResultId
		LEFT JOIN LabTest T ON T.Id=D.LabTestId 
		LEFT JOIN LabPerson P on P.Id=D.TechnicianId
		LEFT JOIN LabPerson LP on LP.Id=D.ConsultantId
    WHERE 
        (@ImgResultId IS NULL OR ImagingResultId=@ImgResultId) AND
        (@ImgResultDetailId IS NULL OR D.Id=@ImgResultDetailId) AND
        (@IsTitle IS NULL OR IsTitle=@IsTitle) AND
        (@IsPrinted IS NULL OR IsPrinted=@IsPrinted)
    ORDER BY SortOrder
END