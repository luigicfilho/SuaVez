namespace LCFila.Application.Helpers;

internal interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> //where TResult : class
{
    Task<TResult> Handle(TQuery query);
}