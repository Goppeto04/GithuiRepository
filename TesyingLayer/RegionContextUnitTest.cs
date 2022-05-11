using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingLayer
{
    public class RegionContextUnitTest
    {
        private HobbyDbContext dbContext;
        private RegionContext regionContext;
        DbContextOptionsBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            dbContext = new HobbyDbContext(builder.Options);
            regionContext = new RegionContext(dbContext);
        }

        [Test]
        public void TestCreateRegion()
        {
            int regionsBefore = regionContext.ReadAll().Count();

            regionContext.Create(new Region("Plovdiv"));

            int regionsAfter = regionContext.ReadAll().Count();

            Assert.IsTrue(regionsBefore != regionsAfter);
        }

        [Test]
        public void TestDeleteRegion()
        {
            regionContext.Create(new Region("Delete this region"));

            int regionsBeforeDeletion = regionContext.ReadAll().Count();

            regionContext.Delete(1);

            int regionsAfterDeletion = regionContext.ReadAll().Count();

            Assert.AreNotEqual(regionsBeforeDeletion, regionsAfterDeletion);
        }

        [Test]
        public void TestReadRegion()
        {
            regionContext.Create(new Region("Kazanluk"));

            Region Area = regionContext.Read(1);

            Assert.That(Area != null, "There is no record with id 1!");
        }

        [Test]
        public void TestUpdateArea()
        {
            regionContext.Create(new Region("Varna"));

            Region region = regionContext.Read(1);

            region.Name = "Sopot";

            regionContext.Update(region);

            Region region1 = regionContext.Read(1);

            Assert.IsTrue(region1.Name == "Sopot", "RegionUpdate() does not change name!");
        }
    }
}
