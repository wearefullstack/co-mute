Use Master
GO

If exists (Select * from sys.databases where name = 'CarPool')
Drop Database CarPool
GO

Create Database CarPool
GO

Use CarPool
GO


--------Register---------
Create Table Register
(
Register_ID int primary key identity(1,1) not null,
[Name] varchar(50) not null,
Surname varchar(50) not null,
Phone int,
Email varchar(50) not null,
[Password] varchar(50) not null,

)
go


Create Table User_Car_Pool
(
User_Car_Pool_ID int primary key identity(1,1) not null,
Departure time not null,
Expected_Arrival time not null ,
Origin varchar(50) not null,
[Days] varchar(50) not null,
Destination varchar(50) not null,
Available_Seats int not null,
Number_Of_Passengers int,
Notes varchar(50) ,
Register_ID int references Register(Register_ID),
Date_Created Datetime,
)
go

 Create Table [Status]
 (
Status_ID int primary key identity(1,1),
Status_Description varchar(50) 
 )
 go

 INSERT INTO [Status] ( Status_Description)
VALUES ('Joined');
INSERT INTO [Status] ( Status_Description)
VALUES ('Cancelled');



Create Table Passenger_Pool
(
Passenger_Pool_ID int Primary Key identity(1,1)NOT NULL ,
Register_ID int foreign key references Register (Register_ID),
User_Car_Pool_ID int foreign key references User_Car_Pool (User_Car_Pool_ID),
Status_ID int foreign key references [Status] (Status_ID),
Date_Joined Datetime,
)
go

