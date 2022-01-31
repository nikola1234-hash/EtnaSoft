CREATE TABLE [dbo].[Rooms]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoomNumber] nvarchar(10) NOT NULL
)
GO
CREATE INDEX IX_RoomId ON [dbo].[Rooms] (Id)