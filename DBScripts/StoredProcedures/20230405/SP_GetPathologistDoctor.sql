USE [thirisandardb_demo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPathologistDoctor]    Script Date: 4/5/2023 1:20:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Htet Wai Aung
-- Create date: April 05,2023
-- Description:	GetPathologistDoctor
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetPathologistDoctor]
	-- Add the parameters for the stored procedure here
	@BranchId INT = NULL,
	@IsPathologist BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.*,S.Name as SpecialityName
    FROM [Doctor] D join Speciality S on S.Id=D.SpecialityId
	WHERE
		(@BranchId IS NULL OR D.BranchId=@BranchId) AND
        ((@IsPathologist=0 and S.Name<>'Pathologist') or (@IsPathologist=1 and S.Name='Pathologist')) And
        D.IsDelete = 0 
		ORDER BY Code
END
