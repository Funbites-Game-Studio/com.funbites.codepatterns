namespace Funbites.Patterns.Asynchronous
{
    public interface IAsyncTiming 
    {
        AsyncTimingOperationLoadingState CurrentState { get; }
    }
}