using KursaVasiljev.Common;

namespace KursaVasiljev.Managers
{
    internal class StudentsManager(StundentsManagerConfiguration configuration)
    {
        private static int _idCounter = 0;
        private readonly Random _rand = new();
        private readonly StundentsManagerConfiguration _configuration = configuration;

        public Student[] CreateRandomStudents(int count)
        {
            var result = new Student[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = _createRandomStudent();
            }
            return result;
        }

        public Student CreateRandomStudent() => _createRandomStudent();

        private Student _createRandomStudent()
        {
            int[,] marks = new int[_configuration.MarksRowsCount, _configuration.MarksColumnsCount];
            for (int i = 0; i < _configuration.MarksRowsCount; i++)
            {
                for (int j = 0; j < _configuration.MarksColumnsCount; j++)
                {
                    marks[i, j] = (int)_rand.Next(_configuration.MinMark, _configuration.MaxMark);
                }
            }

            var student = new Student
            {
                Id = _idCounter++,
                Age = _rand.Next(_configuration.MinAge, _configuration.MaxAge),
                Misses = _rand.Next(_configuration.MinMisses, _configuration.MaxMisses),
                Name = Constants.Names[_rand.Next(Constants.Names.Length)],
                Surname = Constants.Surnames[_rand.Next(Constants.Surnames.Length)],
                Marks = marks
            };
            return student;
        }
    }

    internal class StundentsManagerConfiguration
    {
        public int MarksRowsCount { get; set; }
        public int MarksColumnsCount { get; set; }

        // For random generation
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MinMisses { get; set; }
        public int MaxMisses { get; set; }
        public int MinMark { get; set; }
        public int MaxMark { get; set; }
    }
}
