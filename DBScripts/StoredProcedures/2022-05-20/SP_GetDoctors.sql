/****** Object:  StoredProcedure [dbo].[SP_GetDoctors]    Script Date: 5/20/2022 3:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetDoctors]
    @BranchId INT = NULL,
    @DoctorId INT = NULL,
    @Name NVARCHAR(MAX) = NULL,
    @Code NVARCHAR(MAX) = NULL,
    @SamaNo NVARCHAR(MAX) = NULL,
    @DepartmentId INT =NULL,
    @DepartmentType INT = NULL,
    @SpecialityId INT = NUll
AS
BEGIN

    SET NOCOUNT ON

    SELECT D.*,S.Name as SpecialityName
    FROM [Doctor] D join Speciality S on S.Id=D.SpecialityId
    WHERE
        (@BranchId IS NULL OR D.BranchId=@BranchId) AND
        (@DoctorId IS NULL OR D.Id=@DoctorId) AND
        (@Name IS NULL OR dbo.IncludeInEachOther(D.[Name], @Name)=1) AND
        (@Code IS NULL OR dbo.IncludeInEachOther(Code, @Code)=1) AND
        (@SamaNo IS NULL OR dbo.IncludeInEachOther(SamaNumber, @SamaNo)=1) AND
        (@DepartmentId IS NULL OR EXISTS 
            ( SELECT *
        FROM Schedule AS S
        WHERE S.DoctorId = D.Id
            AND S.DepartmentId=@DepartmentId
            )) AND
        (@DepartmentType IS NULL OR EXISTS 
            ( SELECT *
        FROM Schedule AS S
        JOIN Department DP ON S.DepartmentId=DP.Id
        WHERE S.DoctorId = D.Id
            AND DP.Type=@DepartmentType
            )) AND    
        (@SpecialityId IS NULL OR SpecialityId=@SpecialityId) AND
        D.IsDelete = 0
    ORDER BY Code
END