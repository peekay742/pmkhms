USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetIPDRecords]    Script Date: 1/3/2023 9:38:18 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Phyo Min Khant
-- Create Date: Dec 08, 2022
-- Description: Get All LabOrders
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetIPDRecords]  
	@BranchId Int=null,
    @IPDRecordId INT = NULL,
    @Doa DATETIME2(7) = NULL,  
    @Dodc DATETIME2(7) = NULL,
	@PatientId INT = NULL,  
	@IsPaid INT = NULL,
	@PaidDate DATETIME2(7) = NULL,
	@PaymentType INT = NULL,
	@Status NVARCHAR(MAX) = NULL,
	@RoomId INT = NULL,
	@BedId INT = NULL,
	@Remark NVARCHAR(MAX) = NULL,  
	@Discount INT = NULL,
	@Tax INT = NULL,
	@DepartmentId INT = NULL,
	@VoucherNo NVARCHAR(MAX) = NULL,
	@TreatmentProcess int=null,
	@StartDate DATETIME2(7)=null,
	@EndDate DATETIME2(7)=null,
	@BarCode nvarchar(MAX)=null,
	@QRCode nvarchar(MAX)=null,
	@MedicalOfficer nvarchar(MAX) = null, 
	@Service nvarchar(MAX)= null,
	@AdmittedFor nvarchar(MAX) = null
AS  
BEGIN  
  
    SET NOCOUNT ON  
  
	Select IPD.*,P.Name As [PatientName],R.RoomNo As [RoomNo],B.No As [BedNo],D.Name As [DepartmentName] from IPDRecord IPD
		LEFT JOIN Patient P ON IPD.PatientId=P.Id
		LEFT JOIN Room R ON IPD.RoomId=R.Id
		LEFT JOIN Bed B ON IPD.BedId=B.Id
		LEFT JOIN Department D ON IPD.DepartmentId=D.Id
        WHERE   
		    (@BranchId IS NULL OR IPD.BranchId=@BranchId) AND
            (@IPDRecordId IS NULL OR IPD.Id=@IPDRecordId) AND 			
            (@Doa IS NULL OR Convert(date,DOA)=Convert(date,@Doa)) AND  
            (@Dodc IS NULL OR Convert(date,DODC)=Convert(date,@Dodc)) AND  			
			(@PatientId IS NULL OR PatientId=@PatientId) AND  
			(@IsPaid IS NULL OR IsPaid=@IsPaid) AND
			(@PaidDate IS NULL OR [PaidDate]=@PaidDate) AND  
			(@PaymentType IS NULL OR PaymentType=@PaymentType) AND			
			(@Status IS NULL OR IPD.[Status]=@Status) AND						
			(@RoomId IS NULL OR IPD.RoomId=@RoomId) AND
			(@BedId IS NULL OR BedId=@BedId) AND
			(@Remark IS NULL OR Remark=@Remark) AND						
			(@Discount IS NULL OR Discount=@Discount) AND			
			(@Tax IS NULL OR Tax=@Tax) AND
			(@DepartmentId IS NULL OR DepartmentId=@DepartmentId) AND
			(@VoucherNo IS NULL OR (VoucherNo LIKE '%' + @VoucherNo + '%' OR @VoucherNo LIKE '%' + VoucherNo + '%')) AND       
			(@TreatmentProcess IS NULL OR  IPDStatusEnum=@TreatmentProcess) And
			(@StartDate IS NULL OR Convert(date,DOA)>=Convert(date,@StartDate)) And
			(@EndDate IS NULL OR Convert(date,DOA)<=Convert(date,@EndDate)) And
			(@BarCode IS NULL OR P.BarCode=@BarCode) AND
			(@QRCode IS NULL OR P.QRCode=@QRCode) AND
			(@MedicalOfficer IS NULL OR MedicalOfficer=@MedicalOfficer) AND
			(@Service IS NULL OR IPD.service=@Service) AND	
			(@AdmittedFor IS NULL OR AdmittedFor=@AdmittedFor) AND	
            IPD.IsDelete = 0  and P.IsDelete=0
			ORDER BY [VoucherNo] DESC  
END