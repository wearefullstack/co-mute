CREATE TABLE [AccessControl].[User] (
    [UserId]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Surname]     VARCHAR (50)  NOT NULL,
    [Phone]       VARCHAR (12)  NULL,
    [Email]       VARCHAR (50)  NOT NULL,
    [Pasword]     VARCHAR (50)  NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

