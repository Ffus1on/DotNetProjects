namespace Lab1_v2.ResultTypes;

public abstract record GetStudentResult
{
    private GetStudentResult() { }

    public sealed record Success : GetStudentResult;

    public sealed record NotFound : GetStudentResult;
}