USE [thirisandar_hms_demolab]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLabProfiles]    Script Date: 26/1/2023 11:14:36 am ******/
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
	@BranchId INT = NULL,
	@LabProfileId INT = NULL,
	 @Name NVARCHAR(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from LabProfile
		where 
			(@BranchId IS NULL OR BranchId=@BranchId) AND 
            (@LabProfileId IS NULL OR Id=@LabProfileId) AND 
			(@Name IS NULL OR dbo.IncludeInEachOther([Name], @Name)=1) AND
            IsDelete=0  
        ORDER BY Name   


END
