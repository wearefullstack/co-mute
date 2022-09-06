---------------------SQL------------------------------------
Access the 'SQL' Folder
open the following files on SSMS and run in that order:

Create co-mute db 

ASSUME THE FOLLOWING IS PRECEEDED WITH 'CreateTable':

CreateCarPoolTickets
CreateCarPoolTicketAllocation
Users
Owners
CarPoolTicketStatus

MOVE TO THE 'Stored Procedures' FOLDER AND RUN EACH PROCEDURE.

-------------------------------REACT---------------------------------------------

run 'npm i' <- on react to install dependencies 

REACT dotenv not working with genereated WebApi-React-SPA-template

ADD JOB that runs Daily
	'UPDATE CarPoolTickets SET DaysAvailable = DaysAvailable -1 WHERE DaysAvailable > 0'
https://i.stack.imgur.com/MViKk.gif -> activate job to run daily


NO NEED TO LOOK AT .env FILE FOR LINKS, THEY DONT WORK WITH WEB-API-REACT-TEMPLATE
---------------------------WEB-API-----------------------------------------------------------

IN NUGET CONSOLE  TRY
Update-Package -reinstall

































































































































































would love to have put more time in to this. it was fun learning these things :)
