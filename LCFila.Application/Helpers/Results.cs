namespace LCFila.Application.Helpers;

public sealed record Results<TValue> //: IResults //Not implemented as interface, will be next
{
    // If you want theses objects be accessible only on math()
    // make them private otherwise let them public
    // but will make things more complex, and work with delegates 
    // maybe you want these values accessible or create method for that
    public readonly TValue? Value;
    public readonly Error? Error;

    public bool IsSuccess { get; } = false;
    public bool IsError => !IsSuccess;

    private Results() { }

    private Results(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }

    private Results(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator Results<TValue>(TValue value) => new(value);

    public static implicit operator Results<TValue>(Error error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onFailure)
                => IsSuccess ? onSuccess(Value!) : onFailure(Error!);

    public void Match(Action<TValue>? success = null, Action<Error>? failure = null)
    {
        if (this.IsSuccess)
        {
            success?.Invoke(Value!);
        }
        else
        {
            failure?.Invoke(Error!);
        }
    }

    public static Results<TValue> Failure(Error error) => new(error);
    public static Results<TValue> Success(TValue value) => new(value);
}
