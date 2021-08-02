CREATE TABLE [dbo].[Reservations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoomReservationId] INT NOT NULL,
	[NumberOfPeople] int NOT NULL,
	[StartDate] date NOT NULL,
	[EndDate] date NOT NULL,
	[TotalPrice] decimal(19, 4) NOT NULL,
	[IsCheckedIn] bit NOT NULL DEFAULT 0,
	[IsCanceled] bit NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Reservations_ToRoomReservation] FOREIGN KEY (RoomReservationId) REFERENCES RoomReservations(Id)
)
