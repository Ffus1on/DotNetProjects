using Lab1_v2.ResultTypes;
using Lab1_v2.University.Course;
using Lab1_v2.University.Group;
using Lab1_v2.University.Student;

namespace Lab1_v2.Service;

public interface IIsuService
{
    AddGroupResult AddGroup(GroupName name, CourseNumber courseNumber); 
    AddStudentResult AddStudent(Group group, string name);

    GetStudentResult GetStudent(int id);
    Student? FindStudent(int id);
    List<Student?>? FindStudents(GroupName groupName);
    List<Student>? FindStudents(CourseNumber courseNumber);
    
    Group? FindGroup(GroupName groupName);
    List<Group>? FindGroups(CourseNumber courseNumber);

    ChangeStudentGroupResult ChangeStudentGroup(Student student, Group? newGroup);
}