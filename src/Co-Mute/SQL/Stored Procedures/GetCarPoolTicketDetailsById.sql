USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[GetCarPoolTicketDetailsById]    Script Date: 9/6/2022 12:40:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCarPoolTicketDetailsById](@OwnerId int)  
AS  
  

 SELECT cpt.CarPoolTicketId as Cid,cpt.*, cpts.Status ,cpta.CarPoolTicketPassengerID,cpt.CarPoolTicketId as id, cpta.PassengerNote, u.Name, u.Surname, u.Phone, u.Email FROM CarPoolTickets cpt
 LEFT Join CarPoolTicketAllocation cpta On cpt.CarPoolTicketId = cpta.CarPoolTicketId
 LEFT JOIN Users u ON cpta.CarPoolTicketPassengerID = u.UserId
 Join CarPoolTicketStatus cpts ON  cpt.CarPoolTicketsStatus = cpts.StatusId
 WHERE cpt.OwnerId =  
 @OwnerId
GO

