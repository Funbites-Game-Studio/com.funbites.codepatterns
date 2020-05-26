namespace Funbites.Patterns.Asynchronous
{
    public enum AsyncTimingOperationLoadingState
    {
        Initialized,
        Failed,
        Running,
        Completed
    }

    public class AsyncTimingOperationException : System.Exception
    {
        public readonly AsyncTimingOperationLoadingState StateWhenExceptionWasThow;
        public readonly AsyncTimingOperation TimingOperation;
        public AsyncTimingOperationException(AsyncTimingOperation timingOperation, string message) : base(message) {
            TimingOperation = timingOperation;
            StateWhenExceptionWasThow = timingOperation.CurrentState;

        }
    }
    public class AsyncTimingOperation : UnityEngine.CustomYieldInstruction
    {
        public AsyncTimingOperationLoadingState CurrentState { get; private set; }

        private System.Collections.Generic.IEnumerator<float> coroutine;

        public System.Action OnCompleted;

        public override bool keepWaiting => CurrentState == AsyncTimingOperationLoadingState.Running;

        public AsyncTimingOperation() {
            CurrentState = AsyncTimingOperationLoadingState.Failed;
        }

        public AsyncTimingOperation(System.Collections.Generic.IEnumerator<float> coroutine) {
            CurrentState = AsyncTimingOperationLoadingState.Initialized;
            this.coroutine = coroutine;
        }

        public AsyncTimingOperation Run() {
            if (CurrentState == AsyncTimingOperationLoadingState.Running)
                throw new System.Exception("Coroutine is alreay running");
            CurrentState = AsyncTimingOperationLoadingState.Running;
            MEC.Timing.RunCoroutine(InnerRunOperation(coroutine));
            return this;
        }

        private System.Collections.Generic.IEnumerator<float> InnerRunOperation(System.Collections.Generic.IEnumerator<float> coroutine) {
            yield return MEC.Timing.WaitUntilDone(MEC.Timing.RunCoroutine(coroutine));
            CurrentState = AsyncTimingOperationLoadingState.Completed;
            OnCompleted?.Invoke();
        }
    }
}