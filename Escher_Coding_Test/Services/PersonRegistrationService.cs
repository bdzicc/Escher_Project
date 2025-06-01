using Escher_Coding_Test.Interfaces;
using Escher_Coding_Test.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Services
{
    public class PersonRegistrationService : IRegistrationService
    {
        private readonly IUserInputService _input;
        private readonly IPersonRepository _personRepo;

        public PersonRegistrationService(IUserInputService input, IPersonRepository personRepository)
        {
            _input = input;
            _personRepo = personRepository;
        }

        public void Register()
        {
            var firstName = _input.GetUserInput("Please enter your first name:");
            var surname = _input.GetUserInput("Please enter your surname:");
            var dateOfBirth = GetValidDate(_input.GetUserInput("Please enter your date of birth(mm-dd-yyyy):"));

            var person = new Person();
            person.FirstName = firstName;
            person.Surname = surname;
            person.DateOfBirth = dateOfBirth;
            person.Id = Guid.NewGuid().ToString();

            if (person.age < 16)
            {
                throw new Exception("Sorry, your registration has been denied. Must be 16 or older.");
            }

            if (person.age < 18)
            {
                var parentAuth = _input.GetUserInput("My parents allow registration (yes/no)");
                person.ParentAuthorization = string.Equals(parentAuth, "yes", StringComparison.InvariantCultureIgnoreCase);

                if (!person.ParentAuthorization)
                {
                    throw new Exception("Sorry, your registration has been denied. Parental consent required.");
                }
            }

            var maritalStatus = _input.GetUserInput("Please enter your marital status:");
            person.MaritalStatus = maritalStatus;

            if (string.Equals(maritalStatus, "married", StringComparison.InvariantCultureIgnoreCase))
            {
                var firstNameSpouse = _input.GetUserInput("Please enter spouse first name:");
                var surnameSpouse = _input.GetUserInput("Please enter spouse surname:");
                var dateOfBirthSpouse = GetValidDate(_input.GetUserInput("Please enter spouse date of birth(mm-dd-yyyy):"));

                person.Spouse = new Person();
                person.Spouse.FirstName = firstNameSpouse;
                person.Spouse.Surname = surnameSpouse;
                person.Spouse.DateOfBirth = dateOfBirthSpouse;
                person.Spouse.MaritalStatus = "Married";
                person.Spouse.Id = Guid.NewGuid().ToString();
            }
            _personRepo.SavePersonToFile(person);
        }

        private DateTime GetValidDate(string strDate)
        {
            DateTime validDate;
            string[] formats = { "MM-dd-yyyy", "M-d-yyyy", "MM/dd/yyyy", "M/d/yyyy", "yyyy-MM-dd" };
            while (!DateTime.TryParseExact(strDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out validDate))
            {
                Console.WriteLine("Invalid date. Please try again:");
                strDate = Console.ReadLine() ?? "";
            }
            return validDate;
        }
    }
}
