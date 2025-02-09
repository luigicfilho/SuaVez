namespace LCFila.Application.Helpers;

internal class Mediator : IMediator
{
    private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();

    public void RegisterHandler<TRequest, TResponse>(object handler)
    {
        _handlers.Add(typeof(TRequest), handler);
    }

    public async Task<TResult> Send<TResult>(IQuery<TResult> query)
    {
        if (_handlers.TryGetValue(query.GetType(), out var handler))
        {
            // Assuming handler implements IQueryHandler<TQuery, TResult>
            var queryHandler = (IQueryHandler<IQuery<TResult>, TResult>)handler;
            return await queryHandler.Handle(query);
        }

        //var handler = (RequestHandlerWrapper<TResponse>)_handlers.Add(query.GetType(), static requestType =>
        //{
        //    var wrapperType = typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResult));
        //    var wrapper = Activator.CreateInstance(wrapperType) ?? throw new InvalidOperationException($"Could not create wrapper type for {requestType}");
        //    return (RequestHandlerBase)wrapper;
        //});
        //return handler.Handle(request, _serviceProvider, cancellationToken);

        throw new InvalidOperationException($"No handler registered for {query.GetType()}");
    }

    public async Task Send(ICommand command)
    {
        if (_handlers.TryGetValue(command.GetType(), out var handler))
        {
            // Assuming handler implements ICommandHandler<TCommand>
            var commandHandler = (ICommandHandler<ICommand>)handler;
            await commandHandler.Handle(command);
        }

        throw new InvalidOperationException($"No handler registered for {command.GetType()}");
    }
}

/*
 private readonly Dictionary<string, Participant> participants = [];
    public void Register(Participant participant)
    {
        participants.TryAdd(participant.Name, participant);
        participant.Chatroom = this;
    }
    public void Send(string from, string to, string message)
    {
        var participant = participants[to];
        if (participant != null)
        {
            participant.Receive(from, message);
        }
    }
 */

/*
internal class Mediator2 : IMediator2
{
    private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();

    public async Task<Results> HandleCommandAsync<TCommand>(TCommand command, CancellationToken token)
        where TCommand : ICommand
    {
        var context = new Mediator();
        context.RegisterHandler<TCommand, bool>(command);

        if (_handlers.TryGetValue(command.GetType(), out var handler))
        {
            // Assuming handler implements ICommandHandler<TCommand>
            var commandHandler = (ICommandHandler<ICommand>)handler;
            await commandHandler.Handle(command);
        }

        throw new InvalidOperationException($"No handler registered for {command.GetType()}");
        
    }

    public async Task<Results<TResult>?> HandleCommandAsync<TCommand, TResult>(TCommand command, CancellationToken token)
        where TCommand : ICommand
        where TResult : class?
    {

        var context = new Mediator();
        context.RegisterHandler<TCommand, bool>(command);

        if (_handlers.TryGetValue(command.GetType(), out var handler))
        {
            // Assuming handler implements ICommandHandler<TCommand>
            var commandHandler = (ICommandHandler<ICommand>)handler;
            await commandHandler.Handle(command);
            //return Results<TResult>.Success(TResult);
            return Results<TResult>.Failure(new Error("2", "1", ErrorType.GenericFailure));
        }

        return Results<TResult>.Failure(new Error("2","1",ErrorType.GenericFailure));
        //return Results.FromResult(true);

    }

    public async Task<Results<TResult>> HandleQueryAsync<TQuery, TResult>(TQuery query, CancellationToken token)
        where TQuery : IQuery<TResult>
        where TResult : class?
    {

        var context = new Mediator();
        context.RegisterHandler<TQuery, bool>(query);

        if (_handlers.TryGetValue(query.GetType(), out var handler))
        {
            // Assuming handler implements ICommandHandler<TCommand>
            var commandHandler = (IQueryHandler<TQuery, TResult>)handler;
            var result = await commandHandler.Handle(query);
            //return Results<TResult>.Success(TResult);
            return Results<TResult>.Success(result);
        }

        return Results<TResult>.Failure(new Error("w","1", ErrorType.GenericFailure));
        //var handler = this.provider.GetRequiredService(this.configuration.GetHandler<TQuery>());
        //if (handler is IQueryHandler<TQuery, TResult> queryHandler)
        //{
        //    var result = await handler.Handle(context).ConfigureAwait(false);
        //    await this.HandleQueryInternalAsync(queryHandler, context).ConfigureAwait(false);
        //}
        //else
        //{
        //    throw new InvalidCastException($"Registered handler is not of type {typeof(IQueryHandler<TQuery, TResult>)}");
        //}
    }
}

*/