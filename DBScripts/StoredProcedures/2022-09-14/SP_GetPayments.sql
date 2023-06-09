/****** Object:  StoredProcedure [dbo].[SP_GetPayments]    Script Date: 9/14/2022 11:59:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetPayments]
	-- Add the parameters for the stored procedure here
	@RecordId int=null,
	@Id int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select *,'' as PatientName,'' as PaymentStatus from IPDPayment  where
	(@RecordId is null or IPDRecordId=@RecordId) and
	(@Id is null or Id=@Id) and IsDelete=0
END