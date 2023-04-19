USE [thirisandar_hms_dev]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDischargeTypes]    Script Date: 12/04/2023 12:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetDischargeTypes]
    @BranchId INT = NULL,
    @DischargeTypeId INT = NULL

AS
BEGIN

    SET NOCOUNT ON

    SELECT * FROM DischargeType 
        WHERE 
            (@BranchId IS NULL Or BranchId=@BranchId) And 
            
            (@DischargeTypeId IS NULL OR Id=@DischargeTypeId) AND
            IsDelete = 0
        ORDER BY Name
END