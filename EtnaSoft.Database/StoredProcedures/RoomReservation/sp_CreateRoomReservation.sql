CREATE PROCEDURE [dbo].[sp_CreateRoomReservation]
	@GuestId int,
	@StayTypeId int,
	@RoomId int,
	@CreatedBy nvarchar(25)
	AS
	BEGIN
		DECLARE @DateCreated date;
		SET @DateCreated = GETDATE();
		INSERT INTO dbo.RoomReservations(GuestId, StayTypeId, RoomId, CreatedBy, DateCreated)
		VALUES(@GuestId, @StayTypeId, @RoomId, @CreatedBy, @DateCreated);
		DECLARE @Id int;
		SET @Id = SCOPE_IDENTITY();
			BEGIN
			SELECT * from dbo.RoomReservations where Id = @Id;
			END
	END