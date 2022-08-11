using CoMute.Core.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;

namespace CoMute.Core.Tests.Domain
{
    [TestFixture]
    public class JoinCarPoolsOpportunityTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new JoinCarPoolsOpportunity());
            //---------------Test Result -----------------------
        }

        [TestCase("JoinCarPoolsOpportunityId", typeof(Guid))]
        [TestCase("CarPoolId", typeof(Guid))]
        [TestCase("UserId", typeof(Guid))]
        [TestCase("DateJoined", typeof(DateTime))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(JoinCarPoolsOpportunity);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}