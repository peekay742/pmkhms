USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBooks]    Script Date: 28/2/2023 11:22:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_GetBooks]
    @BranchId INT = NULL,
    @BookId INT = NULL,
    @BookNo NVARCHAR(MAX) = NULL,
    @Date DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @PatientId INT = NULL,
    @DoctorId INT = NULL,
	@SpecilityId INT = NULL,
	@DepartmentId INT = NULL,
    @Status INT = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT B.*, P.Name AS [PatientName], D.Name AS [DoctorName]
    FROM Book B
        LEFT JOIN Patient P ON B.PatientId=P.Id
        LEFT JOIN Doctor D ON B.DoctorId=D.Id
    WHERE 
        (@BranchId IS NULL OR B.BranchId=@BranchId)AND
        (@BookId IS NULL OR B.Id=@BookId)AND
        (@BookNo IS NULL OR dbo.IncludeInEachOther([BookNo], @BookNo)=1)AND
        (@Date IS NULL OR [Date]=@Date)AND
        (@StartDate IS NULL OR Convert(date,[Date])>=Convert(date,@StartDate))AND
        (@EndDate IS NULL OR Convert(date,[Date])<=Convert(date,@EndDate))AND
        (@PatientId IS NULL OR PatientId=@PatientId) AND
        (@DoctorId IS NULL OR B.DoctorId=@DoctorId) AND
        (@Status IS NULL OR B.Status=@Status) AND
        B.IsDelete = 0 AND B.Status=1
    ORDER BY B.Status,B.Date
END