USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[RegisterNewUser]    Script Date: 9/6/2022 12:42:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegisterNewUser](
	@Name VARCHAR(50), 
	@Surname VARCHAR(50), 
	@Email VARCHAR(50),
	@Phone VARCHAR(10),
	@Password VARBINARY(MAX),
	@Salt VARBINARY(MAX)
)
AS
	DECLARE @UserID INT;
	INSERT INTO Users(Name, Surname, Email, Phone, Password) VALUES(@Name, @Surname, @Email, @Phone, @Password);
	SELECT @UserID = SCOPE_IDENTITY();
	INSERT INTO Owners(OwnerId, Name, Salt) VALUES(@UserID, @Name + ' ' + @Surname, @Salt) 
GO

