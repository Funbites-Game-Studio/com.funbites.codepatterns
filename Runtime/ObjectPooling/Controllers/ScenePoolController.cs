using MEC;
using Sirenix.OdinInspector;

namespace Funbites.Patterns.ObjectPooling {
    public enum ScenePoolState
    {
        NotInitialized,
        Initializing,
        Initialized,
        Finalizing
    }
    public class ScenePoolController : UnityEngine.MonoBehaviour
    {
        [Sirenix.OdinInspector.InfoBox("This component is not initialized automatically, you must call Initialize via code, because it uses coroutines that you instantiate the prefabs.", Sirenix.OdinInspector.InfoMessageType.Warning)]
        [Sirenix.OdinInspector.ShowInInspector]
        private bool devDebug
        {
            get { return Debugging.DevDebug.IsActive(typeof(ScenePoolController)); }
            set
            {
                if (value)
                {
                    Debugging.DevDebug.Add(typeof(ScenePoolController));
                }
                else
                {
                    Debugging.DevDebug.Remove(typeof(ScenePoolController));
                }
            }
        }

        [UnityEngine.SerializeField]
        private Pool[] m_scenesPool = null;



        [Sirenix.OdinInspector.ShowInInspector, Sirenix.OdinInspector.ReadOnly]
        public ScenePoolState State { get; private set; } = ScenePoolState.NotInitialized;
        [ShowInInspector, ReadOnly]
        protected System.Collections.Generic.List<Pool> allScenePools = new System.Collections.Generic.List<Pool>();

        public bool AddPool(Pool pool, out CoroutineHandle loading)
        {
            if (allScenePools.Contains(pool))
            {
                loading = default(CoroutineHandle);
                return false;
            }
            allScenePools.Add(pool);
            loading = Timing.RunCoroutine(pool.InitializePool(this));
            return true;
        }

        public bool RemovePool(Pool pool)
        {
            if (!allScenePools.Contains(pool))
            {
                return false;
            }
            allScenePools.Remove(pool);
            pool.ReturnAllObjectsToPool(true);
            pool.DestroyObjectsInPool();
            return true;
        }

        public Pool[] CopyPoolListCurrentState()
        {
            return allScenePools.ToArray();
        }


        private void Awake()
        {
            isQuitting = false;
            State = ScenePoolState.NotInitialized;
        }

        public System.Collections.Generic.IEnumerator<float> InitializePool(params Pool[] aditionalScenePools)
        {
            if (State == ScenePoolState.NotInitialized)
            {
                Debugging.DevDebug.Log(() => $"Initializing pools {name}", this);
                State = ScenePoolState.Initializing;
                allScenePools.AddRange(m_scenesPool);
                allScenePools.AddRange(aditionalScenePools);
                foreach (Pool pool in allScenePools)
                {
                    if (pool == null)
                    {
                        Debugging.Logger.LogError("Pool element is null", gameObject);
                    }
                    else
                    {
                        while (Asynchronous.WorkScheduler.Instance.IsToSkipToNextFrame)
                        {
                            yield return 0;
                        }
                        Debugging.DevDebug.Log(() => $"Initializing pool: {pool.name}", this);
                        yield return MEC.Timing.WaitUntilDone(MEC.Timing.RunCoroutine(pool.InitializePool(this)));
                        Debugging.DevDebug.Log(() => $"Initialized pool: {pool.name}", this);
                    }
                }
                Debugging.DevDebug.Log(() => $"Initialized pools {name}", this);
                State = ScenePoolState.Initialized;
            }
            else
            {
                Debugging.Logger.LogError("Trying to initialize a pool that is already initialized");
            }
        }

        public Asynchronous.AsyncTimingOperation FinalizePool(bool destroySelf, bool isDestroying = false)
        {
            if (State != ScenePoolState.Initialized)
                return new Asynchronous.AsyncTimingOperation();
            State = ScenePoolState.Finalizing;
            var result = new Asynchronous.AsyncTimingOperation(FinalizePoolCoroutine(destroySelf, isDestroying));
            result.Run();
            return result;
        }

        public System.Collections.Generic.IEnumerator<float> FinalizePoolCoroutine(bool destroySelf, bool isDestroying = false)
        {
            foreach (Pool pool in allScenePools)
            {
                pool.ReturnAllObjectsToPool(isDestroying);
                pool.FinalizePool(this);
                yield return Timing.WaitForOneFrame;
            }

            if (destroySelf)
            {
                Destroy(gameObject);
            }
            yield return 0;
            State = ScenePoolState.NotInitialized;
        }

        private bool isQuitting;

        public void OnApplicationQuit()
        {
            isQuitting = true;
        }

        private void OnDestroy()
        {
            if (!isQuitting && State == ScenePoolState.Initialized)
                FinalizePool(false, true);
        }
        
        public void ReturnAllObjectsToPool()
        {
            foreach (Pool pool in allScenePools)
            {
                pool.ReturnAllObjectsToPool(false);
            }
        }
        
    }
}