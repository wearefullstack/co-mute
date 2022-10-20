CREATE TABLE [Config].[CarPool] (
    [CarPoolId]           INT           IDENTITY (1, 1) NOT NULL,
    [UserId]              INT           NOT NULL,
    [DepartureTime]       DATETIME2 (7) NOT NULL,
    [ExpectedArrivalTime] DATETIME2 (7) NOT NULL,
    [Origin]              VARCHAR (50)  NOT NULL,
    [DaysAvailable]       INT           NOT NULL,
    [Destination]         VARCHAR (50)  NOT NULL,
    [AvailableSeats]      INT           NOT NULL,
    [OwnerLeader]         VARCHAR (50)  NOT NULL,
    [Notes]               VARCHAR (50)  NULL,
    [DateCreated]         DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([CarPoolId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [AccessControl].[User] ([UserId])
);

