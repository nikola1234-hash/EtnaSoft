USE [EtnaDb]
GO
/****** Object:  Table [dbo].[Guests]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](25) NOT NULL,
	[LastName] [nvarchar](25) NOT NULL,
	[Telephone] [nvarchar](250) NOT NULL,
	[EmailAddress] [nvarchar](250) NULL,
	[UniqueNumber] [nvarchar](13) NULL,
	[BirthDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NULL,
	[CreatedBy] [nvarchar](25) NOT NULL,
	[ModifiedBy] [nvarchar](25) NULL,
	[Address] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Labels]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Labels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](20) NOT NULL,
	[Color] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentStatuses]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomReservationId] [int] NOT NULL,
	[NumberOfPeople] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[TotalPrice] [decimal](19, 4) NOT NULL,
	[IsCheckedIn] [bit] NOT NULL,
	[IsCanceled] [bit] NOT NULL,
	[CreatedBy] [nvarchar](25) NOT NULL,
	[DateCreated] [date] NOT NULL,
	[ModifiedBy] [nvarchar](25) NULL,
	[DateModified] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomReservations]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomReservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[StayTypeId] [int] NOT NULL,
	[GuestId] [int] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NULL,
	[CreatedBy] [nvarchar](25) NOT NULL,
	[ModifiedBy] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StayTypes]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StayTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Price] [decimal](19, 4) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[LastName] [nvarchar](25) NOT NULL,
	[Username] [nvarchar](25) NOT NULL,
	[PasswordHash] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NULL,
	[CreatedBy] [nvarchar](25) NOT NULL,
	[ModifiedBy] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Guests] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Reservations] ADD  DEFAULT ((0)) FOR [IsCheckedIn]
GO
ALTER TABLE [dbo].[Reservations] ADD  DEFAULT ((0)) FOR [IsCanceled]
GO
ALTER TABLE [dbo].[RoomReservations] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[StayTypes] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_ToRoomReservation] FOREIGN KEY([RoomReservationId])
REFERENCES [dbo].[RoomReservations] ([Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_ToRoomReservation]
GO
ALTER TABLE [dbo].[RoomReservations]  WITH CHECK ADD  CONSTRAINT [FK_RoomReservations_ToGuests] FOREIGN KEY([GuestId])
REFERENCES [dbo].[Guests] ([Id])
GO
ALTER TABLE [dbo].[RoomReservations] CHECK CONSTRAINT [FK_RoomReservations_ToGuests]
GO
ALTER TABLE [dbo].[RoomReservations]  WITH CHECK ADD  CONSTRAINT [FK_RoomReservations_ToRoom] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[RoomReservations] CHECK CONSTRAINT [FK_RoomReservations_ToRoom]
GO
ALTER TABLE [dbo].[RoomReservations]  WITH CHECK ADD  CONSTRAINT [FK_RoomReservations_ToStayTypes] FOREIGN KEY([StayTypeId])
REFERENCES [dbo].[StayTypes] ([Id])
GO
ALTER TABLE [dbo].[RoomReservations] CHECK CONSTRAINT [FK_RoomReservations_ToStayTypes]
GO
/****** Object:  StoredProcedure [dbo].[sp_BookingResource]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BookingResource]
	

	AS

	BEGIN
		SELECT [r].[Id], [r].[RoomReservationId], [r].[NumberOfPeople], [r].[StartDate], [r].[EndDate],
		[r].[TotalPrice], [r].[IsCheckedIn], [r].[IsCanceled], [rs].[Id], [rs].[RoomId], [rs].[StayTypeId],
		[rs].[GuestId], [rs].[DateCreated], [rs].[DateModified], [rs].[CreatedBy], [rs].[ModifiedBy],
		[ro].[Id] as RoomId, [ro].[RoomNumber], [g].[Id] as GuestId, [g].[FirstName], [g].[LastName], [g].[Telephone],
		[g].[EmailAddress], [g].[UniqueNumber], [g].[BirthDate], [g].[IsActive], [g].[Address] 
		from dbo.Reservations as r
		inner join dbo.RoomReservations as rs ON rs.Id = r.RoomReservationId
		inner join dbo.Rooms as ro ON rs.RoomId = ro.Id
		inner join dbo.Guests as g ON rs.GuestId = g.Id

	END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateGuest]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateReservation]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CreateReservation]
	@RoomReservationId int, 
	@NumberOfPeople int,
	@StartDate date,
	@EndDate date,
	@TotalPrice decimal(19,4),
	@CreatedBy nvarchar(25)
	AS
	BEGIN
		DECLARE @DateCreated date;
		SET @DateCreated = GETDATE();
		INSERT INTO dbo.Reservations (RoomReservationId, NumberOfPeople, StartDate, EndDate, TotalPrice, CreatedBy, DateCreated)
		VALUES (@RoomReservationId, @NumberOfPeople, @StartDate, @EndDate, @TotalPrice, @CreatedBy, @DateCreated)
		DECLARE @Id int;
		SET @Id = SCOPE_IDENTITY();
		BEGIN
			SELECT * from dbo.Reservations WHERE Id = @Id;
		END


	END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateRoomReservation]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CreateRoomReservation]
	@GuestId int,
	@StayTypeId int,
	@RoomId int,
	@CreatedBy nvarchar(25)
	AS
	BEGIN
		DECLARE @DateCreated date;
		SET @DateCreated = GETDATE();
		INSERT INTO dbo.RoomReservations(GuestId, StayTypeId, RoomId, CreatedBy, DateCreated)
		VALUES(@GuestId, @StayTypeId, @RoomId, @CreatedBy, @DateCreated);
		DECLARE @Id int;
		SET @Id = SCOPE_IDENTITY();
			BEGIN
			SELECT * from dbo.RoomReservations where Id = @Id;
			END
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateUser]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
		
		BEGIN
			SELECT * From dbo.Users where Id = @@ROWCOUNT;
		END

	END
GO
/****** Object:  StoredProcedure [dbo].[sp_GuestUpdate]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateReservation]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateReservation]
	@Id int,
	@RoomReservationId int, 
	@NumberOfPeople int,
	@StartDate date,
	@EndDate date,
	@TotalPrice decimal(19,4),
	@IsCheckedIn bit,
	@ModifiedBy nvarchar(25),
	@IsCanceled bit
	AS
	BEGIN
		if exists(SELECT 1 from dbo.Reservations WHERE Id = @Id)
		BEGIN 
			DECLARE @DateModified date;
			SET @DateModified = GETDATE();
			UPDATE dbo.Reservations SET RoomReservationId = @RoomReservationId,
			NumberOfPeople = @NumberOfPeople, StartDate = @StartDate, EndDate = @EndDate,
			TotalPrice = @TotalPrice, IsCheckedIn = @IsCheckedIn, ModifiedBy = @ModifiedBy,
			DateModified = @DateModified, IsCanceled = @IsCanceled WHERE Id = @Id;
			END
			SELECT * FROM dbo.Reservations WHERE Id = @@IDENTITY;
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateRoomReservation]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateRoomReservation]
	@Id int,
	@RoomId int,
	@GuestId int,
	@StayTypeId int,
	@ModifiedBy nvarchar(25)
	
	AS
	BEGIN
	DECLARE @DateModified date;
	SET @DateModified = GETDATE();
		UPDATE dbo.RoomReservations SET RoomId = @RoomId, GuestId = @GuestId, StayTypeId = @StayTypeId, ModifiedBy = @ModifiedBy,
		DateModified= @DateModified WHERE Id = @Id;

	END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUser]    Script Date: 01/09/2021 21:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
