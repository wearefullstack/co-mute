USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[GetCreatedCarPoolTicketsByUserId]    Script Date: 9/6/2022 12:40:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCreatedCarPoolTicketsByUserId](@OwnerId INT)
AS
	SELECT cpt.*, cpt.CarPoolTicketId as Id, cpts.Status FROM CarPoolTickets cpt
	left JOIN CarPoolTicketStatus cpts ON cpt.CarPoolTicketsStatus = cpts.StatusId
	WHERE OwnerId = @OwnerId ORDER By cpt.CarPoolTicketsStatus
GO

