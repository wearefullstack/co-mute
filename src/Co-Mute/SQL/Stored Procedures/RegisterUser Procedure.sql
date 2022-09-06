CREATE PROCEDURE RegisterNewUser(
	@Name VARCHAR(50), 
	@Surname VARCHAR(50), 
	@Email VARCHAR(50),
	@Phone VARCHAR(10),
	@Password VARCHAR(MAX)
)
AS
	DECLARE @UserID INT;
	INSERT INTO Users(Name, Surname, Email, Phone, Password) VALUES(@Name, @Surname, @Email, @Phone, @Password)
	SELECT @UserID = SCOPE_IDENTITY();
	INSERT INTO Owners(OwnerId, Name) VALUES(@UserID, @Name + ' ' + @Surname) 