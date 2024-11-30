using KursaVasiljev.Interfaces;
using System.Text;
using KursaVasiljev.Extensions;
using KursaVasiljev.Common;
using MessagePack;

namespace KursaVasiljev.Models
{
    public class Group : ICountable, IReportable
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }

        public override string ToString()
        {
            return $"Группа: {Name}, Студентов: {Students.Length} {(Students.Length < 10 ? "(Внимание! Количество студентов в группе должно быть не менее 10)" : Students.Length > 30 ? "(Внимание! Количество студентов в группе должно быть не более 30)" : "")}";
        }

        public void SortStudentsByAverageMark(SortDirection sortDirection)
        {
            OwnMath.BinaryInsertionSort(Students, (student) => student.AverageMark, sortDirection);
        }

        public void SortStudentsBySurnameAndName(SortDirection sortDirection)
        {
            OwnMath.BinaryInsertionSort(Students, (student) => student.Surname + student.Name, sortDirection);
        }

        public int Count() => Students.Length;

        public int Count(double avgMark) => Count(avgMark, double.MaxValue);

        public int Count(double avgMarkMin, double avgMarkMax)
        {
            var count = 0;
            foreach (Student student in Students)
            {
                count += student.AverageMark >= avgMarkMin && student.AverageMark <= avgMarkMax ? 1 : 0;
            }
            return count;
        }

        public double GetAverageMark() => Students.OwnAvg((student) => student.AverageMark);

        public double GetAverageMisses() => Students.OwnAvg((student) => student.Misses);

        public int CountBadStudents() => Students.OwnSum((student) => student.BadStudent ? 1 : 0);

        public string GenerateReport()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Отчет по группе ({DateTime.Now.ToString()})");
            strBuilder.AppendLine($"Название: {Name}");
            strBuilder.AppendLine($"Численность: {Count()}");
            strBuilder.AppendLine($"Средний балл группы: {GetAverageMark()}");
            strBuilder.AppendLine($"Среднее количество пропусков на студента: {GetAverageMisses()}");
            var badStudents = CountBadStudents();
            strBuilder.AppendLine($"Количество неуспевающих студентов (% от общего числа): {badStudents} ({Convert.ToDouble(badStudents) / Count() * 100}%)");

            return strBuilder.ToString();
        }
    }
}
