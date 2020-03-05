using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Labs
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();


            var students = new List<Student>();
            var flag = true;

            while (flag)
            {
                Console.WriteLine("--------------Release v0.1----------------");
                Console.WriteLine("1.{0}\n2.{1}\n3.{2}\n4.{3}\n{4}\n",
                    "Create student", "Print students (Average)", "Print students (Median)", "Exit",
                    "Your choice:");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    {
                        students.Add(CreateStudent());
                        break;
                    }
                    case "2":
                    {
                        PrintAverage(students);
                        break;
                    }
                    case "3":
                    {
                        PrintMedian(students);
                        break;
                    }
                    case "4":
                    {
                        flag = false;
                        break;
                    }
                }

                {
                }
            }
        }

        private static Student CreateStudent()
        {
            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter surname:");
            var surname = Console.ReadLine();
            
            var homeworks = new List<double>();
            var check = true;
            var next = 0;
            while (check)
            {
                Console.WriteLine("Enter grade #" + (next + 1)+" or stop typing  ");
                    var textGrade = Console.ReadLine();
                    if (textGrade != null && textGrade.Equals(""))
                    {
                        check = false;
                        break;
                    }

                    if (textGrade != null && double.TryParse(textGrade, out double grade))
                    {
                        homeworks.Add(grade);
                        next++;
                    }
            }

          
            
            Console.WriteLine("Enter exam grade:");
            var textExam = Console.ReadLine();
            if (!double.TryParse(textExam, out double exam))
            {
                Console.WriteLine("Wrong type. Exam grade will be 0");
                exam = 0;
            }
            formatGrades(homeworks);
            var numberHomeworks = homeworks.Count;
            var homeworkArray = new double[numberHomeworks];
            homeworkArray = homeworks.ToArray();
            
            return new Student(name, surname, homeworks, exam, homeworkArray, GradeMed(homeworks, exam),
                GradeAverage(homeworks, exam));
        }

        private static void formatGrades(List<double> grades)
        {
            if (grades.Count == 0)
            {
                grades.Add(0);
            }
            
        }
        

        private static double GradeAverage(List<double> grades, double exam)
        {
            return grades == null ? Math.Round(exam * 0.7, 2) : Math.Round(grades.Average() * 0.3 + exam * 0.7, 2);
        }

        private static double GradeMed(List<double> grades, double exam)
        {
            
            double median = grades.Count % 2 == 0
                ? (grades[grades.Count / 2] + grades[(grades.Count / 2) - 1]) / 2
                : grades[(grades.Count / 2)];
            return  Math.Round((median * 0.3) + (exam * 0.7), 2);
        }

        private static void PrintAverage(List<Student> students)
        {
            var dataTable = new DataTable {TableName = "Grades"};
            dataTable.Columns.Add("#", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Surname", typeof(string));
            dataTable.Columns.Add("Final ponts(Aver.)", typeof(double));

            var number = 1;
            foreach (var student in students)
            {
                object[] row = {number, student.Name1, student.Surname1, student.FinalResultAverage};
                dataTable.Rows.Add(row);
                number++;
            }

            Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}|","#", "First Name:", "Last Name:", "Final points(Aver.)");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}| \n", row[0],
                    row[1], row[2], row[3]);
            }
            Console.WriteLine("-------------------------------------------------------------\n");

            
        }
        
        private static void PrintMedian(List<Student> students)
        {
            var dataTable = new DataTable {TableName = "Grades"};
            dataTable.Columns.Add("#", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Surname", typeof(string));
            dataTable.Columns.Add("Final ponts(Median.)", typeof(double));

            var number = 1;
            foreach (var student in students)
            {
                object[] row = {number, student.Name1, student.Surname1, student.FinalResultMedian};
                dataTable.Rows.Add(row);
                number++;
            }
            dataTable.Rows.Add();
            Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}|","#", "First Name:", "Last Name:", "Final points(Med.)");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}|", row[0],
                    row[1], row[2], row[3]);
            }
            Console.WriteLine("-------------------------------------------------------------\n");
            
        }
        
    }
}