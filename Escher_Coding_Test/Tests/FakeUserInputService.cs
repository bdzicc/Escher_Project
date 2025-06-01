using Escher_Coding_Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Tests
{
    public class FakeUserInputService : IUserInputService
    {
        private readonly Queue<string> _responses;

        public FakeUserInputService(IEnumerable<string> responses)
        {
            _responses = new Queue<string>(responses);
        }

        public string GetUserInput(string message)
        {
            return _responses.Dequeue();
        }
    }
}
