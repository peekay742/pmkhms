USE [msis_hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLocations]    Script Date: 2/11/2022 1:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: 04_Feb_2022
-- Description:	Get Locations
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetLocations]
	-- Add the parameters for the stored procedure here
	@BranchId INT = NULL,
    @LocationName NVARCHAR(MAX) = NULL,
    @LocationCode NVARCHAR(MAX) = NULL,
    @WarehouseId NVARCHAR(MAX) = NULL,
	@LocationId Int=NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Location where IsDelete=0 
	and ((@LocationId is not null and Id=@LocationId) or (@LocationId is null and Id is not null))
	and ((@BranchId is not null and BranchId=@BranchId) or (@BranchId is null and BranchId is not null))
	and ((@LocationName is not null and Name like '%'+@LocationName+'%') or (@LocationName is null and Name is not null))
	and ((@LocationCode is not null and Code like '%'+@LocationCode+'%') or (@LocationCode is null and Code is not null))
	and ((@WarehouseId is not null and WarehouseId=@WarehouseId) or (@WarehouseId is null and WarehouseId is not null))
END
