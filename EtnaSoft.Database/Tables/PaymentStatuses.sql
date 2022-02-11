CREATE TABLE [dbo].[PaymentStatuses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Caption] nvarchar(250) NOT NULL,
	[InvoiceId] int not null,
	[IsAvansPaid] bit NOT NULL DEFAULT 0
)
