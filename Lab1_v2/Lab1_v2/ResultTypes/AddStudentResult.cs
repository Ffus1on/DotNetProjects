namespace Lab1_v2.ResultTypes;

public abstract record AddStudentResult
{
    private AddStudentResult() { }

    public sealed record Success : AddStudentResult;

    public sealed record AlreadyMember : AddStudentResult;

    public sealed record StudentLimitReached(int Limit) : AddStudentResult;

    public sealed record NotExist : AddStudentResult;
}