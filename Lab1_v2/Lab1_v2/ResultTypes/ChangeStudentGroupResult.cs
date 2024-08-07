namespace Lab1_v2.ResultTypes;

public abstract record ChangeStudentGroupResult
{
    private ChangeStudentGroupResult() { }

    public sealed record Success : ChangeStudentGroupResult;

    public sealed record AlreadyMember : ChangeStudentGroupResult;

    public sealed record StudentLimitReached(int Limit) : ChangeStudentGroupResult;

    public sealed record Failed : ChangeStudentGroupResult;
}