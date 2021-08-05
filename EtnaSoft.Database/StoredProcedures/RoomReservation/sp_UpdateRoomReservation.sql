CREATE PROCEDURE [dbo].[sp_UpdateRoomReservation]
	@Id int,
	@RoomId int,
	@GuestId int,
	@StayTypeId int,
	@ModifiedBy nvarchar(25)
	
	AS
	BEGIN
	DECLARE @DateModified date;
	SET @DateModified = GETDATE();
		UPDATE dbo.RoomReservations SET RoomId = @RoomId, GuestId = @GuestId, StayTypeId = @StayTypeId, ModifiedBy = @ModifiedBy,
		DateModified= @DateModified WHERE Id = @Id;

	END