/****** Object:  StoredProcedure [dbo].[SP_GetIPDPayemntUnderPercent]    Script Date: 9/15/2022 1:36:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetIPDPayemntUnderPercent]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select P.*,Pa.Name as PatientName,'' as PaymentStatus from IPDPayment P join IPDRecord R on R.Id=P.IPDRecordId join Patient Pa on Pa.Id=R.PatientId where AlertAmount>=Amount and P.IsActive=1
END
