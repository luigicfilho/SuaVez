namespace LCFilaApplication.Helpers.testes;

public class ResultFactory<T> : IResultFactory<T>
{
    public IResult<T> Success(T value)
    {
        return (IResult<T>)StrictResult<T>.Success(value);
    }

    public IResult<Error> Failure(Error error)
    {
        return (IResult<Error>)StrictResult<Error>.Failure(error);
    }
}