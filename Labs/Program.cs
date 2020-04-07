using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace Labs
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            var methods = new Methods();


            var students = new List<Student>();
            var flag = true;

            while (flag)
            {
                Console.WriteLine("--------------Release v0.1----------------");
                Console.WriteLine("1.{0}\n2.{1}\n3.{2}\n4.{3}\n5.{4}\n {5}\n",
                    "Create student", "Print students (Average)", "Print students (Median)","Read from file", "Exit",
                    "Your choice:");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    {
                        students.Add(methods.CreateStudent());
                        break;
                    }
                    case "2":
                    {
                        methods.PrintAverage(students);
                        break;
                    }
                    case "3":
                    {
                        methods.PrintMedian(students);
                        break;
                    }
                    case "4":
                    {
                        methods.ReadData(students);
                        break;
                    }
                    case "5":
                    {
                        flag = false;
                        break;
                    }
                }

                {
                }
            }
        }

        
        
    }
}