﻿CREATE PROCEDURE [dbo].[sp_UpdateReservation]
	@Id int,
	@RoomReservationId int, 
	@NumberOfPeople int,
	@InvoiceId int,
	@StartDate date,
	@EndDate date,
	@IsCheckedIn bit,
	@ModifiedBy nvarchar(25),
	@IsCanceled bit
	AS
	BEGIN
		if exists(SELECT 1 from dbo.Reservations WHERE Id = @Id)
		BEGIN 
			DECLARE @DateModified date;
			SET @DateModified = GETDATE();
			UPDATE dbo.Reservations SET RoomReservationId = @RoomReservationId, InvoiceId = @InvoiceId,
			NumberOfPeople = @NumberOfPeople, StartDate = @StartDate, EndDate = @EndDate,
			IsCheckedIn = @IsCheckedIn, ModifiedBy = @ModifiedBy,
			DateModified = @DateModified, IsCanceled = @IsCanceled WHERE Id = @Id;
			END
			SELECT * FROM dbo.Reservations WHERE Id = @@IDENTITY;
	END
