﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Interfaces
{
    public interface IPersonRepository
    {
        void SavePersonToFile(Models.Person person);
    }
}
