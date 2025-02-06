namespace LCFilaApplication.Helpers;

public sealed record Error(string Code, string Message, ErrorType ErrorType)
{
    public static readonly Error EmptyFailure = new(string.Empty, string.Empty, ErrorType.GenericFailure);

    public static Error GenericFailure(string code, string message) 
        => new(code, message, ErrorType.GenericFailure);

    public static Error ValidationFailure(string code, string message)
        => new(code, message, ErrorType.Validation);

    public static Error NotFoundFailure(string code, string message)
        => new(code, message, ErrorType.NotFound);

    public static Error ConflitFailure(string code, string message)
        => new(code, message, ErrorType.Conflit);

    public static Error ServerErrorFailure(string code, string message)
        => new(code, message, ErrorType.ServerError);
};
