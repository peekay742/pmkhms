GO
/****** Object:  StoredProcedure [dbo].[SP_GetRooms]    Script Date: 5/9/2022 11:59:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
ALTER PROCEDURE [dbo].[SP_GetOperationRooms]  
    @WardId int=NULL,
	@RoomId int=NULL, 
    @RoomNo NVARCHAR(MAX) = NULL,  
    @Status NVARCHAR(MAX) = NULL                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
AS                                                                                                                                                                                                                                                                                                                                              
BEGIN	 
	SET NOCOUNT ON; 
 
	Select R.*,W.Name As [WardName]  from OperationRoom R LEFT JOIN Ward W ON R.WardId=W.Id
       
	WHERE  
    (@WardId is NULL or R.WardId=@WardId) AND 
    (@RoomId is NULL or R.Id=@RoomId) AND 
    (@RoomNo is NULL or R.RoomNo=@RoomNo) AND 
    (@Status is NULL or R.Status=@Status) AND 
    R.IsDelete=0   
 
END 
