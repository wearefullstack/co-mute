USE [Co-mute]
GO

/****** Object:  Table [dbo].[CarPoolTicketStatus]    Script Date: 9/6/2022 12:35:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CarPoolTicketStatus](
	[StatusId] [int] IDENTITY(0,1) NOT NULL,
	[Status] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO CarPoolTicketStatus VALUES('pending')
INSERT INTO CarPoolTicketStatus VALUES('accepted')
INSERT INTO CarPoolTicketStatus VALUES('declined')
INSERT INTO CarPoolTicketStatus VALUES('cancelled')
INSERT INTO CarPoolTicketStatus VALUES('completed')