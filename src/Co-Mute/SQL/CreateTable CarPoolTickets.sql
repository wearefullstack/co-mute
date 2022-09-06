USE [Co-mute]
GO

/****** Object:  Table [dbo].[CarPoolTickets]    Script Date: 9/6/2022 12:34:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CarPoolTickets](
	[CarPoolTicketId] [int] IDENTITY(0,1) NOT NULL,
	[OwnerId] [int] NOT NULL,
	[Origin] [varchar](50) NOT NULL,
	[Destination] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ExpectedArrivalTime] [datetime] NOT NULL,
	[AvailableSeats] [int] NOT NULL,
	[DaysAvailable] [int] NULL,
	[Notes] [varchar](500) NULL,
	[CarPoolTicketsStatus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CarPoolTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

