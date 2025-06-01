using Escher_Coding_Test.Interfaces;
using Escher_Coding_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Tests
{
    public class FakePersonRepository : IPersonRepository
    {
        public Person SavedPerson;
        public void SavePersonToFile(Person person)
        {
            SavedPerson = person;
        }
    }
}
