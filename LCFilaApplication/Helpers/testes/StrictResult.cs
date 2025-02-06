//using LCFilaApplication.Helpers.testes;

namespace LCFilaApplication.Helpers.testes;
public class Result<T, TError>
{
    private readonly T? _value;
    private readonly TError? _error;
}

public class StrictResult<TValue> //: IResult<TValue>
{
    private readonly TValue? _value;
    private readonly Error? _error;

    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;


    private StrictResult(TValue value)
    {
        IsSuccess = true;
        _value = value;
        _error = default;
    }

    private StrictResult(Error error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
    }

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(_value!) : onFailure(_error!);
    }

    public static StrictResult<TValue> Success(TValue value) => new StrictResult<TValue>(value);

    public static StrictResult<TValue> Failure(Error error) => new StrictResult<TValue>(error);
}