CREATE TABLE [dbo].[Invoices]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Avans] decimal(19,4) NOT NULL DEFAULT 0,
	[SubTotal] decimal(19,4) NOT NULL DEFAULT 0,
	[TotalPrice] decimal (19, 4) NOT NULL DEFAULT 0,
	[IsCanceled] bit NOT NULL DEFAULT 0,
	[DateGenerated] date NOT NULL
)

GO




CREATE INDEX [IX_Invoices_Id] ON [dbo].[Invoices] (Id)

GO

CREATE INDEX [IX_Invoices_TotalPrice] ON [dbo].[Invoices] (TotalPrice)

GO

CREATE INDEX [IX_Invoices_Avans] ON [dbo].[Invoices] (Avans)

GO

CREATE INDEX [IX_Invoices_Date] ON [dbo].[Invoices] (DateGenerated)
