namespace Lab1_v2.ResultTypes;

public abstract record AddGroupResult
{
    private AddGroupResult() { }

    public sealed record Success : AddGroupResult;

    public sealed record GroupLimitReached(int Limit) : AddGroupResult;

    public sealed record AlreadyExist : AddGroupResult;
    
    public sealed record BadCourseNumber : AddGroupResult;

    public sealed record InvalidName : AddGroupResult;
}