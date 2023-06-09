USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOperationOrders]    Script Date: 28/3/2023 10:45:04 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetOperationOrders]
	-- Add the parameters for the stored procedure here
	@BranchId INT = NULL,
    @OperationOrderId INT = NULL,
    @PatientId INT = NULL,
    @DoctorId INT = NULL,
    @OTDate DATETIME2(7) = NULL,
	@AdmitDate DATETIME2(7) = NULL,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
	@Status INT = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Od.*, P.Name AS [PatientName],D.Name AS [DoctorName],B.Name as BranchName,OType.Name as OperationTypeName,R.RoomNo as RoomNo FROM OperationOrder Od
		LEFT JOIN Patient P ON Od.PatientId=P.Id
        LEFT JOIN Doctor D ON Od.ChiefSurgeonDoctorId=D.Id 
		LEFT JOIN Branch B ON Od.BranchId=B.Id
		LEFT JOIN OperationType OType On OType.Id=Od.OpeartionTypeId
		LEFT JOIN OperationRoom R on R.Id=Od.OperationRoomId
	WHERE 
		(@BranchId IS NULL OR Od.BranchId=@BranchId) AND
        (@OperationOrderId IS NULL OR Od.Id=@OperationOrderId) AND
        (@PatientId IS NULL OR Od.PatientId=@PatientId) AND
        (@DoctorId IS NULL OR Od.ChiefSurgeonDoctorId=@DoctorId) AND
        (@OTDate IS NULL OR Od.OTDate=@OTDate) AND
		(@AdmitDate IS NULL OR Od.AdmitDate=@AdmitDate) AND
        (@StartDate IS NULL OR Convert(date,OTDate)>=Convert(date,@StartDate)) AND 
        (@EndDate IS NULL OR Convert(date,AdmitDate)<=Convert(date,@EndDate)) AND 
		(@Status IS NULL OR Od.Status=@Status) AND 
        Od.IsDelete = 0
    ORDER BY CreatedAt DESC
END
