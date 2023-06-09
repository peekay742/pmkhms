
/****** Object:  StoredProcedure [dbo].[SP_GetRoom]    Script Date: 4/21/2022 11:29:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
ALTER PROCEDURE [dbo].[SP_GetRooms]  
    @WardId int=NULL, 
	@RoomTypeId int=NULL, 
	@RoomId int=NULL, 
    @RoomNo NVARCHAR(MAX) = NULL,  
    @Status NVARCHAR(MAX) = NULL                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
AS                                                                                                                                                                                                                                                                                                                                              
BEGIN	 
	SET NOCOUNT ON; 
 
	Select R.*,W.Name As [WardName],RT.Name As [RoomTypeName] from Room R LEFT JOIN Ward W ON R.WardId=W.Id
        LEFT JOIN RoomType RT ON R.RoomTypeId=RT.Id
	WHERE  
    (@WardId is NULL or R.WardId=@WardId) AND 
	(@RoomTypeId is NULL or R.RoomTypeId=@RoomTypeId) AND 
    (@RoomId is NULL or R.Id=@RoomId) AND 
    (@RoomNo is NULL or R.RoomNo=@RoomNo) AND 
    (@Status is NULL or R.Status=@Status) AND 
    R.IsDelete=0   
 
END 
