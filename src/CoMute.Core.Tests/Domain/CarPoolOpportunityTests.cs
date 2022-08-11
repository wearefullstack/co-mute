using CoMute.Core.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;

namespace CoMute.Core.Tests.Domain
{
    [TestFixture]
    public class CarPoolOpportunityTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new CarPoolOpportunity());
            //---------------Test Result -----------------------
        }

        [TestCase("CarPoolId", typeof(Guid))]        
        [TestCase("DepartureTime", typeof(DateTime))]
        [TestCase("ExpectedArrivalTime", typeof(DateTime))]
        [TestCase("Origin", typeof(string))]
        [TestCase("DaysAvailable", typeof(int))]
        [TestCase("Destination", typeof(string))]
        [TestCase("AvailableSeats", typeof(int))]
        [TestCase("Notes", typeof(string))]
        [TestCase("OwnerOrLeader", typeof(string))]
        [TestCase("DateCreated", typeof(DateTime))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(CarPoolOpportunity);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}