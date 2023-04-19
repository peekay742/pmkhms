-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE SP_GetLabOrderTestbyLaborderId
	-- Add the parameters for the stored procedure here
@LabOrderId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select LT.*,T.Name as LabTestName,R.Name as ReferrerName from LabOrderTest LT join LabTest T on LT.LabTestId=T.Id
	left join Referrer R on R.Id=LT.ReferrerId where LT.LabOrderId=@LabOrderId
END
GO
