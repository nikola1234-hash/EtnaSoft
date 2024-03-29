﻿CREATE TABLE [dbo].[StayTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Price] DECIMAL(19, 4) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsSpecialType] BIT NOT NULL DEFAULT 0
)
GO
CREATE INDEX IX_StayTypesId ON dbo.StayTypes (Id)
