using KursaVasiljev.Common;
using KursaVasiljev.Models;
using System.Xml.Serialization;

namespace KursaVasiljev
{
    public partial class Student : Person
    {
        public int Id { get; set; }
        [XmlIgnore]
        public int[,] Marks { get => _marks; set { _marks = value; _onMarksPropertyChanged(); } }
        public double AverageMark { get; set; }
        public int Misses { get; set; }


        [XmlArray("Marks")]
        public int[] MarksDto
        {
            get { return MultidimensionalArraysUtils.Flatten(Marks); }
            set { Marks = MultidimensionalArraysUtils.Expand(value, 2); }
        }

        private int[,] _marks;

        private void _onMarksPropertyChanged()
        {
            var sum = 0d;
            for (int i = 0; i < _marks.GetLength(0); i++) // rows
            {
                for (int j = 0; j < _marks.GetLength(1); j++) // cols
                {
                    sum += _marks[i, j];
                }
            }
            AverageMark = sum / _marks.Length;

            _onMarksPropertyChangedInternal();
        }

        private partial void _onMarksPropertyChangedInternal();

        public override string ToString()
        {
            return $"Id: {Id} | {Surname} {Name} ({Age} лет) Средний бал: {AverageMark}, Пропуски: {Misses}";
        }
    }
}
