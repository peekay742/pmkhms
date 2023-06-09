/****** Object:  StoredProcedure [dbo].[SP_GetOperationRooms]    Script Date: 5/20/2022 3:03:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
ALTER PROCEDURE [dbo].[SP_GetOperationRooms]  
    @WardId int=NULL,
	@OperationRoomId int=NULL, 
    @RoomNo NVARCHAR(MAX) = NULL,  
    @Status NVARCHAR(MAX) = NULL
AS                                                                                                                                                                                                                                                                                                                                              
BEGIN	 
	SET NOCOUNT ON; 
 
	Select R.*,W.Name As [WardName]  from OperationRoom R JOIN Ward W ON R.WardId=W.Id
       
	WHERE  
    (@WardId is NULL or R.WardId=@WardId) AND 
    (@OperationRoomId is NULL or R.Id=@OperationRoomId) AND 
    (@RoomNo is NULL or R.RoomNo=@RoomNo) AND 
    (@Status is NULL or R.Status=@Status) And
    R.IsDelete=0   
 
END 
