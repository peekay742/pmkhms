-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,Dec 21
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetPatientDiagnoses
	-- Add the parameters for the stored procedure here
	@MedicalRecordId INT = NULL,
    @DiagnosisId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select PDsis.* from PatientDiagnosis PDsis Left Join Diagnosis D On PDsis.DiagnosisId=D.Id
	 WHERE 
        (@MedicalRecordId IS NULL OR PDsis.MedicalRecordId=@MedicalRecordId)AND
        (@DiagnosisId IS NULL OR PDsis.DiagnosisId=@DiagnosisId)
    ORDER BY [SortOrder] DESC
END
GO
