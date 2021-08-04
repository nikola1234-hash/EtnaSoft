CREATE PROCEDURE [dbo].[sp_CreateGuest]
	@FirstName nvarchar(25),
	@LastName nvarchar(25),
	@Telephone nvarchar(25),
	@EmailAddress nvarchar(250),
	@Address nvarchar(250),
	@UniqueNumber nvarchar(13),
	@BirthDate date,
	@CreatedBy nvarchar(25)
AS
BEGIN
	DECLARE @DateCreated date;
	SET @DateCreated = GETDATE();
	Insert into dbo.Guests (FirstName, LastName, Telephone, EmailAddress, [Address], UniqueNumber, BirthDate, DateCreated, CreatedBy)
    VALUES (@FirstName, @LastName, @Telephone, @EmailAddress, @Address, @UniqueNumber, @BirthDate, @DateCreated, @CreatedBy);
    BEGIN
	Declare @id as int;
	SET @id = @@IDENTITY;
	SELECT * from dbo.Guests where Id = @id
	END
END