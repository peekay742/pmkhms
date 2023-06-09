
/****** Object:  StoredProcedure [dbo].[SP_GetStaffs]    Script Date: 2/8/2022 9:03:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 07_Feb_2022
-- Description:	Get Staffs
--=============================================
CREATE PROCEDURE [dbo].[SP_GetStaffs] 
	-- Add the parameters for the stored procedure here
	@StaffId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Staff where IsDelete=0 and ((@StaffId is not null and Id=@StaffId) or (@StaffId is null and Id is not null))
END
