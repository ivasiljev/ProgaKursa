using KursaVasiljev.Common;
using KursaVasiljev.Models;

namespace KursaVasiljev.Managers
{
    internal class TeachersManager(TeachersManagerConfiguration configuration)
    {
        private readonly Random _rand = new();
        private readonly TeachersManagerConfiguration _configuration = configuration;

        public Teacher CreateTeacher(Group[] groups, string seniority, string subject)
        {
            var teacher = new Teacher()
            {
                Age = _rand.Next(_configuration.MinAge, _configuration.MaxAge),
                Name = Constants.Names[_rand.Next(Constants.Names.Length)],
                Surname = Constants.Surnames[_rand.Next(Constants.Surnames.Length)],
                Groups = groups,
                Seniority = seniority,
                Subject = subject,
            };
            return teacher;
        }
    }

    internal class TeachersManagerConfiguration
    {
        // For random generation
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
