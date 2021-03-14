using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment3.Models;
using Assignment3.Controllers;
using System.Collections.Generic;
using Assignment3.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Assignment3UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        DbContextOptions<Assignment3Context> contextOptions;

        public UnitTest1()
        {
            string testDB = "Server=(localdb)\\MSSQLLocalDB;Database=Assignment3Context;Trusted_Connection=True;MultipleActiveResultSets=true";

            this.contextOptions = new DbContextOptionsBuilder<Assignment3Context>()
                .UseSqlServer(testDB)
                .EnableSensitiveDataLogging().Options;
        }

        [TestMethod]
        public async Task Immunization_GetPostUpdateDeleteAsync()
        {

            Immunization testImmunization = new Immunization()
            {
                Id = Guid.NewGuid(),
                OfficialName = "OfficialName123",
                TradeName = "TradeName321",
                LotNumber = "a1234",
                ExpirationDate = new DateTime(2005, 06, 04),
            };

            //POST
            using (var context = new Assignment3Context(contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var controller = new ImmunizationController(context);

                //Create the Immunization defined by testImmunization
                await controller.PostImmunization(testImmunization);

                //Get the created Immunization from the DB and check if it's equal to testImmunization
                Assert.AreEqual(
                    controller.GetImmunization(testImmunization.Id).Result.Value,
                    testImmunization
                );
            }

            //PUT
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new ImmunizationController(context);

                Immunization testChangedImmunization = new Immunization()
                {
                    Id = testImmunization.Id,
                    OfficialName = "OfficialChangedName123",
                    TradeName = testImmunization.TradeName,
                    LotNumber = testImmunization.LotNumber,
                    ExpirationDate = testImmunization.ExpirationDate,
                };

                //Update testImmunization to be testChangedImmunization
                await controller.PutImmunization(testImmunization.Id, testChangedImmunization);

                //Get the updated Immunization from the DB and check if the OfficialName is not the same as testImmunization
                Assert.AreEqual(
                    "OfficialChangedName123",
                    controller.GetImmunization(testImmunization.Id).Result.Value.OfficialName //The original `testImmunization` object, which has 1 property changed "OfficialName"                    
                );
            }
        }
    }
}
