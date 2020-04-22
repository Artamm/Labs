using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Labs
{
    public class Methods
    {
        
         public  Student CreateStudent()
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
                Console.WriteLine("Enter grade #" + (next + 1)+", write random or stop typing  ");
                    var textGrade = Console.ReadLine();
                    if (textGrade != null && textGrade.Equals(""))
                    {
                        check = false;
                        break;
                    }

                    if (textGrade != null && double.TryParse(textGrade, out double grade) )
                    {
                        if (grade <= 10 & grade > 0)
                        {
                            homeworks.Add(grade);
                            next++;
                        }
                    }
                    if (textGrade.ToUpper().Equals("RANDOM"))
                    {
                        var random = new Random();
                        var randomDouble = Math.Round(random.NextDouble() * 10,2);
                        homeworks.Add(Math.Round(randomDouble,2));
                        Console.WriteLine("your random double: " + randomDouble);
                        next++;
                    }
            }


            double exam;
            Console.WriteLine("Enter exam grade or type random");
            var textExam = Console.ReadLine();
            if (textExam != null && textExam.ToUpper().Equals("RANDOM"))
            {
                Random random = new Random();
                var randomDouble = random.NextDouble() * 10;
                 exam = Math.Round(randomDouble,2);
                Console.WriteLine("Your random double: " + randomDouble);

            }
            if (!double.TryParse(textExam, out  exam) & !textExam.ToUpper().Equals("RANDOM"))
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

         private void formatGrades(List<double> grades)
        {
            if (grades.Count == 0)
            {
                grades.Add(0);
            }
            
        }


        private double GradeAverage(List<double> grades, double exam)
        {
            return grades == null ? Math.Round(exam * 0.7, 2) : Math.Round(grades.Average() * 0.3 + exam * 0.7, 2);
        }

        private double GradeMed(List<double> grades, double exam)
        {
            
            double median = grades.Count % 2 == 0
                ? (grades[grades.Count / 2] + grades[(grades.Count / 2) - 1]) / 2
                : grades[(grades.Count / 2)];
            return  Math.Round((median * 0.3) + (exam * 0.7), 2);
        }

        public  void PrintAverage(List<Student> students)
        {
            var dataTable = new DataTable {TableName = "Grades"};
            dataTable.Columns.Add("#", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Surname", typeof(string));
            dataTable.Columns.Add("Final ponts(Aver.)", typeof(double));

            var number = 1;

            List<Student> sorted = students.OrderBy(o => o.Name1).ToList();
            foreach (var student in sorted)
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
        
        public  void PrintMedian(List<Student> students)
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
        
        public   void ReadData(List<Student> students)
        {
            string line = " ";
            Console.WriteLine("Enter full path to the file for custom file or type students.txt for sample");
            //Format: C:\\Sample.txt
            line = Console.ReadLine();
            
            try 
            {
                StreamReader sr = new StreamReader(line);

                line = sr.ReadLine();

                while (line != null) 
                {
                    students.Add(prepareStudent(line));
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }

                sr.Close();
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception happened: " + e.Message);
            }
            finally 
            {
                Console.WriteLine("End of file");
            }
        }

        private Student prepareStudent(string line)
        {
            string[] raw = line.Split();
            List<double> grades = new List<double>();
            grades.Add(Double.Parse(raw[2]));
            grades.Add(Double.Parse(raw[3]));
            grades.Add(Double.Parse(raw[4]));
            grades.Add(Double.Parse(raw[5]));
            grades.Add(Double.Parse(raw[6]));
            
            double exam = Double.Parse(raw[7]); 
            
            return new Student(raw[0],raw[1],grades,exam,grades.ToArray(),GradeMed(grades,exam),GradeAverage(grades,exam)); 
        }
    }

}
