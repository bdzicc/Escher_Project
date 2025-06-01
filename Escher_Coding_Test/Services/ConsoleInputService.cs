using Escher_Coding_Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escher_Coding_Test.Services
{
    public class ConsoleInputService : IUserInputService
    {
        public string GetUserInput(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input. Please try again:");
                input = Console.ReadLine();
            }
            return input.Trim();
        }
    }
}
