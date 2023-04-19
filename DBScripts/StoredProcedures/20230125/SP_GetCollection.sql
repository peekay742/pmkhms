USE [thirisandar_hms_demolab]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCollection]    Script Date: 25/01/2023 14:35:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Phyo Min Khant
-- Create date: 24/01/2023
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCollection]
	-- Add the parameters for the stored procedure here
	@BranchId INT = NULL,
	@Id int=null,
	@Name nvarchar(MAX)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Collection where 
	(@BranchId IS NULL OR BranchId=@BranchId) AND
	(@Id is  null Or Id=@Id) and
	(@Name is  null or Name=@Name) and 
	IsDelete=0
END
