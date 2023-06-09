USE [thirisandar_hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetImgOrders]    Script Date: 08/12/2022 14:50:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Zun Pwint Phyu
-- Create Date: Sep 27, 2022
-- Description: Get All ImagingOrders
-- =============================================
-- =============================================
-- Author:     Phyo Min Khant
-- Create Date: Dec 8, 2022
-- Description: Get All ImagingOrders
-- Description: Update QR and Bar Code Search
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetImgOrders]
    @BranchId INT = NULL,
	@BarCode nvarchar(MAX)=null,
	@QRCode nvarchar(MAX)=null,
    @ImagingOrderId INT = NULL,
    @PatientId INT = NULL,
    @VoucherNo NVARCHAR(MAX) = NULL,
    @IsPaid BIT = NULL,
    @Date DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @PaidDate DATETIME2(7) = NULL,
    @StartPaidDate DATETIME2(7) = NULL,
    @EndPaidDate DATETIME2(7) = NULL,
	@TestId INT=null
AS
BEGIN

    SET NOCOUNT ON

    SELECT distinct O.*, P.Name AS [PatientName], P.Guardian AS [PatientGuardian], B.Name as BranchName,B.Address AS [BranchAddress],B.Phone AS [BranchPhone],null as isCompleteResult,null as imagingResultId  FROM [ImagingOrder] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN ImagingOrderTest LT ON LT.ImagingOrderId=O.Id
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND
            (@ImagingOrderId IS NULL OR O.Id=@ImagingOrderId) AND
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
            O.IsDelete = 0
        ORDER BY [VoucherNo] DESC
END