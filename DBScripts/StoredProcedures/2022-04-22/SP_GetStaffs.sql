
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStaffs]    Script Date: 4/22/2022 9:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 07_Feb_2022
-- Description:	Get Staffs
--=============================================
ALTER PROCEDURE [dbo].[SP_GetStaffs] 
	-- Add the parameters for the stored procedure here
    @BranchId int=null,
	@StaffId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select S.*,P.Name As [PositionName],D.Name As [DepartmentName] from Staff S 
	JOIN Position P ON S.PositionId=P.Id
	JOIN Department D ON S.DepartmentId=D.Id
	where  
    (@BranchId is null or S.BranchId=@BranchId) and
    (@StaffId is null or S.Id=@StaffId)and 
	S.IsDelete=0
END

