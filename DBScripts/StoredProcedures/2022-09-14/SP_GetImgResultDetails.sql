/****** Object:  StoredProcedure [dbo].[SP_GetLabResultDetails]    Script Date: 9/28/2022 10:48:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Sep 28, 2022
-- Description: Get All ImagingResultDetails
-- =============================================
Alter PROCEDURE [dbo].[SP_GetImgResultDetails]
    @ImgResultId INT = NULL,
    @ImgResultDetailId INT = NULL,
    @IsTitle BIT = NULL,
    @IsPrinted BIT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT D.*, L.ResultNo AS [ImagingResultNo]
    FROM ImagingResultDetail D
        LEFT JOIN [ImagingResult] L ON L.Id=D.ImagingResultId
    WHERE 
        (@ImgResultId IS NULL OR ImagingResultId=@ImgResultId) AND
        (@ImgResultDetailId IS NULL OR D.Id=@ImgResultDetailId) AND
        (@IsTitle IS NULL OR IsTitle=@IsTitle) AND
        (@IsPrinted IS NULL OR IsPrinted=@IsPrinted)
    ORDER BY SortOrder
END