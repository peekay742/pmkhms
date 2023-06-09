USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetFood]    Script Date: 4/1/2022 11:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[SP_GetFoods]   
 @FoodCategoryId int=NULL,    
 @FoodId int=NULL,
 @Name NVARCHAR(MAX) = NULL,
 @UnitPrice int=NULL,
 @Code NVARCHAR(MAX)=NULL,
 @Description NVARCHAR(MAX)=NULL


AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 Select * from Food where  
    (@FoodCategoryId is NULL OR FoodCategoryId=@FoodCategoryId) AND  
    (@FoodId is NULL OR Id=@FoodId) AND
	(@UnitPrice is NULL OR UnitPrice=@UnitPrice)AND
	(@Code is NULL OR Code=@Code)AND
	(@Description is NULL OR Description=@Description)AND
	(@Name IS NULL OR (Name LIKE '%' + @Name + '%' OR @Name LIKE '%' + Name + '%')) AND
            IsDelete=0  
        ORDER BY Name
END  

