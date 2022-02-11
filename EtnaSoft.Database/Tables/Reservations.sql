CREATE TABLE [dbo].[Reservations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoomReservationId] INT NOT NULL,
	[InvoiceId] INT NOT NULL,
	[NumberOfPeople] int NOT NULL,
	[StartDate] date NOT NULL,
	[EndDate] date NOT NULL,
	[IsCheckedIn] bit NOT NULL DEFAULT 0,
	[IsCanceled] bit NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(25) NOT NULL, 
	[DateCreated] date NOT NULL,
	[ModifiedBy] nvarchar(25) NULL,
	[DateModified] date NULL
    CONSTRAINT [FK_Reservations_ToRoomReservation] FOREIGN KEY (RoomReservationId) REFERENCES RoomReservations(Id),
	CONSTRAINT [FK_Reservations_ToInvoices] FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id)

)
GO
CREATE INDEX [IX_ReservationId] ON [dbo].[Reservations] (Id)
GO
CREATE INDEX [IX_RoomReservationId] ON [dbo].[Reservations] (RoomReservationId)
GO 
CREATE INDEX [IX_StartDate] ON dbo.Reservations (StartDate)
GO 
CREATE INDEX [IX_EndDate] ON dbo.Reservations (EndDate)
GO
Create INDEX [IX_IsCheckedId] ON dbo.Reservations (IsCheckedIn)
GO
CREATE INDEX [IX_Invoice] ON dbo.Reservations (InvoiceId)