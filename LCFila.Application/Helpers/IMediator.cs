namespace LCFila.Application.Helpers;

internal interface IMediator
{
    Task<TResult> Send<TResult>(IQuery<TResult> query);
    Task Send(ICommand command);
}

//internal interface IMediator2
//{
//    Task<Results<TResult>> HandleQueryAsync<TQuery, TResult>(TQuery query, CancellationToken token)
//        where TQuery : IQuery<TResult>
//        where TResult : class?;

//    Task<Results<TResult>> HandleCommandAsync<TCommand, TResult>(TCommand command, CancellationToken token)
//        //where TCommand : ICommand<TResult>
//        where TResult : class?;

//    //Task<Results> HandleCommandAsync<TCommand>(TCommand command, CancellationToken token)
//    //    where TCommand : ICommand;
//}