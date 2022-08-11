using CoMute.Core.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Core.Tests.Domain
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new User());
            //---------------Test Result -----------------------
        }

        [TestCase("UserId", typeof(Guid))]
        [TestCase("Name", typeof(string))]
        [TestCase("Surname", typeof(string))]
        [TestCase("EmailAddress", typeof(string))]
        [TestCase("PhoneNumber", typeof(string))]
        [TestCase("PasswordHash", typeof(byte[]))]
        [TestCase("PasswordSalt", typeof(byte[]))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(User);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}