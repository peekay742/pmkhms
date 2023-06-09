/****** Object:  StoredProcedure [dbo].[SP_GetImgOrderTestbyLaborderId]    Script Date: 10/26/2022 10:34:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetImgOrderTestbyLaborderId]
	-- Add the parameters for the stored procedure here
@ImgOrderId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select LT.*,T.Name as LabTestName,R.Name as ReferrerName from ImagingOrderTest LT join LabTest T on LT.LabTestId=T.Id
	left join Referrer R on R.Id=LT.ReferrerId
	where LT.ImagingOrderId=@ImgOrderId
END
