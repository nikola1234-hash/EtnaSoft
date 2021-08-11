CREATE PROCEDURE [dbo].[sp_UpdateReservation]
	@Id int,
	@RoomReservationId int, 
	@NumberOfPeople int,
	@StartDate date,
	@EndDate date,
	@TotalPrice decimal(19,4),
	@IsCheckedIn bit,
	@ModifiedBy nvarchar(25),
	@IsCanceled bit,
	@DateModified date,
	@DateCreated date,
	@CreatedBy nvarchar(25)
	AS
	BEGIN
		if exists(SELECT 1 from dbo.Reservations WHERE Id = @Id)
		BEGIN 

			SET @DateModified = GETDATE();
			UPDATE dbo.Reservations SET RoomReservationId = @RoomReservationId,
			NumberOfPeople = @NumberOfPeople, StartDate = @StartDate, EndDate = @EndDate,
			TotalPrice = @TotalPrice, IsCheckedIn = @IsCheckedIn, ModifiedBy = @ModifiedBy,
			DateModified = @DateModified, IsCanceled = @IsCanceled WHERE Id = @Id;
			END
			SELECT * FROM dbo.Reservations WHERE Id = @@IDENTITY;
	END
