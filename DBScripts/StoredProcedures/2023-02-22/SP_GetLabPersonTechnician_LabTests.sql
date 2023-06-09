USE [thirisandar_hms_demolab]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabPersonTechnician_LabTests]    Script Date: 22/02/2023 10:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Phyo Min Khant
-- Create Date: May 07, 2022
-- Description: Get All LabPerson_LabTests
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabPersonTechnician_LabTests]
    @BranchId INT = NULL,
	@Id int=null,
	@DepartmentId int=null,
	@Name nvarchar(MAX)=null
AS
BEGIN

    SET NOCOUNT ON

    SELECT P.*, DP.Name AS [DepartmentName], D.Name AS [DoctorName]
    FROM LabPerson P
    LEFT JOIN Department DP ON P.DepartmentId=DP.Id
    LEFT JOIN Doctor D ON P.DoctorId=D.Id
    WHERE   
        (@BranchId IS NULL OR P.BranchId=@BranchId) AND
        (@DepartmentId IS NULL OR DepartmentId=@DepartmentId) AND
        (@Id IS NULL OR P.Id=@Id) and
		P.Type=1 and
        P.IsDelete=0
    ORDER BY P.[Name]
		
END