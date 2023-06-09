
/****** Object:  StoredProcedure [dbo].[SP_GetWard]    Script Date: 4/21/2022 1:45:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetWards] 
    @FloorId int=NULL,
	@DepartmentId int=NULL,
	@WardId int=NULL,
    @Name NVARCHAR(MAX) = NULL 
AS
BEGIN
	SET NOCOUNT ON;

	Select W.*,D.Name As [DepartmentName] , F.Name As [FloorName] , O.Name As [OutletName] from Ward W  
		LEFT JOIN Department D ON W.DepartmentId=D.Id
        LEFT JOIN Floor F ON W.FloorId=F.Id
		LEFT JOIN Outlet O ON W.OutletId=O.Id
	where             
    (@Name IS NULL OR (W.Name LIKE '%' + @Name + '%' OR @Name LIKE '%' + W.Name + '%')) AND
    (@FloorId is NULL or W.FloorId=@FloorId) AND
    (@DepartmentId is NULL or W.DepartmentId=@DepartmentId) AND
	(@WardId is NULL or W.Id=@WardId) AND
    W.IsDelete=0
END


