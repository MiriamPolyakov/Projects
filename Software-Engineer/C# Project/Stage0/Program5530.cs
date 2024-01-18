// See https://aka.ms/new-console-template for more information
using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome5530();
            Welcome7773();
            Console.ReadKey();
        }

        static partial void Welcome7773();
        private static void Welcome5530()
        {
            Console.WriteLine("Enter your name: ");
            string name_ = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name_);
        }

       
    }
}
