CREATE PROCEDURE [dbo].[sp_CreateReservation]
	@RoomReservationId int, 
	@InvoiceId int,
	@NumberOfPeople int,
	@StartDate date,
	@EndDate date,
	@CreatedBy nvarchar(25)
	AS
	BEGIN
		DECLARE @DateCreated date;
		SET @DateCreated = GETDATE();
		INSERT INTO dbo.Reservations (RoomReservationId, InvoiceId, NumberOfPeople, StartDate, EndDate, CreatedBy, DateCreated)
		VALUES (@RoomReservationId, @InvoiceId, @NumberOfPeople, @StartDate, @EndDate, @CreatedBy, @DateCreated)
		DECLARE @Id int;
		SET @Id = SCOPE_IDENTITY();
		BEGIN
			SELECT * from dbo.Reservations WHERE Id = @Id;
		END


	END
