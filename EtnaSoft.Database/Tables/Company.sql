CREATE TABLE [dbo].[Company]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nvarchar(80) NOT NULL,
	[BankName] nvarchar(25) NULL,
	[BankAccount] NVARCHAR(100) NULL,
	[BankName2] nvarchar(25) NULL,
	[BankAccount2] NVARCHAR(100) NULL,
	[Address] NVARCHAR(100) NULL,
	[Telephone] NVARCHAR(25) NULL,
	[Telephone2] NVARCHAR(25) NULL,
	[Email] nvarchar(256) NULL,
	[ImageLogo] VARBINARY NULL,
	[Pib] Nvarchar(50) NULL,
	[MaticniBroj] Nvarchar(50) NULL



)

GO

CREATE INDEX [IX_Company_ID] ON [dbo].[Company] (Id)
