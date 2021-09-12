CREATE PROCEDURE [dbo].[sp_LoadFreeRooms]
	@StartDate date,
	@EndDate date

	AS

	BEGIN
		select * from dbo.Rooms as r
		where r.Id not in
		( select rr.RoomId from dbo.RoomReservations as rr
		inner join dbo.Reservations as re on re.RoomReservationId = rr.Id
		where 
		(@startDate < re.StartDate AND @endDate < re.EndDate)
		OR(re.StartDate <= @endDate AND @endDate < re.EndDate)
		OR(re.StartDate <= @startDate AND @startDate < re.EndDate));
	END