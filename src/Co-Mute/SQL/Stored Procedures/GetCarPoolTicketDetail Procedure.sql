CREATE PROCEDURE GetCarPoolTicketDetailsById(@CarPoolTicketId int)
AS
SELECT * FROM CarPoolTickets WHERE CarPoolTicketId = @CarPoolTicketId;
SELECT cpta.*, u.Name, u.Surname, u.Email, u.Phone FROM CarPoolTicketAllocation cpta JOIN Users u ON cpta.CarPoolTicketPassengerID = u.UserId
WHERE cpta.CarPoolTicketId = @CarPoolTicketId
