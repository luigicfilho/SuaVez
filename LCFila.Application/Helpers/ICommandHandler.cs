namespace LCFila.Application.Helpers;

internal interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}

internal interface ICommandHandler<TCommand, TResult> where TCommand : ICommand //where TResult : class
{
    Task<TResult> Handle(TCommand command);
}