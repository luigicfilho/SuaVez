namespace LCFilaApplication.Helpers.testes;

public interface IResultFactory<T>
{
    IResult<T> Success(T value);
    IResult<Error> Failure(Error error);
}
