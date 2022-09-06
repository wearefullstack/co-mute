USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 9/6/2022 12:43:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateUserDetails](  
 @UserId INT,  
 @Name VARCHAR(50),  
 @Surname VARCHAR(50),  
 @Email VARCHAR(50),  
 @Phone VARCHAR(50),  
 @Password VARBINARY(MAX),  
 @PasswordSalt VARBINARY(MAX)  
)  
AS  
 UPDATE Users SET Name = @Name, Surname = @Surname, Email = @Email, Phone = @Phone, Password = @Password WHERE UserId = @UserId;  
 UPDATE Owners SET salt = @PasswordSalt WHERE OwnerId = @UserId  
  
 SELECT @UserId
GO

