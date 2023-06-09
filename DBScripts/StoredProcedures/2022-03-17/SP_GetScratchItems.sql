
GO
/****** Object:  StoredProcedure [dbo].[SP_GetScratchItems]    Script Date: 3/17/2022 9:21:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Zun Pwint Phyu
-- Create date: Feb 25,2022
-- Description: Get Scratchs
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetScratchItems] 
   @ScratchId INT = NULL, 
    @ItemId INT = NULL, 
    @UnitId INT = NULL, 
    @BatchId INT = NULL 
AS 
BEGIN 
 
    SET NOCOUNT ON 
 
    SELECT * FROM ScratchItem  
        WHERE  
            ((@ScratchId IS NOT NULL AND ScratchId=@ScratchId) OR  
            (@ScratchId IS NULL AND ScratchId IS NOT NULL)) AND 
            ((@ItemId IS NOT NULL AND ItemId=@ItemId) OR  
            (@ItemId IS NULL AND ItemId IS NOT NULL)) AND 
            ((@UnitId IS NOT NULL AND UnitId=@UnitId) OR  
            (@UnitId IS NULL AND UnitId IS NOT NULL)) AND 
            ((@BatchId IS NOT NULL AND BatchId=@BatchId) OR  
            (@BatchId IS NULL AND BatchId IS NOT NULL)) 
        ORDER BY SortOrder 
END
