CREATE PROCEDURE [dbo].[sp_CreateInvoice]
	@avans decimal,
	@subTotal decimal,
	@totalPrice decimal
AS
	BEGIN
	DECLARE @Date date;
	SET @Date = GETDATE();
		INSERT into dbo.Invoices (Avans, SubTotal, TotalPrice, DateGenerated)VALUES(@avans, @subTotal, @totalPrice, @Date)
		DECLARE @Id int;
		SET @Id = SCOPE_IDENTITY();
			BEGIN
			SELECT * from dbo.Invoices where Id = @Id;
			END
	END
