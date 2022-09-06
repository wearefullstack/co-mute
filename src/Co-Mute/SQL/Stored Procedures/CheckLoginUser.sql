USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[CheckLoginUser]    Script Date: 9/6/2022 12:38:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CheckLoginUser](@Email VARCHAR(50), @Password VARCHAR(MAX))
AS

IF EXISTS(SELECT 1 FROM Users WHERE Email = @Email AND Password = @Password)
BEGIN
	RETURN 1;
END
ELSE
	RETURN -1;
GO

