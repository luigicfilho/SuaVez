namespace LCFila.Application.Helpers.testes;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsError { get; }

    TResult Match<TResult>(Func<TResult> onSuccess, Func<Error> onFailure);

}

//public interface IResult<out T> : IResult
//{
//    T? Value { get; }
//    TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure);

//    static abstract IResult<T> Success(T value);
//    static abstract IResult<Error> Failure(Error error);
//}
public interface IResult<T> : IResult
{
    TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure);

    static abstract IResult<T> Success(T value);
    static abstract IResult<Error> Failure(Error error);

}