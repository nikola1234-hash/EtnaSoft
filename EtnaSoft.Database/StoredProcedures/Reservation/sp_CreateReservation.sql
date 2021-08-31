CREATE PROCEDURE [dbo].[sp_CreateReservation]
	@RoomReservationId int, 
	@NumberOfPeople int,
	@StartDate date,
	@EndDate date,
	@TotalPrice decimal(19,4),
	@CreatedBy nvarchar(25)
	AS
	BEGIN
		DECLARE @DateCreated date;
		SET @DateCreated = GETDATE();
		INSERT INTO dbo.Reservations (RoomReservationId, NumberOfPeople, StartDate, EndDate, TotalPrice, CreatedBy, DateCreated)
		VALUES (@RoomReservationId, @NumberOfPeople, @StartDate, @EndDate, @TotalPrice, @CreatedBy, @DateCreated)
		DECLARE @Id int;
		SET @Id = SCOPE_IDENTITY();
		BEGIN
			SELECT * from dbo.Reservations WHERE Id = @Id;
		END


	END
