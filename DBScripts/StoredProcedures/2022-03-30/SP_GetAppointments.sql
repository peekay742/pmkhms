USE [hms_db]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointments]    Script Date: 3/31/2022 8:39:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointments]
	-- Add the parameters for the stored procedure here
	@DoctorId int=null,
	@PatientId int=null,
	@AppointmentId int =null,
	@StartDate datetime=null,
	@EndDate datetime=null
AS
BEGIN

	SET NOCOUNT ON;

  
	
select A.*,D.Name as DoctorName,P.Name as PatientName,APT.Type as AppointmentTypeName from Appointment A join Doctor D on D.Id=A.DoctorId
join Patient P on P.Id=A.PatientId
join AppointmentType APT on APT.Id=A.AppointmentTypeId
where 
(@DoctorId is null OR A.DoctorId=@DoctorId)
and (@AppointmentId is null OR A.Id=@AppointmentId)
and (@PatientId is null OR A.PatientId=@PatientId)
and ((@StartDate is null OR CONVERT(date, A.StartDate)>=CONVERT(date, @StartDate)) or ((@StartDate is null OR CONVERT(date, A.EndDate)>=CONVERT(date, @StartDate))))
and ((@EndDate is null OR CONVERT(date, A.EndDate)>=CONVERT(date, @EndDate)) or (@EndDate is null OR CONVERT(date, A.EndDate)<=CONVERT(date, @EndDate)))
--and (@StartDate is null OR  FORMAT(A.StartDate,'hh')>=FORMAT(@StartDate,'hh'))
--and ((@EndDate is null OR FORMAT(A.EndDate,'hh')>=FORMAT(@EndDate,'hh')) or (@EndDate is null OR FORMAT(A.EndDate,'hh')<=FORMAT(@EndDate,'hh')))
and A.IsDelete=0 and (A.[Status]!=3 and A.[Status]!=4)


End
