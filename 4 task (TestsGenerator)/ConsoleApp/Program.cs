using System;
using Core.TestGenerator.Implementations;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new NUnitTestGenerator(10, 10, 10);
            generator.GenerateTestsAsync(@"z:\BSUIR\mpp4\input",
                @"z:\BSUIR\mpp4\output").Wait();
        }
    }
}