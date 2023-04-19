USE [thirisandar_hms_demotesting]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetIPDRecords]    Script Date: 11/01/2023 22:15:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Phyo Min Khant
-- Create Date: Dec 08, 2022
-- Description: Get All LabOrders
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetIPDRecordsDischarge]  
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
	@DiseaseName nvarchar(MAX)=null,
	@DiseaseSummary nvarchar(MAX)=null,
	@PhotographicExaminationAnswer nvarchar(MAX)=null,
	@MedicalTreatment nvarchar(MAX)=null
AS  
BEGIN  
  
    SET NOCOUNT ON  
  
	Select * from IPDRecord IPD
		LEFT JOIN Patient P ON IPD.PatientId=P.Id
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
			(@RoomId IS NULL OR RoomId=@RoomId) AND
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
			(@DiseaseName IS NULL OR DiseaseName=@DiseaseName) AND
			(@DiseaseSummary IS NULL OR DiseaseSummary=@DiseaseSummary) AND
			(@PhotographicExaminationAnswer IS NULL OR PhotographicExaminationAnswer=@PhotographicExaminationAnswer) AND
			(@MedicalTreatment IS NULL OR MedicalTreatment=@MedicalTreatment) AND
            IPD.IsDelete = 0  and P.IsDelete=0
			ORDER BY [VoucherNo] DESC  
END