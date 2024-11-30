using KursaVasiljev.Common;
using KursaVasiljev.Interfaces;
using MessagePack;
using System.Text;

namespace KursaVasiljev.Models
{
    public class Teacher : Person, IReportable
    {
        public string Seniority { get; set; }
        public string Subject { get; set; }
        public Group[] Groups { get; set; }

        public string GenerateReport()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Отчет по преподавателю ({DateTime.Now.ToString()})");
            strBuilder.AppendLine($"Имя: {Name}");
            strBuilder.AppendLine($"Фамилия: {Surname}");
            strBuilder.AppendLine($"Предмет: {Subject}");
            strBuilder.AppendLine($"Список студентов:");
            foreach (var student in GetStudents(sorted: true))
            {
                strBuilder.AppendLine(student.ToString());
            }
            return strBuilder.ToString();
        }

        public Student[] GetStudents(bool sorted, SortDirection sortDirection = SortDirection.Ascending)
        {
            HashSet<Student> students = [];
            foreach (var group in Groups)
            {
                foreach (var student in group.Students)
                {
                    students.Add(student);
                }
            }
            var studentsArray = students.ToArray();
            if (sorted)
            {
                OwnMath.BinaryInsertionSort(studentsArray, (student) => student.Surname + student.Name, sortDirection);
            }
            return studentsArray;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append($"{Name} ");
            strBuilder.Append($"{Surname} ");
            strBuilder.Append($"Предмет: {Subject} ");
            strBuilder.Append($"Группы: ");
            var lastGroup = Groups.Last();
            foreach (var group in Groups)
            {
                strBuilder.Append(group.Name);
                if (group != lastGroup)
                {
                    strBuilder.Append(", ");
                }
            }
            return strBuilder.ToString();
        }
    }
}
