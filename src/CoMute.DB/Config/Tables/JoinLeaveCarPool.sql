CREATE TABLE [Config].[JoinLeaveCarPool] (
    [JoinLeaveCarPoolId] INT           IDENTITY (1, 1) NOT NULL,
    [UserId]             INT           NOT NULL,
    [CarPoolId]          INT           NOT NULL,
    [OwnerLeader]        VARCHAR (50)  NULL,
    [IsActive]           BIT           NULL,
    [DateAdded]          DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([JoinLeaveCarPoolId] ASC),
    FOREIGN KEY ([CarPoolId]) REFERENCES [Config].[CarPool] ([CarPoolId]),
    FOREIGN KEY ([UserId]) REFERENCES [AccessControl].[User] ([UserId])
);

