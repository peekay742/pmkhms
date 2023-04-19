USE [thirisandar_hms_dev]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientSymptoms]    Script Date: 11/04/2023 21:38:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetPatientVitals]
    @MedicalRecordId INT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT *
    FROM PatientVital
    WHERE 
        (@MedicalRecordId IS NULL OR MedicalRecordId=@MedicalRecordId)
END