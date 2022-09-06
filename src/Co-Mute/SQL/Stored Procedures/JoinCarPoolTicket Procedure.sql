USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[JoinCarPoolTicket]    Script Date: 9/6/2022 12:42:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[JoinCarPoolTicket](
	@CarPoolTicketId INT,
	@OwnerId INT,
	@PassengerNote VARCHAR(500)
)
AS
	IF EXISTS(SELECT 1 FROM CarPoolTickets WHERE DaysAvailable > 0 AND AvailableSeats > 0 AND CarPoolTicketId = @CarPoolTicketId AND CarPoolTicketsStatus < 1)
	BEGIN
		DECLARE @DepartureTime DATETIME, @ExpectedArrivalTime DATETIME, @AllowCreatePermission INT;
		SELECT @DepartureTime = DepartureTime FROM CarPoolTickets WHERE CarPoolTicketId = @CarPoolTicketId;
		SELECT @ExpectedArrivalTime = ExpectedArrivalTime FROM CarPoolTickets WHERE CarPoolTicketId = @CarPoolTicketId;

		EXEC @AllowCreatePermission = CheckCarPoolTicketCreationDateAvailable @DepartureTime, @ExpectedArrivalTime, @OwnerId

		IF(@AllowCreatePermission > 0)
		BEGIN
			UPDATE CarPoolTickets SET AvailableSeats = AvailableSeats - 1 WHERE CarPoolTicketId = @CarPoolTicketId

			INSERT INTO CarPoolTicketAllocation(CarPoolTicketId, CarPoolTicketPassengerID, PassengerNote, DepartureTime, ExpectedArrivalTime, CarPoolTicketAllocationStatus) 
			VALUES(@CarPoolTicketId, @OwnerId, @PassengerNote, @DepartureTime, @ExpectedArrivalTime, 0)


			SELECT CAST(SCOPE_IDENTITY() AS INT);
		END
		ELSE
			SELECt -2
	END
	ELSE
		SELECT -1		

		
GO

