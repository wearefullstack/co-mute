USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[UpdateCarPoolTicketDetails]    Script Date: 9/6/2022 12:42:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateCarPoolTicketDetails](
@CarPoolTicketId INT,
@Origin VARCHAR(50),
@Destination VARCHAR(50),
@DepartureTime DATETIME,
@ExpectedArrivalTime DATETIME,
@AvailableSeats INT,
@Notes VARCHAR(500)
)
AS
IF EXISTS(SELECT 1 FROM CarPoolTickets WHERE CarPoolTicketId = @CarPoolTicketId)
BEGIN
	UPDATE CarPoolTickets SET Origin = @Origin, Destination = @Destination, DepartureTime = @DepartureTime, ExpectedArrivalTime = @ExpectedArrivalTime,
		AvailableSeats = @AvailableSeats, Notes = @Notes WHERE CarPoolTicketId = @CarPoolTicketId
	SELECT @CarPoolTicketId
END
ELSE
	SELECT -1

	
GO

