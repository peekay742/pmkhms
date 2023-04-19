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
CREATE PROCEDURE SP_GetOutletAnaesthetistItems
	-- Add the parameters for the stored procedure here
	@BranchId INT = NULL,
    @OutletId INT = NULL,
    @ItemId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT OI.*, O.Name AS [OutletName], I.Name AS [ItemName], I.Code AS [ItemCode], O.BranchId, B.Name AS [BranchName] FROM OutletItem OI
        JOIN Outlet O On O.Id=OI.OutletId
        JOIN Branch B ON O.BranchId = B.Id
        JOIN Item I ON OI.ItemId = I.Id
        WHERE 
            ((@BranchId IS NOT NULL AND O.BranchId=@BranchId) OR 
            (@BranchId IS NULL AND WarehouseId IS NOT NULL)) AND
            ((@OutletId IS NOT NULL AND OutletId=@OutletId) OR 
            (@OutletId IS NULL AND OutletId IS NOT NULL)) AND
            ((@ItemId IS NOT NULL AND OI.ItemId=@ItemId) OR 
            (@ItemId IS NULL AND OI.ItemId IS NOT NULL)) AND          
            O.IsDelete = 0 and I.IsDelete=0 and I.Category = 3
        ORDER BY B.Code, O.Code, I.Name
END
GO
