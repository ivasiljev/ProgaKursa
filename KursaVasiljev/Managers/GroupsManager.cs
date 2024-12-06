using KursaVasiljev.Models;

namespace KursaVasiljev.Managers
{
    internal class GroupsManager(StudentsManager studentsManager)
    {
        private readonly StudentsManager _studentsManager = studentsManager;

        public List<Group> Groups { get; private set; } = [];

        public Group CreateGroup(string groupName, IEnumerable<Student> students)
        {
            return _addGroup(groupName, students);
        }

        private Group _addGroup(string name, IEnumerable<Student> students)
        {
            var group = new Group
            {
                Name = name,
                Students = students.ToArray(),
            };
            Groups.Add(group);
            return group;
        }

        public Group CreateGroupWithStudents(string groupName, int studentsCount)
        {
            return _addGroup(groupName, _studentsManager.CreateRandomStudents(studentsCount));
        }
    }
}
