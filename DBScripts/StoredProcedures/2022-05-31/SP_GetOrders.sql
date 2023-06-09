/****** Object:  StoredProcedure [dbo].[SP_GetOrders]    Script Date: 5/31/2022 10:44:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aung Naing OO
-- Create Date: Feb 25, 2022
-- Description: Get All Orders
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetOrders]
    @BranchId INT = NULL,
    @OutletId INT = NULL,
    @OrderId INT = NULL,
    @PatientId INT = NULL,
    @DoctorId INT = NULL,
    @VoucherNo NVARCHAR(MAX) = NULL,
    @IsPaid BIT = NULL,
    @Date DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @PaidDate DATETIME2(7) = NULL,
    @StartPaidDate DATETIME2(7) = NULL,
    @EndPaidDate DATETIME2(7) = NULL
AS
BEGIN

    SET NOCOUNT ON

    SELECT O.*, P.Name AS [PatientName], P.Guardian AS [PatientGuardian], D.Name AS [DoctorName],B.Name as BranchName,B.Address,B.Phone,ol.Name as OutletName,R.Fee as ReferrerFee  FROM [Order] O
        LEFT JOIN Patient P ON O.PatientId=P.Id
        LEFT JOIN Doctor D ON O.DoctorId=D.Id
		LEFT JOIN Branch B ON O.BranchId=B.Id
		LEFT JOIN Outlet ol ON ol.Id=O.OutletId
		LEFT JOIN OrderService OS ON OS.OrderId=O.Id
		LEFT JOIN Referrer R ON R.Id=OS.ReferrerId
        WHERE 
            (@BranchId IS NULL OR O.BranchId=@BranchId) AND
            (@OutletId IS NULL OR OutletId=@OutletId) AND
            (@OrderId IS NULL OR O.Id=@OrderId) AND
            (@PatientId IS NULL OR PatientId=@PatientId) AND
            (@DoctorId IS NULL OR O.DoctorId=@DoctorId) AND
            (@VoucherNo IS NULL OR (VoucherNo LIKE '%' + @VoucherNo + '%' OR @VoucherNo LIKE '%' + VoucherNo + '%')) AND
            (@Date IS NULL OR [Date]=@Date) AND
            (@StartDate IS NULL OR [Date]>=@StartDate) AND 
            (@EndDate IS NULL OR [Date]<=@EndDate) AND 
            (@PaidDate IS NULL OR [PaidDate]=@Date) AND
            (@StartPaidDate IS NULL OR [PaidDate]>=@StartDate) AND 
            (@EndPaidDate IS NULL OR [PaidDate]<=@EndDate) AND 
			(@IsPaid IS NULL OR IsPaid=@IsPaid) AND
            O.IsDelete = 0
        ORDER BY [VoucherNo] DESC
END