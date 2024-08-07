using Lab1_v2.ResultTypes;
using Lab1_v2.Service;
using Lab1_v2.University.Course;
using Lab1_v2.University.Group;
using Lab1_v2.University.Student;
using Xunit.Abstractions;

namespace Lab1_v2Tests;

public class IsuTests
{
    
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        
        // Arrange
        var isuService = new IsuService();
        var groupName = new GroupName("M3111");
        var courseNumber = new CourseNumber(1);
        
        // Act
        isuService.AddGroup(groupName, courseNumber);
        var group = isuService.FindGroup(groupName);
        isuService.AddStudent(group, "Artem");
        var student = isuService.FindStudent(0);
        
        //Assert
        Assert.Contains(student, group.Students);
        Assert.Equal(student.Group, group);

    }
    
    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        
        // Arrange
        var isuService = new IsuService();
        var groupName = new GroupName("M3111");
        var courseNumber = new CourseNumber(1);
        isuService.AddGroup(groupName, courseNumber);
        var group = isuService.FindGroup(groupName);
        
        // Act
        for (int i = 0; i < 20; i++)
        {
            var studentName = $"Student{i + 1}";
            isuService.AddStudent(group, studentName);
        }

        //Assert
        var finalResult = isuService.AddStudent(group, "Student21");
        Assert.IsType<AddStudentResult.StudentLimitReached>(finalResult);
        
    }
    
    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        
        // Arrange
        var isuService = new IsuService();
        var invalidGroupName1 = new GroupName(""); // Пустое имя
        var invalidGroupName2 = new GroupName("Invalid@Name"); // Имя с недопустимыми символами
        var validCourseNumber = new CourseNumber(1);

        // Act
        var result1 = isuService.AddGroup(invalidGroupName1, validCourseNumber);
        var result2 = isuService.AddGroup(invalidGroupName2, validCourseNumber);

        // Assert
        Assert.IsType<AddGroupResult.InvalidName>(result1);
        Assert.IsType<AddGroupResult.InvalidName>(result2);
        
    }
    
    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        
        // Arrange
        var isuService = new IsuService();
        
        var firstGroupName = new GroupName("M3111");
        var secondGroupName = new GroupName("M3112");
        
        var courseNumber = new CourseNumber(1);
        
        
        isuService.AddGroup(firstGroupName, courseNumber);
        var firstGroup = isuService.FindGroup(firstGroupName);
        isuService.AddGroup(secondGroupName, courseNumber);
        var secondGroup = isuService.FindGroup(secondGroupName);
        
        isuService.AddStudent(firstGroup, "Artem");
        Student student = isuService.FindStudent(0);

        // Act
        isuService.ChangeStudentGroup(student, secondGroup);

        // Assert
        Assert.Contains(student, secondGroup.Students);
        Assert.DoesNotContain(student, firstGroup.Students);
    }
    
}