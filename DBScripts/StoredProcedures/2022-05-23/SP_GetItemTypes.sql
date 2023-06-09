/****** Object:  StoredProcedure [dbo].[SP_GetItemTypes]    Script Date: 5/23/2022 9:42:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetItemTypes]
    @BranchId INT = NULL,
    @ItemTypeId INT = NULL

AS
BEGIN

    SET NOCOUNT ON

    SELECT * FROM ItemType 
        WHERE 
            (@BranchId IS NULL Or BranchId=@BranchId) And 
            
            (@ItemTypeId IS NULL OR Id=@ItemTypeId) AND
            IsDelete = 0
        ORDER BY Name
END