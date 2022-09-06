USE [Co-mute]
GO

/****** Object:  Table [dbo].[Owners]    Script Date: 9/6/2022 12:35:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Owners](
	[OwnerId] [int] NULL,
	[Name] [varchar](50) NOT NULL,
	[salt] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Owners]  WITH CHECK ADD FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Users] ([UserId])
GO

