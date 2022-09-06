USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[CancelJoinCarPoolTicket]    Script Date: 9/6/2022 12:38:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CancelJoinCarPoolTicket](
	@CarPoolTicketId INT,
	@OwnerId INT
)
AS
	IF EXISTS(SELECT 1 FROM CarPoolTicketAllocation WHERE CarPoolTicketId = @CarPoolTicketId AND CarPoolTicketPassengerID = @OwnerId)
	BEGIN
		DECLARE @DeletedId INT; 
		UPDATE CarPoolTickets SET AvailableSeats = AvailableSeats + 1 WHERE CarPoolTicketId = @CarPoolTicketId;

		SET @DeletedId = (SELECT CarPoolTicketAllocationId FROM CarPoolTicketAllocation WHERE CarPoolTicketId = @CarPoolTicketId AND CarPoolTicketPassengerID = @OwnerId)
		DELETE FROM CarPoolTicketAllocation WHERE CarPoolTicketId = @CarPoolTicketId AND CarPoolTicketPassengerID = @OwnerId
		
		SELECT @DeletedId;
	END
GO

