namespace Lab1_v2.University.Group;

public class GroupName
{
    public string? Name { get; set; }

    public GroupName(string? name)
    {
        Name = name;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        GroupName other = (GroupName)obj;
        return Name == other.Name; 
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}