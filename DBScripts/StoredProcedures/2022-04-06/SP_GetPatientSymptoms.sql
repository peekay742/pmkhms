USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientSymptoms]    Script Date: 4/6/2022 3:13:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetPatientSymptoms]
    @MedicalRecordId INT = NULL,
    @SymptomId INT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT PS.*,S.Description as SymptomDesc
    FROM PatientSymptom PS
        LEFT JOIN Symptom S ON PS.SymptomId=S.Id
    WHERE 
        (@MedicalRecordId IS NULL OR PS.Id=@MedicalRecordId)AND
        (@SymptomId IS NULL OR PS.SymptomId=@SymptomId)
    ORDER BY [SortOrder] DESC
END
