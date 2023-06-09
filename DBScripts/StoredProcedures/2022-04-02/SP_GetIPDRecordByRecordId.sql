USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetIPDRecordByRecordId]    Script Date: 4/2/2022 12:20:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 01 April,2022
-- Description: Get IPDRecord data by RecordId
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetIPDRecordByRecordId]
	@RecordId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 select R.VoucherNo as VoucherNo,P.Id as PatientId,P.Name as PatientName,P.Guardian as Guardian,(YEAR(GETDATE())-P.AgeYear) as Age,R.PaymentType,null as PaymentTypeName,R.Status as [Status],R.DOA ,R.DODC from IPDRecord R
	 join Patient P on P.Id=R.PatientId
	 where R.IsDelete=0 and R.Id=@RecordId
END
