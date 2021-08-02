CREATE TABLE [dbo].[Guests]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[FirstName] NVARCHAR(25) NOT NULL,
	[LastName] NVARCHAR(25) NOT NULL,
	[Telephone] NVARCHAR(250) NOT NULL,
	[EmailAddress] NVARCHAR(250) NULL,
	[UniqueNumber] NVARCHAR(13) null,
	[BirthDate] date null,
	[IsActive] bit NOT NULL DEFAULT 1,
	[DateCreated] date NOT NULL DEFAULT GETDATE(),
	[DateModified] date NULL,
	[CreatedBy] NVARCHAR(25) NOT NULL,
	[ModifiedBy] NVARCHAR(25) NULL

)
