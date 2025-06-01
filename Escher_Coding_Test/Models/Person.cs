using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public bool ParentAuthorization { get; set; }
        public Person? Spouse { get; set; }

        public int age => CalculateAge();

        private int CalculateAge()
        {
            var dateToday = DateTime.Today;
            int age = dateToday.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > dateToday.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
