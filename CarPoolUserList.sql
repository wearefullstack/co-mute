USE [CarPool]
GO

/****** Object:  Table [dbo].[CarPoolUserList]    Script Date: 2022/07/15 12:56:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CarPoolUserList](
	[Idx] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CarPoolId] [int] NOT NULL,
 CONSTRAINT [PK_CarPoolUserList_1] PRIMARY KEY CLUSTERED 
(
	[Idx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CarPoolUserList]  WITH CHECK ADD  CONSTRAINT [FK_CarPoolUserList_CarPoolList] FOREIGN KEY([CarPoolId])
REFERENCES [dbo].[CarPoolList] ([CarPoolId])
GO

ALTER TABLE [dbo].[CarPoolUserList] CHECK CONSTRAINT [FK_CarPoolUserList_CarPoolList]
GO

ALTER TABLE [dbo].[CarPoolUserList]  WITH CHECK ADD  CONSTRAINT [FK_CarPoolUserList_CarPoolUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[CarPoolUser] ([Id])
GO

ALTER TABLE [dbo].[CarPoolUserList] CHECK CONSTRAINT [FK_CarPoolUserList_CarPoolUser]
GO


