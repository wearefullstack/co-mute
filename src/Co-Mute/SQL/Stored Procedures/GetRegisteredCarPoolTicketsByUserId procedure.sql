USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[GetRegisteredCarPoolTicketsbyUserId]    Script Date: 9/6/2022 12:41:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRegisteredCarPoolTicketsbyUserId](
@CarPoolTicketPassengerID INT
)
AS
IF EXISTS (SELECT 1 FROM Users WHERE UserId = @CarPoolTicketPassengerID)
BEGIN
	SELECT cpta.*, cpta.CarPoolTicketId as Id, cpt.*, cpts.Status FROM CarPoolTicketAllocation cpta 
	LEFT JOIN CarPoolTickets cpt ON cpta.CarPoolTicketId = cpt.CarPoolTicketId 
	LEFT JOIN CarPoolTicketStatus cpts ON cpta.CarPoolTicketAllocationStatus = cpts.StatusId
	WHERE cpta.CarPoolTicketPassengerID = @CarPoolTicketPassengerID
END
GO

