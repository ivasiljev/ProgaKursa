using KursaVasiljev.Common;
using KursaVasiljev.Models;

namespace KursaVasiljev.Managers
{
    internal class TeachersManager(TeachersManagerConfiguration configuration)
    {
        private readonly Random _rand = new();
        private readonly TeachersManagerConfiguration _configuration = configuration;

        public List<Teacher> Teachers { get; private set; } = [];

        public Teacher CreateTeacher(IEnumerable<Group> groups, string seniority, string subject)
        {
            var teacher = _addTeacher(
                age: _rand.Next(_configuration.MinAge, _configuration.MaxAge),
                name: Constants.Names[_rand.Next(Constants.Names.Length)],
                surname: Constants.Surnames[_rand.Next(Constants.Surnames.Length)],
                groups: groups,
                seniority: seniority,
                subject: subject
            );
            return teacher;
        }

        private Teacher _addTeacher(int age, string name, string surname, IEnumerable<Group> groups, string seniority, string subject)
        {
            var group = new Teacher
            {
                Age = age,
                Name = name,
                Surname = surname,
                Groups = groups.ToArray(),
                Seniority = seniority,
                Subject = subject
            };
            Teachers.Add(group);
            return group;
        }
    }

    internal class TeachersManagerConfiguration
    {
        // For random generation
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
