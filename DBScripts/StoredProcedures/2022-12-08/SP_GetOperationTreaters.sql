USE [thirisandar_hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOperationTreaters]    Script Date: 08/12/2022 13:43:47 ******/
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
    @PaidDate DATETIME2(7) = NULL,
	@BarCode nvarchar(MAX)=null,
	@QRCode nvarchar(MAX)=null
AS
BEGIN

    SET NOCOUNT ON

    SELECT O.*, P.Name AS [PatientName],IIF(P.DateOfBirth is null,P.AgeYear,(YEAR(CURRENT_TIMESTAMP)-YEAR(P.DateOfBirth))) As PatientAge,P.Gender As PatientGender,  D.Name AS [DoctorName],D1.Name As[AneasthetistName],B.Name as BranchName,B.Address,B.Phone,OType.Name as OperationTypeName,R.RoomNo as RoomNo,Rf.Name ReferrerName,R.Price as RoomPrice  FROM OperationTreater O
        LEFT JOIN Patient P ON O.PatientId=P.Id
        LEFT JOIN Doctor D ON O.ChiefSurgeonDoctorId=D.Id 
		LEFT JOIN Doctor D1 ON O.AneasthetistDoctorId=D1.Id
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
            (@StartDate IS NULL OR Convert(date,OperationDate)>=Convert(date,@StartDate)) AND 
            (@EndDate IS NULL OR Convert(date,OperationDate)<=Convert(date,@EndDate)) AND 
			(@BarCode IS NULL OR P.BarCode=@BarCode) AND
			(@QRCode IS NULL OR P.QRCode=@QRCode) AND
            O.IsDelete = 0
        ORDER BY CreatedAt DESC
END