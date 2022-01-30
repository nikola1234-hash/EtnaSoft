CREATE TABLE [dbo].[RoomStatus]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoomId] INT NOT NULL,
	[DateUsed] date NOT NULL,
	[IsDirty] BiT NOT NULL DEFAULT 1, 
	[DateCleaned] date NULL, 
    [RoomReservationId] INT NOT NULL, 
    CONSTRAINT [FK_RoomStatus_ToRoomReservation] FOREIGN KEY (RoomReservationId) REFERENCES RoomReservations(Id)
  

)
