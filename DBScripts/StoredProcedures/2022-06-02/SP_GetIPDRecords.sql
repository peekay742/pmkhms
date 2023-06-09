/****** Object:  StoredProcedure [dbo].[SP_GetIPDRecords]    Script Date: 6/3/2022 10:33:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	@TreatmentProcess int=null
AS  
BEGIN  
  
    SET NOCOUNT ON  
  
	Select * from IPDRecord 
        WHERE   
		    (@BranchId IS NULL OR BranchId=@BranchId) AND
            (@IPDRecordId IS NULL OR Id=@IPDRecordId) AND 			
            (@Doa IS NULL OR Convert(date,DOA)=Convert(date,@Doa)) AND  
            (@Dodc IS NULL OR Convert(date,DODC)=Convert(date,@Dodc)) AND  			
			(@PatientId IS NULL OR PatientId=@PatientId) AND  
			(@IsPaid IS NULL OR IsPaid=@IsPaid) AND
			(@PaidDate IS NULL OR [PaidDate]=@PaidDate) AND  
			(@PaymentType IS NULL OR PaymentType=@PaymentType) AND			
			(@Status IS NULL OR [Status]=@Status) AND						
			(@RoomId IS NULL OR RoomId=@RoomId) AND
			(@BedId IS NULL OR BedId=@BedId) AND
			(@Remark IS NULL OR Remark=@Remark) AND						
			(@Discount IS NULL OR Discount=@Discount) AND			
			(@Tax IS NULL OR Tax=@Tax) AND
			(@DepartmentId IS NULL OR DepartmentId=@DepartmentId) AND
			(@VoucherNo IS NULL OR (VoucherNo LIKE '%' + @VoucherNo + '%' OR @VoucherNo LIKE '%' + VoucherNo + '%')) AND       
			(@TreatmentProcess IS NULL OR  IPDStatusEnum=@TreatmentProcess) And
            IsDelete = 0  
			ORDER BY [VoucherNo] DESC  
END