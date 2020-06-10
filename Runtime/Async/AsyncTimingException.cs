namespace Funbites.Patterns.Asynchronous
{

    public class AsyncTimingException : System.Exception
    {
        public readonly AsyncTimingOperationLoadingState StateWhenExceptionWasThow;
        public readonly IAsyncTiming TimingOperation;
        public AsyncTimingException(IAsyncTiming timingOperation, string message) : base(message)
        {
            TimingOperation = timingOperation;
            StateWhenExceptionWasThow = timingOperation.CurrentState;

        }
    }
}