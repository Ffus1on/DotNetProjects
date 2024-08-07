using Lab1_v2.University.Course;

namespace Lab1_v2.University.Group;

public class Group              
{
    public List<Student.Student?>? Students { get; set; }
    public CourseNumber CourseNumber { get; set; }
    public GroupName GroupName { get; set; }
    public int MaxSize { get; set; } = 20;

    public Group(GroupName groupName)
    {
        GroupName = groupName;
        Students = new List<Student.Student?>();
    }

    public Group(GroupName groupName, CourseNumber courseNumber)
    {
        GroupName = groupName;
        CourseNumber = courseNumber;
        Students = new List<Student.Student?>();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }   

        Group other = (Group)obj;
        return GroupName.Equals(other.GroupName); // Сравниваем по имени группы
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }
}