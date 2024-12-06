using KursaVasiljev.Common;

namespace KursaVasiljev.Managers
{
    internal class StudentsManager(StundentsManagerConfiguration configuration)
    {
        private static int _idCounter = 0;
        private readonly Random _rand = new();
        private readonly CustomRandom _customRandom = new();
        private readonly StundentsManagerConfiguration _configuration = configuration;

        public List<Student> Students { get; private set; } = [];

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
            var isLikelyBadStudent = _rand.Next(0, 7) == 0; // 12.5% that this student has a good chance to be a bad student
            for (int i = 0; i < _configuration.MarksRowsCount; i++)
            {
                for (int j = 0; j < _configuration.MarksColumnsCount; j++)
                {
                    marks[i, j] = _customRandom.Next(_configuration.MinMark, _configuration.MaxMark);
                    if (i > 0 && marks[i, j] == 2)
                    {
                        marks[i - 1, j] = 2;
                    }
                }
            }

            return _addStudent(
                age: _customRandom.Next(_configuration.MinAge, _configuration.MaxAge, true),
                misses: _customRandom.Next(_configuration.MinMisses, _configuration.MaxMisses, true),
                name: Constants.Names[_rand.Next(Constants.Names.Length)],
                surname: Constants.Surnames[_rand.Next(Constants.Surnames.Length)],
                marks: marks
            );
        }

        private Student _addStudent(int age, int misses, string name, string surname, int[,] marks)
        {
            var student = new Student
            {
                Id = _idCounter++,
                Age = age,
                Misses = misses,
                Name = name,
                Surname = surname,
                Marks = marks
            };
            Students.Add(student);
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
