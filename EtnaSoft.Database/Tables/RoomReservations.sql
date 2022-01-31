CREATE TABLE [dbo].[RoomReservations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoomId] INT NOT NULL,
	[StayTypeId] INT NOT NULL,
	[GuestId] INT NOT NULL,
	[DateCreated] date NOT NULL DEFAULT GETDATE(),
	[DateModified] date NULL,
	[CreatedBy] NVARCHAR(25) NOT NULL,
	[ModifiedBy] NVARCHAR(25) NULL, 
    CONSTRAINT [FK_RoomReservations_ToRoom] FOREIGN KEY (RoomId) REFERENCES Rooms(Id), 
    CONSTRAINT [FK_RoomReservations_ToStayTypes] FOREIGN KEY (StayTypeId) REFERENCES StayTypes(Id), 
    CONSTRAINT [FK_RoomReservations_ToGuests] FOREIGN KEY (GuestId) REFERENCES Guests(Id)
)
GO
CREATE INDEX IX_RoomReservationId ON [dbo].[RoomReservations](Id)