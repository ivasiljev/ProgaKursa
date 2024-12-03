using KursaVasiljev.Interfaces;
using System.Text;

namespace KursaVasiljev
{
    public partial class Student : IReportable
    {
        public bool BadStudent { get; set; }

        private int _countOfBadMarksToBecomeBadStudent = 5;
        private bool _isBadMarkCondition(int mark) => mark <= 2;

        private bool _isBadStudent()
        {
            var badMarksCounter = 0;
            var failedSubject = false;
            for (int j = 0; j < _marks.GetLength(1); j++) // cols
            {
                var onlyBad = true;
                for (int i = 0; i < _marks.GetLength(0); i++) // rows
                {
                    if (_isBadMarkCondition(_marks[i, j]))
                    {
                        badMarksCounter++;
                    }
                    else
                    {
                        onlyBad = false;
                    }
                }
                failedSubject = failedSubject || onlyBad;
            }
            return failedSubject && badMarksCounter > _countOfBadMarksToBecomeBadStudent;
        }

        private partial void _onMarksPropertyChangedInternal()
        {
            BadStudent = _isBadStudent();
        }

        public string GenerateReport()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Отчет по студенту ({DateTime.Now.ToString()})");
            strBuilder.AppendLine($"ID: {Id}");
            strBuilder.AppendLine($"Имя: {Name}");
            strBuilder.AppendLine($"Фамилия: {Surname}");
            strBuilder.AppendLine($"Возраст: {Age}");
            strBuilder.AppendLine($"Средний балл: {AverageMark}");
            strBuilder.AppendLine($"Пропусков: {Misses}");
            strBuilder.AppendLine($"Является должником: {(BadStudent ? "да" : "нет")}");

            for (int i = 0; i < _marks.GetLength(0); i++) // rows
            {
                for (int j = 0; j < _marks.GetLength(1); j++) // cols
                {
                    strBuilder.Append(_marks[i, j]).Append(' ');
                }
                strBuilder.Append('\n');
            }
            return strBuilder.ToString();
        }
    }
}
