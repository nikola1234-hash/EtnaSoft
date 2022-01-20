CREATE PROCEDURE [dbo].[sp_CreateUser]
	@Name nvarchar(25),
	@LastName nvarchar(25),
	@Username nvarchar(25),
	@PasswordHash nvarchar(250),
	@CreatedBy nvarchar(25)

	AS
	BEGIN
	    DECLARE @DateCreated date;
		SET @DateCreated = GETDATE();

		INSERT INTO dbo.[Users] ([Name], LastName, Username, PasswordHash, CreatedBy, DateCreated)
		VALUES (@Name, @LastName, @Username,@PasswordHash, @CreatedBy, @DateCreated);
		DECLARE @id int;
		SET @id = @@IDENTITY;
		BEGIN
			SELECT * From dbo.Users where Id = @id;
		END

	END
