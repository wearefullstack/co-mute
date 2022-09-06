USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[FindCarPoolTickets]    Script Date: 9/6/2022 12:39:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FindCarPoolTickets](
@OwnerId INT,
@searchText VARCHAR(50)
)
AS
SELECT cpt.*, cpt.CarPoolTicketId as Id, cpts.Status, ISNULL(o.Name, 'not set') as FullName
FROM CarPoolTickets cpt Left Join Owners o ON cpt.OwnerId = o.OwnerId 
Inner Join CarPoolTicketStatus cpts ON cpt.CarPoolTicketsStatus = cpts.StatusId
WHERE cpt.OwnerId <> @OwnerId AND cpt.CarPoolTicketsStatus < 2  AND Destination LIKE @searchText +'%' ORDER BY cpt.CarPoolTicketsStatus
GO

