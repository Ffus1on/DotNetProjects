using Lab1_v2.ResultTypes;
using Lab1_v2.University.Course;
using Lab1_v2.University.Group;
using Lab1_v2.University.Student;

namespace Lab1_v2.Service;

public class IsuService : IIsuService
{
    private Dictionary<CourseNumber, List<Group?>> Courses { get; set; }
    private int GroupLimit { get; } = 20;
    private int Counter { get; set; } = 0;
    
    public IsuService()
    {
        Courses = new Dictionary<CourseNumber, List<Group?>>();
        
        for (int i = 1; i <= 4; i++)
        {
            Courses.Add(new CourseNumber(i), new List<Group?>());
        }
    }

    public AddGroupResult AddGroup(GroupName name, CourseNumber courseNumber)
    {
        
        if (string.IsNullOrWhiteSpace(name.Name) || name.Name.Any(c => !char.IsLetterOrDigit(c)))
        {
            return new AddGroupResult.InvalidName();
        }
        
        var groups = Courses[courseNumber];
        var group = new Group(name, courseNumber);
        
        if (Courses.ContainsKey(courseNumber))
        {
            if (groups.Contains(group))
            {
                return new AddGroupResult.AlreadyExist();
            }

            if (groups.Count == GroupLimit)
            {
                return new AddGroupResult.GroupLimitReached(GroupLimit);
            }

            Courses[courseNumber].Add(group);
            return new AddGroupResult.Success();
        }

        return new AddGroupResult.BadCourseNumber();
    }

    public AddStudentResult AddStudent(Group group, string name)
    {
        foreach (var course in Courses)
        {
            foreach (var groupIterate in course.Value)
            {
                if (Equals(groupIterate, group))
                {
                    var student = new Student(groupIterate, Counter, name);

                    if (groupIterate.Students != null && groupIterate.Students.Contains(student))
                    {
                        return new AddStudentResult.AlreadyMember();
                    }

                    if (groupIterate.Students.Count == group.MaxSize)
                    {
                        return new AddStudentResult.StudentLimitReached(group.MaxSize);
                    }
        
                    groupIterate.Students.Add(student);
                    Counter++;
                    return new AddStudentResult.Success();
                }
            }
        }

        return new AddStudentResult.NotExist();
    }

    public GetStudentResult GetStudent(int id)
    {
        foreach (var course in Courses)
        {
            foreach (var group in course.Value)
            {
                foreach (var student in group.Students)     
                {
                    if (student != null && id == student.Id)
                    {
                        return new GetStudentResult.Success();
                    }
                }  
            }
        }

        return new GetStudentResult.NotFound();
    }

    public Student? FindStudent(int id)
    {
        foreach (var course in Courses)
        {
            foreach (var group in course.Value)
            { 
                foreach (var student in group.Students)
                {
                    if (student != null && id == student.Id)
                    {
                        return student;
                    }
                }
            }
        }

        return null;
    }

    public List<Student?>? FindStudents(GroupName groupName)
    {
        foreach (var course in Courses)
        {
            foreach (var group in course.Value)
            {
                if (Equals(group.GroupName, groupName))
                {
                    return group.Students;
                }
            }
        }

        return null;
    }

    public List<Student>? FindStudents(CourseNumber courseNumber)
    {
        if (courseNumber.Number > 0 && courseNumber.Number < 5)
        {
            var groups = Courses[courseNumber];
            
            List<Student> studentsInCourse = new List<Student>();

            foreach (var group in groups)
            {
                foreach (var student in group.Students)        
                {
                    studentsInCourse.Add(student);
                }
            }

            return studentsInCourse;
        }

        return null;
    }

    public Group? FindGroup(GroupName groupName)
    {
        foreach (var course in Courses)
        {
            foreach (var group in course.Value)
            {
                if (Equals(group.GroupName.Name, groupName.Name))
                {
                    return group;
                }
            }
        }

        return null;
    }   

    public List<Group>? FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber.Number > 0 && courseNumber.Number < 5)
        {
            List<Group?> groupsInCourse = new List<Group?>();

            foreach (var group in Courses[courseNumber])
            {
                groupsInCourse.Add(group);
            }
            
            return groupsInCourse;
        }

        return null;
    }

    public ChangeStudentGroupResult ChangeStudentGroup(Student student, Group? newGroup)
    {
        if (Equals(student.Group, newGroup))
        {
            return new ChangeStudentGroupResult.AlreadyMember();
        }

        if (newGroup.Students.Count == GroupLimit)
        {
            return new ChangeStudentGroupResult.StudentLimitReached(newGroup.Students.Count);
        }

        foreach (var course in Courses)
        {
            foreach (var group in course.Value) 
            {
                foreach (var studentIterate in group?.Students)
                {
                    if (Equals(studentIterate, student))
                    {
                        studentIterate.Group = newGroup;
                        newGroup.Students.Add(studentIterate);
                        group.Students.Remove(studentIterate);

                        return new ChangeStudentGroupResult.Success();
                    }
                }
            }
        }

        return new ChangeStudentGroupResult.Failed();
    }
}