/****** Object:  StoredProcedure [dbo].[SP_GetOTStaffs]    Script Date: 5/20/2022 3:04:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: May 19, 2022
-- Description:	Get OT Staff by OperationTreaterId
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetOTStaffs]
	@OperationTreaterId int,
	@StaffId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select OTS.*,S.Name as StaffName from OT_Staff OTS join Staff S on OTS.StaffId=S.Id
	where OperationTreaterId=@OperationTreaterId AND
	 (@StaffId IS NULL OR OTS.StaffId=@StaffId) 
END
