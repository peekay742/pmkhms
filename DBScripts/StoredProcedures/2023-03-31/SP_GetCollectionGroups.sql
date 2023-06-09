USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCollectionGroups]    Script Date: 3/31/2023 3:41:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCollectionGroups]
	-- Add the parameters for the stored procedure here
	@BranchId INT = NULL,
	@CollectionGroupId INT = NULL,
	@Name NVARCHAR(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM CollectionGroup
	Where 
	(@BranchId IS NULL OR BranchId=@BranchId) AND
	(@CollectionGroupId IS NULL OR Id=@CollectionGroupId) AND
	(@Name IS NULL OR Name=@Name) AND
	IsDelete=0
END
