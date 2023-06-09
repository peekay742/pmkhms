
GO
/****** Object:  StoredProcedure [dbo].[SP_GetFoods]    Script Date: 4/21/2022 11:03:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[SP_GetFoods]   
 @FoodCategoryId int=NULL,    
 @FoodId int=NULL,
 @Name NVARCHAR(MAX) = NULL,
 @UnitPrice int=NULL,
 @Code NVARCHAR(MAX)=NULL,
 @Description NVARCHAR(MAX)=NULL


AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 Select F.*,FC.Name As [FoodCategoryName] from Food F JOIN FoodCategory FC ON F.FoodCategoryId=FC.Id where  
    (@FoodCategoryId is NULL OR F.FoodCategoryId=@FoodCategoryId) AND  
    (@FoodId is NULL OR F.Id=@FoodId) AND
	(@UnitPrice is NULL OR F.UnitPrice=@UnitPrice)AND
	(@Code is NULL OR F.Code=@Code)AND
	(@Description is NULL OR F.Description=@Description)AND
	(@Name IS NULL OR (F.Name LIKE '%' + @Name + '%' OR @Name LIKE '%' + F.Name + '%')) AND
            F.IsDelete=0  
        ORDER BY Name
END  

