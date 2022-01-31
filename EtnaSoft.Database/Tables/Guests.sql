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
	[DateCreated] date NOT NULL ,
	[DateModified] date NULL,
	[CreatedBy] NVARCHAR(25) NOT NULL,
	[ModifiedBy] NVARCHAR(25) NULL, 
    [Address] NVARCHAR(250) NULL

)

GO

CREATE INDEX [IX_Guests_ID] ON [dbo].[Guests] (Id)

GO

CREATE INDEX [IX_Guests_FName] ON [dbo].[Guests] (FirstName)

GO

CREATE INDEX [IX_Guests_LName] ON [dbo].[Guests] (LastName)
