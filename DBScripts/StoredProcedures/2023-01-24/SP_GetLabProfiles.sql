USE [thirisandar_hms_demotesting]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabProfiles]    Script Date: 24/1/2023 1:20:22 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetLabProfiles]
	-- Add the parameters for the stored procedure here
	@BranchId INT = null,
	@LabProfileId INT = null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from LabProfile 
		Where 
			(@BranchId IS NULL OR BranchId=@BranchId) AND 
			(@LabProfileId IS NULL OR Id=@LabProfileId) AND 
			IsDelete = 0
		ORDER BY Name
END
