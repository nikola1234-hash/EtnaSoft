CREATE PROCEDURE [dbo].[sp_CreateGuest]
	@FirstName nvarchar(25),
	@LastName nvarchar(25),
	@Telephone nvarchar(25),
	@EmailAddress nvarchar(250),
	@Address nvarchar(250),
	@UniqueNumber nvarchar(13),
	@BirthDate date
AS
BEGIN
	Insert into dbo.Guest (FirstName, LastName, Telephone, EmailAddress, [Address], UniqueNumber, BirthDate)
    VALUES (@FirstName, @LastName, @Telephone, @EmailAddress, @Address, @UniqueNumber, @BirthDate);
    BEGIN
	Declare @id as int;
	SET @id = @@IDENTITY;
	SELECT * from dbo.Guest where Id = @id
	END
END