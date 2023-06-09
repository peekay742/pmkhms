/****** Object:  StoredProcedure [dbo].[SP_GetOperationTreaters]    Script Date: 5/25/2022 10:24:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: May 12, 2022
-- Description: Get All Operation Treater
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetOperationTreaters]
    @BranchId INT = NULL,
    @OutletId INT = NULL,
    @OperationTreaterId INT = NULL,
    @PatientId INT = NULL,
    @DoctorId INT = NULL,
    @IsPaid BIT = NULL,
    @Date DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @PaidDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT O.*, P.Name AS [PatientName],  D.Name AS [DoctorName],B.Name as BranchName,B.Address,B.Phone,OType.Name as OperationTypeName,R.RoomNo as RoomNo,Rf.Name ReferrerName  FROM OperationTreater O
        LEFT JOIN Patient P ON O.PatientId=P.Id
        LEFT JOIN Doctor D ON O.ChiefSurgeonDoctorId=D.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN OperationType OType On OType.Id=O.OpeartionTypeId
		LEFT JOIN OperationRoom R on R.Id=O.OperationRoomId
		LEFT JOIN Referrer Rf on Rf.Id=O.ReferrerId
		
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND
            (@OutletId IS NULL OR OutletId=@OutletId) AND
            (@OperationTreaterId IS NULL OR O.Id=@OperationTreaterId) AND
            (@PatientId IS NULL OR PatientId=@PatientId) AND
            (@DoctorId IS NULL OR O.ChiefSurgeonDoctorId=@DoctorId) AND
            (@Date IS NULL OR OperationDate=@Date) AND
            (@StartDate IS NULL OR OperationDate>=@StartDate) AND 
            (@EndDate IS NULL OR OperationDate<=@EndDate) AND 
			(@IsPaid IS NULL OR IsPaid=@IsPaid) AND
            O.IsDelete = 0
        ORDER BY CreatedAt DESC
END