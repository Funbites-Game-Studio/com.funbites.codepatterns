namespace Funbites.Patterns.Asynchronous
{
    public class AsyncTimingRequest<T> : UnityEngine.CustomYieldInstruction, IAsyncTiming
    {
        public AsyncTimingOperationLoadingState CurrentState { get; private set; }

        private System.Func<AsyncTimingRequest<T>, System.Collections.Generic.IEnumerator<float>> coroutine;

        public System.Action OnCompleted;

        public T Result { get; set; }

        public override bool keepWaiting => CurrentState == AsyncTimingOperationLoadingState.Running;

        public AsyncTimingRequest()
        {
            CurrentState = AsyncTimingOperationLoadingState.Failed;
        }

        public AsyncTimingRequest(System.Func<AsyncTimingRequest<T>, System.Collections.Generic.IEnumerator<float>> coroutine)
        {
            CurrentState = AsyncTimingOperationLoadingState.Initialized;
            this.coroutine = coroutine;
        }

        public AsyncTimingRequest<T> Run()
        {
            if (CurrentState == AsyncTimingOperationLoadingState.Running)
                throw new AsyncTimingException(this, "Coroutine is alreay running"); ;
            CurrentState = AsyncTimingOperationLoadingState.Running;
            MEC.Timing.RunCoroutine(InnerRunOperation(coroutine));
            return this;
        }

        private System.Collections.Generic.IEnumerator<float> InnerRunOperation(System.Func<AsyncTimingRequest<T>, System.Collections.Generic.IEnumerator<float>> coroutine)
        {
            yield return MEC.Timing.WaitUntilDone(MEC.Timing.RunCoroutine(coroutine(this)));
            CurrentState = AsyncTimingOperationLoadingState.Completed;
            OnCompleted?.Invoke();
        }
    }
}