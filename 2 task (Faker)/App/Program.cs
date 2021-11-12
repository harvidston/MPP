using System;
using Lib;

namespace App
{
    public static class Program
    {
        public static void Main()
        {
            FakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<MyExample, string, AlgorithmMethods>(example => example.aString);
            Faker faker = new Faker(fakerConfig);
            for (int i = 0; i < 10; i++)
                Console.WriteLine("--------------------------------------------------\n" +
                                  Formatter.Format(faker.Create<MyExample>().ToString())
                );
        }
    }
}