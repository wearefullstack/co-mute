using CoMute.Core.Domain;
using CoMute.DB.Repository;
using CoMute.Tests.Common.Builders.Domain;
using CoMute.Tests.Common.Helpers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CoMute.DB.Tests.Repository
{
    [TestFixture]
    public class CarPoolOpportunityRepositoryTests
    {
        [Test]
        public void Contruct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new CarPoolOpportunityRepository(Substitute.For<ICoMuteDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenCoMuteDbContextIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new CarPoolOpportunityRepository(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("coMuteDbContext", ex.ParamName);
        }

        [Test]
        public void GetAllCarPools_GivenNoItemsExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            var coMuteDbContext = CreateCoMuteDbContext();
            var carPoolOpportunityRepository = CreateCarPoolsOpportunityRepository(coMuteDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = carPoolOpportunityRepository.GetAllCarPools();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetAllCarPools_GivenItemsExistInRepo_ShouldReturnListOfItems()
        {
            //---------------Set up test pack-------------------
            var carPools = new List<CarPoolOpportunity>();
            var dbSet = CreateDbSetWithAddRemoveSupport(carPools);
            var dbContext = CreateCoMuteDbContext(dbSet);
            var repository = CreateCarPoolsOpportunityRepository(dbContext);

            var opportunity = CarPoolOpportunityBuilder.BuildRandom();
            carPools.Add(opportunity);
            dbSet.GetEnumerator().Returns(info => carPools.GetEnumerator());
            dbContext.CarPoolOpportunities.Returns(info => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = repository.GetAllCarPools();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void Save_GivenNullItem_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var dbContext = CreateCoMuteDbContext();
            var repository = CreateCarPoolsOpportunityRepository(dbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                repository.Save(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("carPoolOpportunity", ex.ParamName);
        }

        [Test]
        public void Save_GivenItem_ShouldSaveItem()
        {
            //---------------Set up test pack-------------------
            var item = CarPoolOpportunityBuilder.BuildRandom();
            var items = new List<CarPoolOpportunity>();

            var dbSet = CreateDbSetWithAddRemoveSupport(items);
            var dbContext = CreateCoMuteDbContext(dbSet);
            var itemsRepository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            itemsRepository.Save(item);
            //---------------Test Result -----------------------
            var itemsFromRepo = itemsRepository.GetAllCarPools();
            CollectionAssert.Contains(itemsFromRepo, item);
        }

        [Test]
        public void Save_GivenValidItem_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var item = CarPoolOpportunityBuilder.BuildRandom();
            var items = new List<CarPoolOpportunity>();
            var dbSet = CreateDbSetWithAddRemoveSupport(items);
            var dbContext = CreateCoMuteDbContext(dbSet);
            var repository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            repository.Save(item);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void GetById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var dbContext = CreateCoMuteDbContext();
            var repository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                repository.GetById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("carPoolOpportunityId", ex.ParamName);
        }

        [Test]
        public void GetById_GivenValidId_ShoulReturnItemWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var item = new CarPoolOpportunityBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<CarPoolOpportunity> { item };
            var dbContext = CreateCoMuteDbContext(dbSet);
            var repository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = repository.GetById(item.CarPoolId);
            //---------------Test Result -----------------------
            Assert.AreEqual(item, result);
        }

        [Test]
        public void DeleteCarPoolOpportunity_GivenItemNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var dbContext = CreateCoMuteDbContext();
            var repository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                repository.DeleteCarPoolOpportunity(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("carPoolOpportunity", ex.ParamName);
        }

        [Test]
        public void DeleteCarPoolOpportunity_GivenValidItemId_ShouldDeleteItem()
        {
            //---------------Set up test pack-------------------
            var items = new List<CarPoolOpportunity>();
            var item = CarPoolOpportunityBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(items);
            var dbContext = CreateCoMuteDbContext(dbSet);
            var repository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            repository.DeleteCarPoolOpportunity(item);
            //---------------Test Result -----------------------
            var itemsFromRepo = repository.GetAllCarPools();
            CollectionAssert.DoesNotContain(itemsFromRepo, item);
        }

        [Test]
        public void DeleteCarPoolOpportunity_GivenValidItem_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var items = new List<CarPoolOpportunity>();
            var item = CarPoolOpportunityBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(items);
            var dbContext = CreateCoMuteDbContext(dbSet);
            var repository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            repository.DeleteCarPoolOpportunity(item);
            //---------------Test Result -----------------------
           dbContext.Received().SaveChanges();
        }

        [Test]
        public void Update_GivenInvalidExistingItem_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var dbContext = CreateCoMuteDbContext();
            var repository = CreateCarPoolsOpportunityRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => repository.Update(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("carPoolOpportunity", ex.ParamName);
        }
            

        private static CarPoolOpportunityRepository CreateCarPoolsOpportunityRepository(ICoMuteDbContext dbContext)
        {
            return new CarPoolOpportunityRepository(dbContext);
        }

        private static IDbSet<CarPoolOpportunity> CreateDbSetWithAddRemoveSupport(List<CarPoolOpportunity> items)
        {
            var dbSet = Substitute.For<IDbSet<CarPoolOpportunity>>();

            dbSet.Add(Arg.Any<CarPoolOpportunity>()).Returns(info =>
            {
                items.Add(info.ArgAt<CarPoolOpportunity>(0));
                return info.ArgAt<CarPoolOpportunity>(0);
            });

            dbSet.Remove(Arg.Any<CarPoolOpportunity>()).Returns(info =>
            {
                items.Remove(info.ArgAt<CarPoolOpportunity>(0));
                return info.ArgAt<CarPoolOpportunity>(0);
            });

            dbSet.GetEnumerator().Returns(info => items.GetEnumerator());
            return dbSet;
        }

        private static ICoMuteDbContext CreateCoMuteDbContext(IDbSet<CarPoolOpportunity> dbSet = null)
        {
            if (dbSet == null) dbSet = CreateDbSetWithAddRemoveSupport(new List<CarPoolOpportunity>());
            var dbContext = Substitute.For<ICoMuteDbContext>();
            dbContext.CarPoolOpportunities.Returns(info => dbSet);
            return dbContext;
        }
    }
}