USE [thirisandar_hms_demolab]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabTests]    Script Date: 27/1/2023 10:20:05 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aung Naing OO
-- Create Date: May 07, 2022
-- Description: Get All LabTests
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabTests]
    @BranchId INT = NULL,
    @LabTestId INT = NULL,
    @Name NVARCHAR(MAX) = NULL,
    @Code NVARCHAR(MAX) = NULL,
	@LabProfileId INT = NULL,
    @IsLabReport BIT = NULL,
    @Category INT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT LT.*, LP.NAME AS [LabProfileName]
	FROM [LabTest] LT
		 LEFT JOIN LabProfile LP ON LT.[LabProfileId]=LP.Id

        WHERE 
            (@BranchId IS NULL OR LT.BranchId=@BranchId) AND
            (@LabTestId IS NULL OR LT.Id=@LabTestId) AND
            (@Name IS NULL OR dbo.IncludeInEachOther(LT.[Name], @Name)=1) AND
            (@Code IS NULL OR dbo.IncludeInEachOther([Code], @Code)=1) AND
			(@LabProfileId IS NULL OR LT.[LabProfileId] =@LabProfileId) AND
            (@IsLabReport IS NULL OR IsLabReport=@IsLabReport) AND
            (@Category IS NULL OR Category=@Category) AND
            LT.IsDelete = 0
        ORDER BY [Name]
END
