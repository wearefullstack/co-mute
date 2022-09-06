USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[CreateCarPoolTicket]    Script Date: 9/6/2022 12:39:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateCarPoolTicket](
	@OwnerId INT, 
	@Origin VARCHAR(50), 
	@Destination VARCHAR(50),
	@DepartureTime DATETIME,
	@ExpectedArrivalTime DATETIME,
	@AvailableSeats INT,
	@Notes VARCHAR(500)
)
AS
	
	DECLARE  @AllowCreatePermission INT;
	EXEC @AllowCreatePermission = CheckCarPoolTicketCreationDateAvailable @DepartureTime, @ExpectedArrivalTime, @OwnerId

	IF(@AllowCreatePermission > 0)
	BEGIN
		INSERT INTO CarPoolTickets(OwnerId, Origin, Destination, CreationDate, DepartureTime, ExpectedArrivalTime, AvailableSeats, DaysAvailable, Notes,CarPoolTicketsStatus) 
		VALUES(@OwnerId, @Origin, @Destination, CONVERT(VARCHAR, GETDATE(), 13), Convert(VARCHAR, @DepartureTime, 13),
				Convert(VARCHAR, @ExpectedArrivalTime, 13), @AvailableSeats, DATEDIFF(DAY, GETDATE(), @DepartureTime), @Notes, 0);
	
		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
GO

