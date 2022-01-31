CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(25) NOT NULL,
	[LastName] NVARCHAR(25) NOT NULL,
	[Username] NVARCHAR(25) NOT NULL,
	[PasswordHash] NVARCHAR(250) NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[DateCreated] date NOT NULL ,
	[DateModified] date NULL,
	[CreatedBy] NVARCHAR(25) NOT NULL,
	[ModifiedBy] NVARCHAR(25) NULL

)
GO
CREATE INDEX IX_UsersId ON dbo.Users (Id)
GO
CREATE INDEX IX_UsersName ON dbo.Users(Username)