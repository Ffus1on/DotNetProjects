using Lab1_v2.University.Course;

namespace Lab1_v2.University.Student;

public class Student
{
    public CourseNumber CourseNumber { get; set; }
    public Group.Group? Group { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Student(Group.Group? group, int id, string name)
    {
        Group = group;
        Id = id;
        Name = name;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Student other = (Student)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}