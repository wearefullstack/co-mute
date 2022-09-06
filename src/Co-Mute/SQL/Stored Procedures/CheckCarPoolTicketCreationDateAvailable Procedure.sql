USE [Co-mute]
GO

/****** Object:  StoredProcedure [dbo].[CheckCarPoolTicketCreationDateAvailable]    Script Date: 9/6/2022 12:38:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CheckCarPoolTicketCreationDateAvailable](@StartTime DATETIME, @EndTime DATETIME, @UserId int)      
AS      
      
 SELECT * INTO #temp from      
 (      
  SELECT DepartureTime, ExpectedArrivalTime FROM CarPoolTickets WHERE OwnerId = @UserId      
  UNION       
  SELECT DepartureTime, ExpectedArrivalTime FROM CarPoolTicketAllocation WHERE CarPoolTicketPassengerId = @UserId      
 ) as tt      
      
 IF EXISTS(SELECT 1 FROM #temp t WHERE @StartTime >= t.DepartureTime AND @StartTime <= t.ExpectedArrivalTime OR @EndTime >= t.DepartureTime AND @EndTime <= t.ExpectedArrivalTime)      
 BEGIN      
  RETURN -1      
 END      
 ELSE      
  RETURN 1      
      
 DROP TABLE #temp
GO

