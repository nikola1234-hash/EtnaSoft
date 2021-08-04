CREATE PROCEDURE [dbo].[sp_UpdateUser]
	@Id int,
	@Name nvarchar(25),
	@LastName nvarchar(25),
	@Username nvarchar(25),
	@PasswordHash nvarchar(250),
	@ModifiedBy nvarchar(25)

	AS
	BEGIN
			
		UPDATE dbo.Users SET [Name] = @Name, LastName = @LastName, Username = @Username, PasswordHash = @PasswordHash, DateModified = GETDATE(),
		ModifiedBy = @ModifiedBy WHERE Id = @Id;

		return 0;
	END