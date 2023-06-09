USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetGroundingItem]    Script Date: 3/14/2022 8:24:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: Mar 09 2022
-- Description: Get Grounding Item by Grounding Id
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetGroundingItem]
	-- Add the parameters for the stored procedure here
	@GroundingId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from GroundingItem where GroundingId=@GroundingId
END
