USE [CarPool]
GO

/****** Object:  Table [dbo].[CarPoolList]    Script Date: 2022/07/15 12:54:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CarPoolList](
	[Destination] [varchar](50) NOT NULL,
	[ArrivalTime] [datetime] NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[Origin] [varchar](50) NULL,
	[AvailableSeats] [int] NOT NULL,
	[Owner] [varchar](50) NOT NULL,
	[Notes] [varchar](max) NULL,
	[CarPoolId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_CarPoolList] PRIMARY KEY CLUSTERED 
(
	[CarPoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CarPoolList]  WITH CHECK ADD  CONSTRAINT [FK_CarPoolList_CarPoolUser] FOREIGN KEY([Id])
REFERENCES [dbo].[CarPoolUser] ([Id])
GO

ALTER TABLE [dbo].[CarPoolList] CHECK CONSTRAINT [FK_CarPoolList_CarPoolUser]
GO


