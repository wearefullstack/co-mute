USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[GetUserLoginDetail]    Script Date: 9/6/2022 12:41:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserLoginDetail](@Email VARCHAR(50))
AS
	SELECT u.Password as PasswordHash, u.UserId, o.Salt as PasswordSalt, u.Email FROM Users u JOIN Owners o ON u.UserId = o.OwnerId WHERE u.Email = @Email


GO

