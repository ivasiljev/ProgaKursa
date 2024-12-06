using KursaVasiljev.Common;
using KursaVasiljev.Managers;
using KursaVasiljev.Models;
using KursaVasiljev.Serialization;
using KursaVasiljev.Extensions;

#region Initialization
var studentsManager = new StudentsManager(new StundentsManagerConfiguration
{
    MarksRowsCount = 2,
    MarksColumnsCount = 10,

    MinAge = 18,
    MaxAge = 25,
    MinMisses = 0,
    MaxMisses = 20,
    MinMark = 2,
    MaxMark = 5
});
var groupsManager = new GroupsManager(studentsManager);
var teachersManager = new TeachersManager(new TeachersManagerConfiguration
{
    MinAge = 25,
    MaxAge = 60
});

var jsonSerializer = new MyJsonSerializer();
var xmlSerializer = new MyXmlSerializer();
var binSerializer = new MyBinarySerializer();

var rand = new Random();
#endregion

#region Fill data
const int InitStudentsCount = 100;
const int GroupsCount = 5;
const int StudentsPerGroup = InitStudentsCount / GroupsCount;
const int ExtraStudentsCount = 50;

var initStudents = studentsManager.CreateRandomStudents(InitStudentsCount);
var groups = new Group[GroupsCount];

initStudents.Shuffle();

for (int i = 0; i < GroupsCount; i++)
{
    var studentsToAddInGroup = initStudents.Skip(i * StudentsPerGroup).Take(StudentsPerGroup);
    groups[i] = groupsManager.CreateGroup($"Group {i + 1}", studentsToAddInGroup);
}

var extraStudents = studentsManager.CreateRandomStudents(ExtraStudentsCount);

foreach (var student in extraStudents)
{
    groups[rand.Next(0, GroupsCount - 1)].AddStudent(student);
}
#endregion

await jsonSerializer.Write(groups, "raw_data.json");
await xmlSerializer.Write(groups, "raw_data.xml");
await binSerializer.Write(groups, "raw_data.bin");

foreach (var group in groups)
{
    group.SortStudentsByAverageMark(SortDirection.Descending);
}
await jsonSerializer.Write(groups, "data.json");

foreach (var group in groups)
{
    group.SortStudentsByAverageMark(SortDirection.Ascending);
}
await xmlSerializer.Write(groups, "data.xml");

foreach (var group in groups)
{
    group.SortStudentsBySurnameAndName(SortDirection.Descending);
}
await binSerializer.Write(groups, "data.bin");

var rawDataXml = await xmlSerializer.Read<Group[]>("raw_data.xml");
var rawDataJson = await jsonSerializer.Read<Group[]>("raw_data.json");
var rawDataBin = await binSerializer.Read<Group[]>("raw_data.bin");

var dataXml = await xmlSerializer.Read<Group[]>("data.xml");
var dataJson = await jsonSerializer.Read<Group[]>("data.json");
var dataBin = await binSerializer.Read<Group[]>("data.bin");

Console.WriteLine(Constants.ConsoleSeparator);
Console.WriteLine("Данные из raw_data.xml");
Console.WriteLine(Constants.ConsoleSeparator);
foreach(var group in rawDataXml)
{
    Console.WriteLine(group.ToString());
    foreach (var student in group.Students)
    {
        Console.WriteLine(student.ToString());
    }
}

Console.WriteLine(Constants.ConsoleSeparator);
Console.WriteLine("Данные из data.xml");
Console.WriteLine(Constants.ConsoleSeparator);
foreach (var group in dataXml)
{
    Console.WriteLine(group.ToString());
    foreach (var student in group.Students)
    {
        Console.WriteLine(student.ToString());
    }
}

Console.WriteLine(Constants.ConsoleSeparator);
Console.WriteLine("Данные из data.json");
Console.WriteLine(Constants.ConsoleSeparator);
foreach (var group in dataJson)
{
    Console.WriteLine(group.ToString());
    foreach (var student in group.Students)
    {
        Console.WriteLine(student.ToString());
    }
}

Console.WriteLine(Constants.ConsoleSeparator);
Console.WriteLine("Данные из data.bin");
Console.WriteLine(Constants.ConsoleSeparator);
foreach (var group in dataBin)
{
    Console.WriteLine(group.ToString());
    foreach (var student in group.Students)
    {
        Console.WriteLine(student.ToString());
    }
}

var worstGroupResult = double.MaxValue;
Group worstGroup = null;
foreach (var group in groups)
{
    var curGroupResult = (double)group.CountBadStudents() / group.Count();
    if (curGroupResult < worstGroupResult)
    {
        worstGroup = group;
        worstGroupResult = curGroupResult;
    }
}

Console.WriteLine(Constants.ConsoleSeparator);
Console.WriteLine("Отчет по группе с наихужшей успеваемостью");
Console.WriteLine(Constants.ConsoleSeparator);

Console.WriteLine(worstGroup?.GenerateReport());

Console.WriteLine(Constants.ConsoleSeparator);
Console.WriteLine("Отчет по студентам группы с наихужшей успеваемостью");
Console.WriteLine(Constants.ConsoleSeparator);

foreach (var student in worstGroup.Students)
{
    Console.WriteLine(student?.GenerateReport());
}

Console.WriteLine(Constants.ConsoleSeparator);
Console.WriteLine("Отчет по преподавателю");
Console.WriteLine(Constants.ConsoleSeparator);

var teacher = teachersManager.CreateTeacher(rawDataJson, "Профессор", "Математический анализ");

Console.WriteLine(teacher.GenerateReport());