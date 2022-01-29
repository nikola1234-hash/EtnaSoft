CREATE TABLE [dbo].[Promocije]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[StayTypeId] INT NOT NULL,
	[StayDays] INT NOT NULL,
	[ChildPrice] decimal(19, 4) NOT NULL,
	[Price] decimal(19, 4) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Promocije_StayTypes] FOREIGN KEY (StayTypeId) REFERENCES StayTypes(Id)
)
