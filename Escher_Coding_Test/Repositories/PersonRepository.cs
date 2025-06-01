using Escher_Coding_Test.Interfaces;
using Escher_Coding_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public void SavePersonToFile(Person person)
        {
            var filePath = "People.txt";
            var spouseFilePath = string.Empty;

            if (person.Spouse != null)
            {
                Directory.CreateDirectory("spouses");
                spouseFilePath = Path.Combine("spouses", $"{person.Spouse.FirstName.ToLower()}_{person.Spouse.Id}.txt");
                File.AppendAllText(spouseFilePath, FormatPerson(person.Spouse));
            }
            File.AppendAllText(filePath, FormatPerson(person, spouseFilePath));
        }

        private string FormatPerson(Person person, string spouseFilePath = "")
        {
            var personVals = new string[]
            {
                person.Id,
                person.FirstName,
                person.Surname,
                person.DateOfBirth.ToString("MM-dd-yyyy"),
                person.MaritalStatus,
                person.ParentAuthorization ? "yes" : "null",
                spouseFilePath
            };
            return string.Join("|", personVals.Where(x => !string.IsNullOrEmpty(x))) + Environment.NewLine;
        }
    }
}
