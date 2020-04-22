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
            var passed = new List<Student>();
            var failed = new List<Student>();

            Dictionary<string,long> speed = new Dictionary<string, long>();
            speed.Add("overall_sort", 0);
            speed.Add("2files_create", 0);
            speed.Add("sort2files", 0);
            speed.Add("file_read",0);


            
            var flag = true;

            while (flag)
            {
                Console.WriteLine("--------------Release v1.0----------------");
                Console.WriteLine(
                    "1.{0}\n2.{1}\n3.{2}\n4.{3}\n5.{4}\n6.{5}\n7.{6}\n8.{7}\n9.{8}\n10.{9}\n11.{10}\n{11}\n",
                    "Create student", "Print students (Average)", "Print students (Median)", "Read from file",
                    "Create hundreds", "Sort 2 files",
                    "Speed Analysis for current list", "Container Testing", "Strategies comparison",
                    "Five Tests Performance",
                    "Exit", "Your choice:");

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
                        methods.createHundreds(10000);
                        methods.createHundreds(100000);
                        methods.createHundreds(1000000);
                        methods.createHundreds(10000000);
                        break;
                    }
                    case "6":
                    {
                        methods.Sort2Files(students,speed,passed,failed);
                        break;
                    }
                    case "7":
                    {
                        methods.SpeedAnalysis(students,speed,passed,failed);
                        break;
                    }
                    case "8":
                    {
                        methods.ContainerTesting("students.txt");
                        methods.ContainerTesting("10000Students.txt");
                        methods.ContainerTesting("100000Students.txt");
                        methods.ContainerTesting("1000000Students.txt");

                        break;
                    }
                    case "9":
                    {
                        methods.CompareStrategies("students.txt");
                        break;
                    }
                    case "10":
                    {
                       methods.FivePerformanceTests(students,speed,passed,failed);
                       break;
                    }
                    case "11":
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