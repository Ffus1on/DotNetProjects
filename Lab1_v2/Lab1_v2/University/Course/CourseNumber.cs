namespace Lab1_v2.University.Course;

public class CourseNumber
{
    public int Number { get; set; }

    public CourseNumber(int number)
    {
        Number = number;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        CourseNumber other = (CourseNumber)obj;
        return Number == other.Number;
    }

    public override int GetHashCode()
    {
        return Number.GetHashCode();
    }
}