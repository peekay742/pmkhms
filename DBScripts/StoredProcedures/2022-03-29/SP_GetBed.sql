USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBed]    Script Date: 3/29/2022 3:49:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[SP_GetBed]   
 @RoomId int=NULL,  
 @BedTypeId int=NULL,  
 @BedId int=NULL,
 @BedNo NVARCHAR(MAX) = NULL,
 @BedStatus NVARCHAR(MAX) = NULL
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 Select * from Bed where  
    (@RoomId is NULL or RoomId=@RoomId) AND  
	(@BedTypeId is NULL or BedTypeId=@BedTypeId) AND  
    (@BedId is NULL or Id=@BedId) AND 
	(@BedStatus is NULL or Status=@BedNo) AND
	(@BedNo IS NULL OR (No LIKE '%' + @BedNo + '%' OR @BedNo LIKE '%' + No + '%')) AND  IsDelete=0
END  
