CREATE PROCEDURE [dbo].[sp_GuestHistory]
	@guestId int


AS 
BEGIN
	SELECT r.StartDate, r.EndDate, r.IsCheckedIn, r.IsCanceled, r.TotalPrice,
	ro.RoomNumber, st.Title
	from dbo.RoomReservations as rr
	INNER JOIN dbo.Reservations AS r ON r.Id = rr.Id
	INNER JOIN dbo.Rooms AS ro ON ro.Id = rr.RoomId
	INNER JOIN dbo.StayTypes AS st ON st.Id = rr.StayTypeId
	WHERE rr.GuestId = @guestId
	
END