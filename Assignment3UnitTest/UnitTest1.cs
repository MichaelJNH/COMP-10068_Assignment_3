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
using Assignment3;

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
        [ExpectedException(typeof(StatusException))]
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
                    controller.GetImmunization(testImmunization.Id).Result.Value, //GET the original `testImmunization` object
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

                //Get the updated Immunization from the DB and check if the OfficialName changed correctly
                Assert.AreEqual(
                    "OfficialChangedName123",
                    controller.GetImmunization(testImmunization.Id).Result.Value.OfficialName //GET The original `testImmunization` object, which has 1 property changed "OfficialName"                    
                );
            }

            //DELETE
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new ImmunizationController(context);

                //delete the immunization
                await controller.DeleteImmunization(testImmunization.Id);

                await controller.GetImmunization(testImmunization.Id); //Raise exception because it should be deleted.
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusException))]
        public async Task Organization_GetPostUpdateDeleteAsync()
        {
            Organization testOrganization = new Organization()
            {
                Id = Guid.NewGuid(),
                Name = "OrganizationName",
                Type = "Hospital",
                Address = "123 Candycane Lane"
            };

            //POST
            using (var context = new Assignment3Context(contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var controller = new OrganizationController(context);

                //Create the Organization defined by testOrganization
                await controller.PostOrganization(testOrganization);

                //Get the created testOrganization from the DB and check if it's equal to testOrganization
                Assert.AreEqual(
                    controller.GetOrganization(testOrganization.Id).Result.Value, //GET the original `testOrganization` object
                    testOrganization
                );
            }

            //PUT
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new OrganizationController(context);

                Organization testChangedOrganization = new Organization()
                {
                    Id = testOrganization.Id,
                    Name = testOrganization.Name,
                    Type = testOrganization.Type,
                    Address = "123 Candycane Road"
                };

                //Update testOrganization to be testChangedOrganization
                await controller.PutOrganization(testOrganization.Id, testChangedOrganization);

                //Get the updated Organization from the DB and check if the Address changed correctly
                Assert.AreEqual(
                    "123 Candycane Road",
                    controller.GetOrganization(testOrganization.Id).Result.Value.Address //GET The original `testOrganization` object, which has 1 property changed "Address"                    
                );
            }

            //DELETE
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new OrganizationController(context);

                //delete the organization
                await controller.DeleteOrganization(testOrganization.Id);

                await controller.GetOrganization(testOrganization.Id); //Raise exception because it should be deleted.
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusException))]
        public async Task Patient_GetPostUpdateDeleteAsync()
        {
            Patient testPatient = new Patient()
            {
                Id = Guid.NewGuid(),
                FirstName = "Michael",
                LastName = "Helbert",
                DateOfBirth = new DateTime(1999, 06, 04)
            };

            //POST
            using (var context = new Assignment3Context(contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var controller = new PatientController(context);

                //Create the Patient defined by testPatient
                await controller.PostPatient(testPatient);

                //Get the created Patient from the DB and check if it's equal to testPatient
                Assert.AreEqual(
                    controller.GetPatient(testPatient.Id).Result.Value, //GET the original `testPatient` object
                    testPatient
                );
            }

            //PUT
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new PatientController(context);

                Patient testChangedPatient = new Patient()
                {
                    Id = testPatient.Id,
                    FirstName = "Bob",
                    LastName = testPatient.LastName,
                    DateOfBirth = testPatient.DateOfBirth
                };

                //Update testPatient to be testChangedPatient
                await controller.PutPatient(testPatient.Id, testChangedPatient);

                //Get the updated Patient from the DB and check if the Address changed correctly
                Assert.AreEqual(
                    "Bob",
                    controller.GetPatient(testPatient.Id).Result.Value.FirstName //GET The original `testPatient` object, which has 1 property changed "Address"                    
                );
            }

            //DELETE
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new PatientController(context);

                //delete the Patient
                await controller.DeletePatient(testPatient.Id);

                await controller.GetPatient(testPatient.Id); //Raise exception because it should be deleted.
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusException))]
        public async Task Provider_GetPostUpdateDeleteAsync()
        {
            Provider testProvider = new Provider()
            {
                Id = Guid.NewGuid(),
                FirstName = "Kelly",
                LastName = "Shelly",
                LicenseNumber = 321123,
                Address = "550 Cookiedough Drive"
            };

            //POST
            using (var context = new Assignment3Context(contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var controller = new ProviderController(context);

                //Create the Provider defined by testProvider
                await controller.PostProvider(testProvider);

                //Get the created Provider from the DB and check if it's equal to testProvider
                Assert.AreEqual(
                    controller.GetProvider(testProvider.Id).Result.Value, //GET the original `testProvider` object
                    testProvider
                );
            }

            //PUT
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new ProviderController(context);

                Provider testChangedProvider = new Provider()
                {
                    Id = testProvider.Id,
                    FirstName = testProvider.FirstName,
                    LastName = "Delly",
                    LicenseNumber = testProvider.LicenseNumber,
                    Address = testProvider.Address
                };

                //Update testProvider to be testChangedProvider
                await controller.PutProvider(testProvider.Id, testChangedProvider);

                //Get the updated Provider from the DB and check if the Address changed correctly
                Assert.AreEqual(
                    "Delly",
                    controller.GetProvider(testProvider.Id).Result.Value.LastName //GET The original `testProvider` object, which has 1 property changed "lastName"                    
                );
            }

            //DELETE
            using (var context = new Assignment3Context(contextOptions))
            {
                var controller = new ProviderController(context);

                //delete the Patient
                await controller.DeleteProvider(testProvider.Id);

                await controller.GetProvider(testProvider.Id); //Raise exception because it should be deleted.
            }
        }
    }
}
