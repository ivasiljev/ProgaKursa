using KursaVasiljev.Models;
using MessagePack;
using System.Xml.Serialization;

namespace KursaVasiljev
{
    public partial class Student : Person
    {
        public int Id { get; set; }
        [XmlIgnore]
        public int[,] Marks { get => _marks; set { _marks = value; OnMarksPropertyChanged(); } }
        public double AverageMark { get; set; }
        public int Misses { get; set; }


        [XmlArray("Marks")]
        public int[] MarksDto
        {
            get { return Flatten(Marks); }
            set { Marks = Expand(value, 2); }
        }

        public static T[] Flatten<T>(T[,] arr)
        {
            int rows0 = arr.GetLength(0);
            int rows1 = arr.GetLength(1);
            T[] arrFlattened = new T[rows0 * rows1];
            for (int j = 0; j < rows1; j++)
            {
                for (int i = 0; i < rows0; i++)
                {
                    var test = arr[i, j];
                    arrFlattened[i + j * rows0] = arr[i, j];
                }
            }
            return arrFlattened;
        }
        public static T[,] Expand<T>(T[] arr, int rows0)
        {
            int length = arr.GetLength(0);
            int rows1 = length / rows0;
            T[,] arrExpanded = new T[rows0, rows1];
            for (int j = 0; j < rows1; j++)
            {
                for (int i = 0; i < rows0; i++)
                {
                    arrExpanded[i, j] = arr[i + j * rows0];
                }
            }
            return arrExpanded;
        }

        private int[,] _marks;

        public void OnMarksPropertyChanged()
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
