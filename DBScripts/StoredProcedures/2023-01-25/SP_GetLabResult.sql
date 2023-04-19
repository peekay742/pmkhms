USE [thirisandar_hms_demotesting]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabResults]    Script Date: 1/25/2023 1:53:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aung Naing OO
-- Create Date: May 13, 2022
-- Description: Get All LabResults
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabResults]
    @BranchId INT = NULL,
	@BarCode nvarchar(MAX)=null,
	@QRCode nvarchar(MAX)=null,
    @LabResultId INT = NULL,
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
    @EndCompletedDate DATETIME2(7) = NULL,
	@IsApprove BIT =NULL,
	@ApproveDate DATETIME2(7) = NULL,
    @StartApproveDate DATETIME2(7) = NULL,
    @EndApproveDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT O.*, P.Name AS [PatientName],P.Gender as PatientSex,P.AgeYear as PatientAge, P.Guardian AS [PatientGuardian], B.Name as BranchName,B.Address AS [BranchAddress],B.Phone AS [BranchPhone], T.Name AS [TechnicianName], C.Name AS [ConsultantName], Test.Name AS [LabTestName]  FROM [LabResult] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN LabPerson T ON O.TechnicianId=T.Id
		LEFT JOIN LabPerson C ON O.ConsultantId=C.Id
        LEFT JOIN LabTest Test ON O.LabTestId=Test.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND
            (@LabResultId IS NULL OR O.Id=@LabResultId) AND
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
			(@BarCode IS NULL OR P.BarCode=@BarCode) AND
			(@QRCode IS NULL OR P.QRCode=@QRCode) AND
			(@IsApprove IS NULL OR [IsApprove]=@IsApprove) AND
			(@ApproveDate IS NULL OR [ApproveDate]=@Date) AND
            (@StartApproveDate IS NULL OR Convert(date,[ApproveDate])>=Convert(date,@StartApproveDate)) AND 
            (@EndApproveDate IS NULL OR Convert(date,[ApproveDate])<=Convert(date,@EndApproveDate)) AND 
            O.IsDelete = 0 AND Test.Category=1
        ORDER BY [ResultNo] DESC
END
