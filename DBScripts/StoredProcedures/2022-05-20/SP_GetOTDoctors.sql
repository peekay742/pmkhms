/****** Object:  StoredProcedure [dbo].[SP_GetOTDoctors]    Script Date: 5/20/2022 3:04:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: May 19, 2022
-- Description:	Get OT Doctor by OperationTreaterId
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetOTDoctors]
	@OperationTreaterId int,
	@DoctorId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select OTD.*,D.Name as DoctorName from OT_Doctor OTD join Doctor D on OTD.DoctorId=D.Id
	where OperationTreaterId=@OperationTreaterId AND
	 (@DoctorId IS NULL OR DoctorId=@DoctorId) 
END
