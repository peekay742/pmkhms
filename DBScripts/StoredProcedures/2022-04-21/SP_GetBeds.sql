
/****** Object:  StoredProcedure [dbo].[SP_GetBed]    Script Date: 4/21/2022 10:01:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[SP_GetBeds]     
 @RoomId int=NULL,    
 @BedTypeId int=NULL,    
 @BedId int=NULL,  
 @BedNo NVARCHAR(MAX) = NULL,  
 @BedStatus NVARCHAR(MAX) = NULL  
AS    
BEGIN    
    
 SET NOCOUNT ON;    
    
 Select B.*,BT.Name As [BedTypeName],R.RoomNo from Bed B LEFT JOIN BedType BT ON B.BedTypeId=BT.Id
        LEFT JOIN Room R ON B.RoomId=R.Id
 where    
    (@RoomId is NULL or B.RoomId=@RoomId) AND    
 (@BedTypeId is NULL or B.BedTypeId=@BedTypeId) AND    
    (@BedId is NULL or B.Id=@BedId) AND   
 (@BedStatus is NULL or B.Status=@BedNo) AND  
 (@BedNo IS NULL OR (B.No LIKE '%' + @BedNo + '%' OR @BedNo LIKE '%' + B.No + '%')) AND  B.IsDelete=0  
END 