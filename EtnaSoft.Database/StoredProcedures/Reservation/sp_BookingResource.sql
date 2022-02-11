CREATE PROCEDURE [dbo].[sp_BookingResource]
	

	AS

	BEGIN
		SELECT [r].[Id], [r].[RoomReservationId], [r].[NumberOfPeople], [r].[StartDate], [r].[EndDate],
		[r].InvoiceId, [r].[IsCheckedIn], [r].[IsCanceled], [rs].[Id], [rs].[RoomId], [rs].[StayTypeId],
		[rs].[GuestId], [rs].[DateCreated], [rs].[DateModified], [rs].[CreatedBy], [rs].[ModifiedBy],
		[ro].[Id] as RoomId, [ro].[RoomNumber], [g].[Id] as GuestId, [g].[FirstName], [g].[LastName], [g].[Telephone],
		[g].[EmailAddress], [g].[UniqueNumber], [g].[BirthDate], [g].[IsActive], [g].[Address] 
		from dbo.Reservations as r
		inner join dbo.RoomReservations as rs ON rs.Id = r.RoomReservationId
		inner join dbo.Rooms as ro ON rs.RoomId = ro.Id
		inner join dbo.Guests as g ON rs.GuestId = g.Id

	END