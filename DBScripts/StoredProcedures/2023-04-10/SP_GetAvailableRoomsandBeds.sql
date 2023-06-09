USE [thirisandar_hms_dev]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAvailableRoomsandBeds]    Script Date: 10/04/2023 13:34:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_GetAvailableRoomsandBeds]      
@FloorId INT = NULL           
AS      
BEGIN          
SET NOCOUNT ON  
  
select r.*,w.Name as WardName,rt.Name as RoomTypeName from Room r   
join Ward w on w.Id=r.WardId   
join Floor f on f.Id=w.FloorId  
join Department d on w.DepartmentId=d.Id  
join Branch b on d.BranchId=b.Id  
join RoomType rt on rt.Id=r.RoomTypeId  
where r.Status='Available' and f.Id=@FloorId and r.IsDelete=0  
  
End