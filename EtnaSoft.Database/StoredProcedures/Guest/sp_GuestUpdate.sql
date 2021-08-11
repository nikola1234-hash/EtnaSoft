CREATE PROCEDURE [dbo].[sp_GuestUpdate]
	@Id int,
	@FirstName nvarchar(25),
	@LastName nvarchar(25),
	@EmailAddress nvarchar(250),
	@UniqueNumber nvarchar(13),
	@Address nvarchar(250),
	@ModifiedBy nvarchar(25),
	@Telephone nvarchar(250)

	AS
	BEGIN
		if exists (select 1 from dbo.Guests WHERE Id = @Id)
		BEGIN 
			DECLARE @DateModified date;
			SET @DateModified = GETDATE();
			UPDATE dbo.Guests SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, UniqueNumber = @UniqueNumber,
			[Address] = @Address, Telephone = @Telephone WHERE Id = @id;
		END
	END
