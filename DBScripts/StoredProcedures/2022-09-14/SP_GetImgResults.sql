/****** Object:  StoredProcedure [dbo].[SP_GetImgResults]    Script Date: 9/28/2022 3:04:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Sep 26, 2022
-- Description: Get All Imaging Results
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetImgResults]
    @BranchId INT = NULL,
    @ImgResultId INT = NULL,
    @LabTestId INT = NULL,
    @PatientId INT = NULL,
    @TechnicianId INT = NULL,
    @ConsultantId INT = NULL,
    @ResultNo NVARCHAR(MAX) = NULL,
    @IsCompleted BIT = NULL,
    @Date DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @CompletedDate DATETIME2(7) = NULL,
    @StartCompletedDate DATETIME2(7) = NULL,
    @EndCompletedDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT O.*, P.Name AS [PatientName],P.Gender as PatientSex,P.AgeYear as PatientAge, P.Guardian AS [PatientGuardian], B.Name as BranchName,B.Address AS [BranchAddress],B.Phone AS [BranchPhone], T.Name AS [TechnicianName], C.Name AS [ConsultantName], Test.Name AS [LabTestName]  FROM ImagingResult O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN LabPerson T ON O.TechnicianId=T.Id
		LEFT JOIN LabPerson C ON O.ConsultantId=C.Id
        LEFT JOIN LabTest Test ON O.LabTestId=Test.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND
            (@ImgResultId IS NULL OR O.Id=@ImgResultId) AND
            (@LabTestId IS NULL OR O.LabTestId=@LabTestId) AND
            (@PatientId IS NULL OR PatientId=@PatientId) AND
            (@TechnicianId IS NULL OR TechnicianId=@TechnicianId) AND
            (@ConsultantId IS NULL OR ConsultantId=@ConsultantId) AND
            (@ResultNo IS NULL OR (ResultNo LIKE '%' + @ResultNo + '%' OR @ResultNo LIKE '%' + ResultNo + '%')) AND
            (@IsCompleted IS NULL OR [IsCompleted]=@IsCompleted) AND
            (@Date IS NULL OR [Date]=@Date) AND
            (@StartDate IS NULL OR [Date]>=@StartDate) AND 
            (@EndDate IS NULL OR [Date]<=@EndDate) AND 
            (@CompletedDate IS NULL OR [CompletedDate]=@Date) AND
            (@StartCompletedDate IS NULL OR Convert(date,[CompletedDate])>=Convert(date,@StartCompletedDate)) AND 
            (@EndCompletedDate IS NULL OR Convert(date,[CompletedDate])<=Convert(date,@EndCompletedDate)) AND 
            O.IsDelete = 0 AND Test.Category=2
        ORDER BY [ResultNo] DESC
END
