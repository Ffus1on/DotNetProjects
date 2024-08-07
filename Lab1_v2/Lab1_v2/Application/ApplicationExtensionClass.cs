using Lab1_v2.Service;
using Lab1_v2.University.Course;
using Lab1_v2.University.Group;
using Lab1_v2.University.Student;

namespace Lab1_v2.Application;

public static class ApplicationExtensionClass
{
    public static GroupName CreateGroupNameInputMode()
    {
        string? input = Console.ReadLine();
        GroupName groupName = new GroupName(input);
        return groupName;
    }

    public static GroupName CreateGroupNameNoInputMode(string? input)
    {
        GroupName groupName = new GroupName(input);
        return groupName; 
    }

    public static CourseNumber CreateCourseNumberInputMode()
    {
        int input = int.Parse(Console.ReadLine());
        CourseNumber courseNumber = new CourseNumber(input);
        return courseNumber;
    }

    public static Student GetStudentInputMode(IsuService isuService)
    {
        int studentId = int.Parse(Console.ReadLine());
        Student? student = isuService.FindStudent(studentId);

        if (student != null)
        {
            return student;
        }

        return null;
    }
}