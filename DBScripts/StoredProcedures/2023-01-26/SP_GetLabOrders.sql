USE [thirisandar_hms_demolab]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabOrders]    Script Date: 26/01/2023 15:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aung Naing OO
-- Create Date: May 13, 2022
-- Description: Get All LabOrders
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabOrders]
    @BranchId INT = NULL,
    @LabOrderId INT = NULL,
    @PatientId INT = NULL,
    @VoucherNo NVARCHAR(MAX) = NULL,
    @IsPaid BIT = NULL,
    @Date DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @PaidDate DATETIME2(7) = NULL,
    @StartPaidDate DATETIME2(7) = NULL,
    @EndPaidDate DATETIME2(7) = NULL,
	@TestId INT=null,
	@BarCode nvarchar(MAX)=null,
	@QRCode nvarchar(MAX)=null,
	@GetCollection BIT = NULL,
	@GetCollectionDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT distinct O.*, P.Name AS [PatientName], P.Guardian AS [PatientGuardian], B.Name as BranchName,B.Address AS [BranchAddress],B.Phone AS [BranchPhone],null as isCompleteResult,null as labResultId   FROM [LabOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN LabOrderTest LT ON LT.LabOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND
            (@LabOrderId IS NULL OR O.Id=@LabOrderId) AND
            (@PatientId IS NULL OR PatientId=@PatientId) AND
            (@VoucherNo IS NULL OR (VoucherNo LIKE '%' + @VoucherNo + '%' OR @VoucherNo LIKE '%' + VoucherNo + '%')) AND
            (@Date IS NULL OR [Date]=@Date) AND
			(@IsPaid IS NULL OR IsPaid=@IsPaid)AND
            (@StartDate IS NULL OR [Date]>=@StartDate) AND 
            (@EndDate IS NULL OR [Date]<=@EndDate) AND 
            (@PaidDate IS NULL OR [PaidDate]=@Date) AND
            (@StartPaidDate IS NULL OR [PaidDate]>=@StartDate) AND 
            (@EndPaidDate IS NULL OR [PaidDate]<=@EndDate) AND 
			(@TestId IS NULL OR LT.LabTestId=@TestId) And
			(@BarCode IS NULL OR P.BarCode=@BarCode) AND
			(@QRCode IS NULL OR P.QRCode=@QRCode) AND
			(@GetCollection IS NULL OR GetCollection=@GetCollection)AND
			(@GetCollectionDate IS NULL OR [GetCollectionDate]=@GetCollectionDate) AND
            O.IsDelete = 0
        ORDER BY [VoucherNo] DESC
END