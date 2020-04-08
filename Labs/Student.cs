using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Labs
{
    public class Student
    {
        private string Name;
        private string Surname;

        private List<double> grades;
        private double exam;
        private double[] gradeArray;
        private double finalResultMedian;
        private double finalResultAverage;

        public Student(string name, string surname, List<double> grades, double exam, double[] gradeArray, double finalResultMedian, double finalResultAverage)
        {
            this.Name = name;
            this.Surname = surname;
            this.grades = grades;
            this.exam = exam;
            this.gradeArray = gradeArray;
            this.finalResultAverage = finalResultAverage;
            this.finalResultMedian = finalResultMedian;
        }


        public string Name1
        {
            get => Name;
            set => Name = value;
        }

        public string Surname1
        {
            get => Surname;
            set => Surname = value;
        }

        public List<double> Grades
        {
            get => grades;
            set => grades = value;
        }

        public double Exam
        {
            get => exam;
            set => exam = value;
        }

        public double[] GradeArray
        {
            get => gradeArray;
            set => gradeArray = value;
        }

        public double FinalResultMedian
        {
            get => finalResultMedian;
            set => finalResultMedian = value;
        }

        public double FinalResultAverage
        {
            get => finalResultAverage;
            set => finalResultAverage = value;
        }

        public override string ToString()
        {
            
            List <string> stringGrades = new List<string>();
            foreach (var grade in grades)
            {
                stringGrades.Add(grade.ToString());
            }

            return this.Name + " " + this.Surname + " " + string.Join(" ", stringGrades.ToArray()) + " " + this.exam;
        }
    }
    
    
}