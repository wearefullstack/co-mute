CREATE PROCEDURE GetAllCarPoolTickets
AS
SELECT cpt.*, o.Name as Owner FROM CarPoolTickets cpt JOIN Owners o ON cpt.Owner = o.OwnerId
