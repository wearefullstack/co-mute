USE [Co-mute]
GO

/****** Object:  Table [dbo].[CarPoolTicketAllocation]    Script Date: 9/6/2022 12:34:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CarPoolTicketAllocation](
	[CarPoolTicketAllocationId] [int] IDENTITY(1,1) NOT NULL,
	[CarPoolTicketId] [int] NULL,
	[CarPoolTicketPassengerID] [int] NULL,
	[PassengerNote] [varchar](500) NULL,
	[DepartureTime] [datetime] NULL,
	[ExpectedArrivalTime] [datetime] NULL,
	[CarPoolTicketAllocationStatus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CarPoolTicketAllocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CarPoolTicketAllocation]  WITH CHECK ADD FOREIGN KEY([CarPoolTicketId])
REFERENCES [dbo].[CarPoolTickets] ([CarPoolTicketId])
GO

