/****** Object:  StoredProcedure [dbo].[SP_GetVisitPatients]    Script Date: 6/7/2022 10:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 2022,May 27
-- Description:	Get Visit Patient
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetVisitPatients]
	-- Add the parameters for the stored procedure here
	 @BranchId INT = NULL,   
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,   
    @Status INT = NULL
AS
BEGIN

	SET NOCOUNT ON;

     SELECT V.Status, VT.[Type] AS [VisitTypeDesc], P.Name AS [PatientName],P.Gender as [PatientGender],IIF(P.DateOfBirth is null,P.AgeYear,(Year(GetDate())-Year(P.DateOfBirth))) as PatientAge, D.Name AS [DoctorName],V.VisitNo as VoucherNo

    FROM Visit V
        LEFT JOIN VisitType VT ON V.VisitTypeId=VT.Id
        LEFT JOIN Patient P ON V.PatientId=P.Id
        LEFT JOIN Doctor D ON V.DoctorId=D.Id
    WHERE 
        (@BranchId IS NULL OR V.BranchId=@BranchId)AND       
        (@StartDate IS NULL OR Convert(Date,[Date])>=Convert(Date,@StartDate))AND
        (@EndDate IS NULL OR Convert(Date,[Date])<=Convert(Date,@EndDate))AND
        (@Status IS NULL OR V.Status=@Status) AND
        V.IsDelete = 0
    ORDER BY V.Status,V.Date
END
