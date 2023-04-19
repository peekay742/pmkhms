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
-- Author:		Zun Pwint Phyu
-- Create date: 2022,Aug 26
-- Description:	Get Item by OutletId
-- =============================================
Alter PROCEDURE SP_GetItemsByOutletId 
	-- Add the parameters for the stored procedure here
	@BranchId int=null,
	@OutletId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select I.* from Item I join OutletItem OI on OI.ItemId=I.Id
	join Outlet O on O.Id=OI.OutletId
	where OI.OutletId=@OutletId 
END
GO
