-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE SP_GetAneathetistDoctor
	@BranchId INT = NULL,
   @IsAnaesthetist bit
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
         ((@IsAnaesthetist=0 and S.Name<>'Anaesthetist') or (@IsAnaesthetist=1 and S.Name='Anaesthetist')) And
        D.IsDelete = 0 
    ORDER BY Code
END
GO
