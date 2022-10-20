using CoMute.BE;
using CoMute.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CoMute.Test
{
    [TestClass]
    public class CoMuteTest
    {
        //Add Car Pool Test Method
        [TestMethod]
        public void AddCarPool()
        {
            CarPoolLogic carPoolLogic = new CarPoolLogic();
            CarPool carPool = new CarPool();

            carPool.UserId = 1;
            carPool.AvailableSeats = 12;
            carPool.DaysAvailable = 31;
            carPool.Destination = "La Lucia";
            carPool.Origin = "Pinetown";
            carPool.OwnerLeader = "Sithembiso";
            carPool.Notes = "Test CarPool saves or not";
            carPool.DepartureTime = DateTime.Now;
            carPool.ExpectedArrivalTime = DateTime.Now.AddDays(1);
            carPoolLogic.AddCarPool(carPool);
        }
    }
}
