namespace Funbites.Patterns.Asynchronous {
    [UnityEngine.AddComponentMenu(""), UnityEngine.DefaultExecutionOrder(-5000)]
    public class WorkScheduler : SingletonMonoBehaviour<WorkScheduler> {

        public float MaxWorkTime = 0.08f;
        public float MaxFrameTime = 0.1f;
        private bool didWorkThisFrame;
        private bool alreadyDidMaxWorkThisFrame;
        private float frameStartReferenceTime;
        private float workStartreferenceTime;

        private void Update() {
            frameStartReferenceTime = UnityEngine.Time.realtimeSinceStartup;
        }
        private float currentTime;
        public bool IsToSkipToNextFrame
        {
            get
            {
                if (alreadyDidMaxWorkThisFrame) return true;
                if (!didWorkThisFrame) {
                    workStartreferenceTime = UnityEngine.Time.realtimeSinceStartup;
                }
                currentTime = UnityEngine.Time.realtimeSinceStartup;
                if (currentTime - frameStartReferenceTime >= MaxFrameTime) {
                    alreadyDidMaxWorkThisFrame = true;
                    return true;
                }
                if (currentTime - workStartreferenceTime >= MaxWorkTime) {
                    alreadyDidMaxWorkThisFrame = true;
                    return true;
                }
                return false;
            }
        }

        private void LateUpdate() {
            didWorkThisFrame = false;
            alreadyDidMaxWorkThisFrame = false;
        }

        protected override void OnCreateInstance()
        {
            didWorkThisFrame = false;
            alreadyDidMaxWorkThisFrame = false;
        }
    }
}