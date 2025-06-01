using Escher_Coding_Test.Interfaces;
using Escher_Coding_Test.Models;
using Escher_Coding_Test.Repositories;
using Escher_Coding_Test.Services;
using Escher_Coding_Test.Tests;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Text;


namespace Escher_Coding_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputService = new ConsoleInputService();
            var personRepository = new PersonRepository();
            var registrationService = new PersonRegistrationService(inputService, personRepository);

            try
            {
                registrationService.Register();
                Console.WriteLine("Registration successful!");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}