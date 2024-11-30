using KursaVasiljev.Models;

namespace KursaVasiljev.Managers
{
    internal class GroupsManager(StudentsManager studentsManager)
    {
        private readonly StudentsManager _studentsManager = studentsManager;

        public Group CreateGroup(string groupName, Student[] students)
        {
            return new Group
            {
                Name = groupName,
                Students = students,
            };
        }

        public Group CreateGroupWithStudents(string groupName, int studentsCount)
        {
            return CreateGroup(groupName, _studentsManager.CreateRandomStudents(studentsCount));
        }
    }
}
