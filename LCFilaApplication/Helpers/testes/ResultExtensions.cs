namespace LCFilaApplication.Helpers.testes;

public static class ResultExtensions
{

    public static Results<TResult> Select<TFrom, TResult>(
        this Results<TFrom> source, // The target for the extension
        Func<TFrom, TResult> selector) // The mapping/selector method
    {
        return source.Match(
            onSuccess: r => selector(r), // success -> run the selector and implicitly convert to Result<TResult>
            onFailure: e => Results<TResult>.Failure(e)); // error -> return a failed Result<TResult>
    }

    public static Results<TResult> SelectMany<TSource, TMiddle, TResult>(
        this Results<TSource> source, // The target for the extension
        Func<TSource, Results<TMiddle>> collectionSelector, // How to map to the Result<TMiddle> type
        Func<TSource, TMiddle, TResult> resultSelector) // How to map a TMiddle to a TResult
    {
        return source.Match(
            onSuccess: r => // success -> run the selectors
            {
                // First run the "collection selector"
                Results<TMiddle> result = collectionSelector(r);

                // If result is a success, we run the "result selector" to
                // get the final TResult. If it is not a success, then
                // Select() just passes the error through as a failed Result<TResult>
                return result.Select(v => resultSelector(r, v));
            },
            onFailure: e => Results<TResult>.Failure(e)); // error -> return a failed Result<TResult>
    }

    public static Results<T> ToResult<T>(this T source)
        => Results<T>.Success(source);

    public static Results<IEnumerable<T>> Sequence<T>(this IEnumerable<Results<T>> results)
    {
        var zero = Results<IEnumerable<T>>.Success([]);
        return results.Aggregate(zero, (accumulated, result) =>
            from previous in accumulated
            from next in result
            select previous.Append(next));
    }
    //public static TResult Match<TValue, TResult>(
    //    this IResult result,
    //    Func<TValue, TResult> onSuccess,
    //    Func<Error, TResult> onFailure)
    //{
    //    if (result is StrictResult<TValue> strict)
    //    {
    //        return strict.Match(onSuccess, onFailure);
    //    }
    //    else if (result is Result<TValue> free)
    //    {
    //        return free.IsSuccess ? onSuccess(free.Value!) : onFailure(free.Error!);
    //    }

    //    throw new InvalidOperationException("Unknown result type.");
    //}


}

public static class TaskResultExtensions
{
    public static async Task<Results<TResult>> SelectMany<TSource, TMiddle, TResult>(
        this Task<Results<TSource>> source,
        Func<TSource, Task<Results<TMiddle>>> collectionSelector,
        Func<TSource, TMiddle, TResult> resultSelector)
    {
        // await the Task<Result<T>> to get the result
        Results<TSource> result = await source;
        return
            await result.Match(
                onSuccess: async r =>
                {
                    // we need a second await here
                    Results<TMiddle> result = await collectionSelector(r);
                    return result.Select(v => resultSelector(r, v));
                },
                // The failure path is synchronous, so wrap the failure in a `Task<>`
                onFailure: e => Task.FromResult(Results<TResult>.Failure(e)));
    }

}