using Escher_Coding_Test.Models;
using Escher_Coding_Test.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Tests
{
    [TestFixture]
    public class RegistrationTests
    {
        [Test]
        public void Register_SinglePerson_ShouldSaveSuccessfully()
        {
            //Arrange
            var personAge = 22;
            var responses = new List<string>
            {
                "Jane",                                                     // First name
                "Doe",                                                      // Surname
                DateTime.Today.AddYears(-personAge).ToString("MM-dd-yyyy"), // DOB
                "Single"                                                    // Marital status
            };

            var fakeInputService = new FakeUserInputService(responses);
            var fakePersonRepo = new FakePersonRepository();
            var registrationService = new PersonRegistrationService(fakeInputService, fakePersonRepo);

            //Act
            registrationService.Register();

            //Assert
            Assert.IsNotNull(fakePersonRepo.SavedPerson);
            Assert.AreEqual("Jane", fakePersonRepo.SavedPerson.FirstName);
            Assert.AreEqual("Doe", fakePersonRepo.SavedPerson.Surname);
            Assert.AreEqual(DateTime.Today.AddYears(-personAge), fakePersonRepo.SavedPerson.DateOfBirth);
            Assert.AreEqual("Single", fakePersonRepo.SavedPerson.MaritalStatus);
            Assert.AreEqual(false, fakePersonRepo.SavedPerson.ParentAuthorization);
            Assert.AreEqual(personAge, fakePersonRepo.SavedPerson.age);
            Assert.IsNull(fakePersonRepo.SavedPerson.Spouse);
        }

        [Test]
        public void Register_MarriedPerson_ShouldSaveSuccessfullyWithSpouse()
        {
            //Arrange
            var personAge = 32;
            var spouseAge = 30;
            var responses = new List<string>
            {
                "John",                                                     // First name
                "Smith",                                                    // Surname
                DateTime.Today.AddYears(-personAge).ToString("MM-dd-yyyy"), // DOB
                "Married",                                                  // Marital status
                "Jill",                                                     //Spouse first name
                "Smith",                                                    //Spouse surname
                DateTime.Today.AddYears(-spouseAge).ToString("MM-dd-yyyy")  //Spouse DOB
            };
            
            var fakeInputService = new FakeUserInputService(responses);
            var fakePersonRepo = new FakePersonRepository();
            var registrationService = new PersonRegistrationService(fakeInputService, fakePersonRepo);

            //Act
            registrationService.Register();

            //Assert
            Assert.IsNotNull(fakePersonRepo.SavedPerson);
            Assert.AreEqual("John", fakePersonRepo.SavedPerson.FirstName);
            Assert.AreEqual("Smith", fakePersonRepo.SavedPerson.Surname);
            Assert.AreEqual(DateTime.Today.AddYears(-personAge), fakePersonRepo.SavedPerson.DateOfBirth);
            Assert.AreEqual("Married", fakePersonRepo.SavedPerson.MaritalStatus);
            Assert.AreEqual(false, fakePersonRepo.SavedPerson.ParentAuthorization);
            Assert.AreEqual(personAge, fakePersonRepo.SavedPerson.age);
            Assert.IsNotNull(fakePersonRepo.SavedPerson.Spouse);

            Assert.AreEqual("Jill", fakePersonRepo.SavedPerson.Spouse.FirstName);
            Assert.AreEqual("Smith", fakePersonRepo.SavedPerson.Spouse.Surname);
            Assert.AreEqual(DateTime.Today.AddYears(-spouseAge), fakePersonRepo.SavedPerson.Spouse.DateOfBirth);
            Assert.AreEqual("Married", fakePersonRepo.SavedPerson.Spouse.MaritalStatus);
            Assert.AreEqual(false, fakePersonRepo.SavedPerson.Spouse.ParentAuthorization);
            Assert.AreEqual(spouseAge, fakePersonRepo.SavedPerson.Spouse.age);
        }

        [Test]
        public void Register_Under16_ShouldThrowException()
        {
            //Arrange
            var personAge = 15;
            var responses = new List<string>
            {
                "Kiddo",                                                    // First name
                "Young",                                                    // Surname
                DateTime.Today.AddYears(-personAge).ToString("MM-dd-yyyy"), // DOB
            };

            var fakeInputService = new FakeUserInputService(responses);
            var fakePersonRepo = new FakePersonRepository();
            var registrationService = new PersonRegistrationService(fakeInputService, fakePersonRepo);

            //Act
            var errorMessage = string.Empty;
            try
            {
                registrationService.Register();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            //Assert
            Assert.AreEqual("Sorry, your registration has been denied. Must be 16 or older.", errorMessage);
        }

        [Test]
        public void Register_UnderAge18WithParentConsent_ShouldSaveWithParentConsent()
        {
            //Arrange
            var personAge = 17;
            var responses = new List<string>
            {
                "Paul",                                                     // First name
                "Jones",                                                    // Surname
                DateTime.Today.AddYears(-personAge).ToString("MM-dd-yyyy"), // DOB
                "Yes",                                                      //Parent Auth
                "Single"                                                    //Marital Status
            };

            var fakeInputService = new FakeUserInputService(responses);
            var fakePersonRepo = new FakePersonRepository();
            var registrationService = new PersonRegistrationService(fakeInputService, fakePersonRepo);

            //Act
            registrationService.Register();

            //Assert
            Assert.IsNotNull(fakePersonRepo.SavedPerson);
            Assert.AreEqual("Paul", fakePersonRepo.SavedPerson.FirstName);
            Assert.AreEqual("Jones", fakePersonRepo.SavedPerson.Surname);
            Assert.AreEqual(DateTime.Today.AddYears(-personAge), fakePersonRepo.SavedPerson.DateOfBirth);
            Assert.AreEqual("Single", fakePersonRepo.SavedPerson.MaritalStatus);
            Assert.AreEqual(true, fakePersonRepo.SavedPerson.ParentAuthorization);
            Assert.AreEqual(personAge, fakePersonRepo.SavedPerson.age);
            Assert.IsNull(fakePersonRepo.SavedPerson.Spouse);
        }

        [Test]
        public void Register_UnderAge18WithoutParentConsent_ShouldThrowException()
        {
            //Arrange
            var personAge = 17;
            var responses = new List<string>
            {
                "Dan",                                                      // First name
                "Griffin",                                                  // Surname
                DateTime.Today.AddYears(-personAge).ToString("MM-dd-yyyy"), // DOB
                "No"                                                        //Parent Auth
            };

            var fakeInputService = new FakeUserInputService(responses);
            var fakePersonRepo = new FakePersonRepository();
            var registrationService = new PersonRegistrationService(fakeInputService, fakePersonRepo);

            //Act
            var errorMessage = string.Empty;
            try
            {
                registrationService.Register();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            //Assert
            Assert.AreEqual("Sorry, your registration has been denied. Parental consent required.", errorMessage);
        }
    }
}
