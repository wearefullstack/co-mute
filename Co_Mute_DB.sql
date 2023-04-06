USE master

--Use this to create the database
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Co_Mute_DB')
DROP DATABASE Co_Mute_DB;
CREATE DATABASE Co_Mute_DB;--Run this part only to create the Student_DB database.
USE Co_Mute_DB;

CREATE TABLE [User] (
    [UserID]   INT IDENTITY(10000,1)  NOT NULL,
    [Name]     VARCHAR (60) NOT NULL,
	[Surname]     VARCHAR (60) NOT NULL,
    [EmailAddress] VARCHAR (60) NOT NULL,
	[PhoneNumber] VARCHAR (60) ,
    [Password] BINARY (64)  NOT NULL,
    PRIMARY KEY (UserID)
);

CREATE TABLE CarPool (
 [CarPoolID] INT IDENTITY(100,2) NOT NULL,
 [DepartureTime] DATETIME NOT NULL,
 [ArrivalTime] DATETIME NOT NULL,
 [Origin] VARCHAR(90) NOT NULL,
 [DaysAvailable] VARCHAR(300) NOT NULL,
 [Destination] VARCHAR(90) NOT NULL,
 [NumOfAvailableSeats] INT NOT NULL,
 [UserID] INT   NOT NULL,
 [Notes] VARCHAR(255),
 PRIMARY KEY([CarPoolID] ASC),
 FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID])
);

CREATE TABLE CarPoolUser (
[CarPoolID] INT NOT NULL,
[UserID] INT NOT NULL,
[UserType] VARCHAR(60) NOT NULL,
PRIMARY KEY([CarPoolID] ASC, [UserID]),
FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]),
FOREIGN KEY ([CarPoolID]) REFERENCES CarPool ([CarPoolID])
)

DROP TABLE CarPool
SELECT * FROM [User]
SELECT * FROM CarPool

CREATE PROCEDURE AddUser(
    @Password VARCHAR(60),
	@EmailAddress VARCHAR(60),
	@PhoneNumber VARCHAR(60),
    @Name VARCHAR(60),
    @Surname VARCHAR(60)
 )   
AS
BEGIN
        INSERT INTO [User]( [Name], Surname, EmailAddress , PhoneNumber, [Password])
        VALUES(@Name, @Surname, @EmailAddress,@PhoneNumber ,HASHBYTES('SHA2_512', @Password))

END;

CREATE PROCEDURE AddCarPool(
    @DepartureTime DATETIME,
	@Origin VARCHAR(60),
    @DaysAvailable VARCHAR(300),
    @Destination VARCHAR(90) ,
    @NumOfAvailableSeats INT ,
    @UserID INT   ,
    @Notes VARCHAR(255)
 )   
AS
BEGIN
 
        DECLARE @CarPoolID INT

        INSERT INTO CarPool( DepartureTime, Origin, DaysAvailable , Destination, NumOfAvailableSeats,UserID,Notes )
        VALUES(@DepartureTime, @Origin, @DaysAvailable,@Destination ,@NumOfAvailableSeats, @UserID, @Notes)

		SELECT @CarPoolID = CarPoolID FROM CarPool WHERE @CarPoolID = CarPoolID

		 INSERT INTO CarPoolUser( CarPoolID, UserID, UserType)
        VALUES(@CarPoolID,@UserID, 'Owner' )

END;