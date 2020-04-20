using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Labs
{
    public class Methods
    {
        public Student CreateStudent()
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
                Console.WriteLine("Enter grade #" + (next + 1) + ", write random or stop typing  ");
                var textGrade = Console.ReadLine();
                if (textGrade != null && textGrade.Equals(""))
                {
                    check = false;
                    break;
                }

                if (textGrade != null && double.TryParse(textGrade, out double grade))
                {
                    if (grade < 10 & grade > 0)
                    {
                        homeworks.Add(grade);
                        next++;
                    }
                }

                if (textGrade.ToUpper().Equals("RANDOM"))
                {
                    var random = new Random();
                    var randomDouble = Math.Round(random.NextDouble() * 10, 2);
                    homeworks.Add(Math.Round(randomDouble, 2));
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
                exam = Math.Round(randomDouble, 2);
                Console.WriteLine("Your random double: " + randomDouble);
            }

            if (!double.TryParse(textExam, out exam) & !textExam.ToUpper().Equals("RANDOM"))
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
            return Math.Round((median * 0.3) + (exam * 0.7), 2);
        }

        public void PrintAverage(List<Student> students)
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

            Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}|", "#", "First Name:", "Last Name:", "Final points(Aver.)");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}| \n", row[0],
                    row[1], row[2], row[3]);
            }

            Console.WriteLine("-------------------------------------------------------------\n");
        }

        public void PrintMedian(List<Student> students)
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
            Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}|", "#", "First Name:", "Last Name:", "Final points(Med.)");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("|{0,05}|{1,15}|{2,15}|{3,20}|", row[0],
                    row[1], row[2], row[3]);
            }

            Console.WriteLine("-------------------------------------------------------------\n");
        }

        public void ReadData(List<Student> students)
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
            catch (Exception e)
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

            return new Student(raw[0], raw[1], grades, exam, grades.ToArray(), GradeMed(grades, exam),
                GradeAverage(grades, exam));
        }

        public void createHundreds(int number)
        {
            try
            {
                StreamWriter sw = new StreamWriter(number + "Students.txt");

                for (int i = 0; i < number; i++)
                {
                    String add = (i + 1).ToString();
                    Student student = StudentGenerator("Name" + add, "Surname" + add);
                    sw.WriteLine(student.ToString());
                    // students.Add(StudentGenerator("Name"+add,"Surname"+add));
                }


                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing Done.");
            }
        }


        private Student StudentGenerator(string name, string surname)
        {
            var homeworks = new List<double>();
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var randomDouble = Math.Round(random.NextDouble() * 10, 2);
                homeworks.Add(Math.Round(randomDouble, 2));
            }


            var randomDoubleExam = random.NextDouble() * 10;
            var exam = Math.Round(randomDoubleExam, 2);

            formatGrades(homeworks);
            var numberHomeworks = homeworks.Count;
            var homeworkArray = new double[numberHomeworks];
            homeworkArray = homeworks.ToArray();

            return new Student(name, surname, homeworks, exam, homeworkArray, GradeMed(homeworks, exam),
                GradeAverage(homeworks, exam));
        }

        public void Sort2Files(List<Student> students, Dictionary<string, long> hash, List<Student> passed,
            List<Student> failed)
        {
            try
            {
                var fileCreation = System.Diagnostics.Stopwatch.StartNew();

                StreamWriter swPassed = new StreamWriter("Passed.txt");
                StreamWriter swFailed = new StreamWriter("Failed.txt");

                fileCreation.Stop();
                hash["2files_create"] = fileCreation.ElapsedMilliseconds;

                var separate2Files = System.Diagnostics.Stopwatch.StartNew();

                foreach (var student in students)
                {
                    if (student.FinalResultAverage < 5.0)
                    {
                        swFailed.WriteLine(student.ToString());
                        failed.Add(student);
                    }
                    else
                    {
                        swPassed.WriteLine(student.ToString());
                        passed.Add(student);
                    }
                }


                swFailed.Close();
                swPassed.Close();
                separate2Files.Stop();
                hash["sort2files"] = separate2Files.ElapsedMilliseconds;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("2 Separate files created");
            }
        }

        public void Sort2FilesLinkedList(LinkedList<Student> students, Dictionary<string, long> hash)
        {
            LinkedList<Student> failed = new LinkedList<Student>();
            LinkedList<Student> passed = new LinkedList<Student>();
            try
            {
                var fileCreation = System.Diagnostics.Stopwatch.StartNew();

                StreamWriter swPassed = new StreamWriter("Passed.txt");
                StreamWriter swFailed = new StreamWriter("Failed.txt");

                fileCreation.Stop();
                hash["2files_create"] = fileCreation.ElapsedMilliseconds;

                var separate2Files = System.Diagnostics.Stopwatch.StartNew();

                foreach (var student in students)
                {
                    if (student.FinalResultAverage < 5.0)
                    {
                        swFailed.WriteLine(student.ToString());
                        failed.AddLast(student);
                    }
                    else
                    {
                        swPassed.WriteLine(student.ToString());
                        passed.AddLast(student);
                    }
                }


                swFailed.Close();
                swPassed.Close();
                separate2Files.Stop();
                hash["sort2files"] = separate2Files.ElapsedMilliseconds;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("2 Separate files created");
            }
        }


        public void Sort2FilesQueue(Queue<Student> students, Dictionary<string, long> hash)
        {
            Queue<Student> failed = new Queue<Student>();
            Queue<Student> passed = new Queue<Student>();
            try
            {
                var fileCreation = System.Diagnostics.Stopwatch.StartNew();

                StreamWriter swPassed = new StreamWriter("Passed.txt");
                StreamWriter swFailed = new StreamWriter("Failed.txt");

                fileCreation.Stop();
                hash["2files_create"] = fileCreation.ElapsedMilliseconds;

                var separate2Files = System.Diagnostics.Stopwatch.StartNew();

                foreach (var student in students)
                {
                    if (student.FinalResultAverage < 5.0)
                    {
                        swFailed.WriteLine(student.ToString());
                        failed.Enqueue(student);
                    }
                    else
                    {
                        swPassed.WriteLine(student.ToString());
                        passed.Enqueue(student);
                    }
                }


                swFailed.Close();
                swPassed.Close();
                separate2Files.Stop();
                hash["sort2files"] = separate2Files.ElapsedMilliseconds;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("2 Separate files created");
            }
        }


        public void FivePerformanceTests(List<Student> students, Dictionary<string, long> hash, List<Student> passed,
            List<Student> failed)
        {
            try
            {
                Console.WriteLine("----Performance----");
                PrepareListForTest(students, "students.txt", hash);
                SpeedAnalysis(students, hash, passed, failed);

                PrepareListForTest(students, "10000Students.txt", hash);
                SpeedAnalysis(students, hash, passed, failed);

                PrepareListForTest(students, "100000Students.txt", hash);
                SpeedAnalysis(students, hash, passed, failed);

                PrepareListForTest(students, "1000000Students.txt", hash);
                SpeedAnalysis(students, hash, passed, failed);

                PrepareListForTest(students, "10000000Students.txt", hash);
                SpeedAnalysis(students, hash, passed, failed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception message:" + ex.Message);
                Console.WriteLine("If file doesn't exist, you can generate it (createHundreds option)");
            }
        }

        public void DisplayResult(Dictionary<string, long> hash, int count, string type)
        {
            Console.WriteLine("For {0} with {1} records, speed test:", type, count);

            Console.WriteLine("|{0,05}|{1,30}|  {2,15}|", "#", "Parameter", "Speed");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("|{0,05}|{1,30}|{2,15}ms|", "1", "Overall work", hash["overall_sort"]);
            Console.WriteLine("|{0,05}|{1,30}|{2,15}ms|", "1", "File read", hash["file_read"]);
            Console.WriteLine("|{0,05}|{1,30}|{2,15}ms|", "2", "2 files created in", hash["2files_create"]);
            Console.WriteLine("|{0,05}|{1,30}|{2,15}ms|", "3", "Data sorted in files", hash["sort2files"]);
            Console.WriteLine("------------------------------------------------------------- \n");

            hash.Clear();
        }

        public void SpeedAnalysis(List<Student> students, Dictionary<string, long> hash, List<Student> passed,
            List<Student> failed)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Sort2Files(students, hash, passed, failed);
                watch.Stop();
                hash["overall_sort"] = watch.ElapsedMilliseconds;

                DisplayResult(hash, students.Count, "list");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Performed.");
            }
        }

        private void PrepareListForTest(List<Student> students, string filePerform, Dictionary<string, long> hash)
        {
            students.Clear();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                StreamReader sr = new StreamReader(filePerform);

                filePerform = sr.ReadLine();

                while (filePerform != null)
                {
                    students.Add(prepareStudent(filePerform));
                    filePerform = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception happened: " + e.Message);
            }

            watch.Stop();
            Console.WriteLine("End of file.");
            hash["file_read"] = watch.ElapsedMilliseconds;
        }

        private void PrepareLinkedListForTest(LinkedList<Student> students, string filePerform,
            Dictionary<string, long> hash)
        {
            students.Clear();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                StreamReader sr = new StreamReader(filePerform);

                filePerform = sr.ReadLine();

                while (filePerform != null)
                {
                    students.AddLast(prepareStudent(filePerform));
                    filePerform = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception happened: " + e.Message);
            }

            watch.Stop();
            Console.WriteLine("End of file.");
            hash["file_read"] = watch.ElapsedMilliseconds;
        }

        private void PrepareQueueForTest(Queue<Student> students, string filePerform,
            Dictionary<string, long> hash)
        {
            students.Clear();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                StreamReader sr = new StreamReader(filePerform);

                filePerform = sr.ReadLine();

                while (filePerform != null)
                {
                    students.Enqueue(prepareStudent(filePerform));
                    filePerform = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception happened: " + e.Message);
            }

            watch.Stop();
            Console.WriteLine("End of file.");
            hash["file_read"] = watch.ElapsedMilliseconds;
        }


        public void ContainerTesting(string filePerform)
        {
            LinkedList<Student> linkedList = new LinkedList<Student>();
            List<Student> list = new List<Student>();
            Queue<Student> queue = new Queue<Student>();

            Dictionary<string, long> timerLinkedList = PrepareStats();
            Dictionary<string, long> timerList = PrepareStats();
            Dictionary<string, long> timerQueue = PrepareStats();


            //List
            var watchList = System.Diagnostics.Stopwatch.StartNew();
            PrepareListForTest(list, filePerform, timerList);
            Sort2Files(list, timerList, new List<Student>(), new List<Student>());

            watchList.Stop();
            timerList["overall_sort"] = watchList.ElapsedMilliseconds;


            //Linked List
            var watchLinkedList = System.Diagnostics.Stopwatch.StartNew();
            PrepareLinkedListForTest(linkedList, filePerform, timerLinkedList);
            Sort2FilesLinkedList(linkedList, timerList);
            
            watchLinkedList.Stop();
            timerLinkedList["overall_sort"] = watchLinkedList.ElapsedMilliseconds;

            //Queue
            var watchQueue = System.Diagnostics.Stopwatch.StartNew();
            PrepareQueueForTest(queue, filePerform, timerQueue);
            Sort2FilesQueue(queue, timerQueue);
            watchQueue.Stop();
            timerQueue["overall_sort"] = watchQueue.ElapsedMilliseconds;
            
            //count is the same for everyone
            int size = list.Count;
            
            DisplayResult(timerList, size, "list");
            DisplayResult(timerLinkedList, size, "linked list");
            DisplayResult(timerQueue, size, "queue");
        }

        private Dictionary<string, long> PrepareStats()
        {
            Dictionary<string, long> stats = new Dictionary<string, long>();
            stats.Add("overall_sort", 0);
            stats.Add("2files_create", 0);
            stats.Add("sort2files", 0);
            stats.Add("file_read", 0);
            return stats;
        }
    }
}