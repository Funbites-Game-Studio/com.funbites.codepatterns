namespace Funbites.Patterns.ObjectPooling {
    using Required = Sirenix.OdinInspector.RequiredAttribute;
    using AssetsOnly = Sirenix.OdinInspector.AssetsOnlyAttribute;
    using ReadOnly = Sirenix.OdinInspector.ReadOnlyAttribute;
    using ShowInInspector = Sirenix.OdinInspector.ShowInInspectorAttribute;
    using SerializeField = UnityEngine.SerializeField;

    public enum PoolState
    {
        NotInitialized,
        Initializing,
        Initialized
    }

    [UnityEngine.CreateAssetMenu(menuName = "Funbites/Object Pooling/Pool")]
    [System.Serializable]
    public class Pool : UnityEngine.ScriptableObject
    {
        

        [SerializeField/*, InfoBox("Use numbers relative of the max number it will apear on screen. Hardly less than 5.", InfoMessageType = InfoMessageType.Warning, VisibleIf = "ValidateInitialSize")*/]
        private int m_initialPoolSize = 3;

        //private bool ValidateInitialSize => m_initialSize < m_maxSizeToCreateMoreInstances;

        //[SerializeField, MinValue(0)]
        //private int m_maxSizeToCreateMoreInstances = 2;

        [SerializeField, Required, AssetsOnly]
        private Poolable m_prefab = null;

        private bool m_useWorkSchedulerToInstantiate = false;

        [SerializeField, Required]
        private bool m_worldPositionStays = false;

        [System.NonSerialized, ShowInInspector, ReadOnly]
        private ScenePoolController controller;


        [System.NonSerialized, ShowInInspector, ReadOnly]
        private System.Collections.Generic.Queue<Poolable> pooledObjects;
        [System.NonSerialized, ShowInInspector, ReadOnly]
        private System.Collections.Generic.List<Poolable> inGameObjects;

        [ShowInInspector, ReadOnly]
        private int currentSize;

        [ShowInInspector, ReadOnly]
        private int requestedSize;

        public Poolable Prefab
        {
            get
            {
                return m_prefab;
            }
        }

        [System.NonSerialized, ShowInInspector, ReadOnly]
        private PoolState state;

        //private bool isTakingInstance;
        [System.NonSerialized]
        private System.Text.StringBuilder nameBuilder;
        [System.NonSerialized]
        private int removeStartIndex;
        [System.NonSerialized]
        private UnityEngine.GameObject currentInstanceGameObject;
        [System.NonSerialized]
        private Poolable currentInstancePoolable;
        
        private const int DefaultNumberDecimalPlaces = 4;
        

        public System.Collections.Generic.IEnumerator<float> InitializePool(ScenePoolController sceneController)
        {
            if (state != PoolState.NotInitialized)
            {
                Debugging.Logger.LogError($"{sceneController.name} is trying to initialize a pool ({name}) that is already initialized by: {(controller != null ? controller.name : "NULLCONTROLLER")}.", sceneController);
                /*if (controller == null) {
                    Debug.LogError("Initializing a pool that is already initialized, but the controller is null", sceneController);
                }*/
            }
            else
            {
                state = PoolState.Initializing;
                //isTakingInstance = false;
                controller = sceneController;
                Debugging.AssertionHelper.AssertNotNull(m_prefab, () => $"m_prefab of {name} is null", this);
                pooledObjects = new System.Collections.Generic.Queue<Poolable>(m_initialPoolSize);
                inGameObjects = new System.Collections.Generic.List<Poolable>(m_initialPoolSize);
                removeStartIndex = m_prefab.gameObject.name.Length + 1;
                nameBuilder = new System.Text.StringBuilder(m_prefab.gameObject.name, removeStartIndex + DefaultNumberDecimalPlaces);
                nameBuilder.Append('_');
                currentSize = 0;
                requestedSize = 0;
                for (int i = 0; i < m_initialPoolSize; i++)
                {
                    yield return MEC.Timing.WaitUntilDone(MEC.Timing.RunCoroutine(CreateInitialInstances(m_worldPositionStays)));
                }
                state = PoolState.Initialized;
            }
        }

        public void ReturnAllObjectsToPool(bool isDestroying = false)
        {
            if (state != PoolState.Initialized)
            {
                Debugging.Logger.LogError($"Trying to return all objets to pool ({name}), but pool has not initialized. Is Destroying? {isDestroying.ToString()}");
                return;
            }
            Poolable poolable;
            for (int i = inGameObjects.Count - 1; i >= 0; i--)
            {
                poolable = inGameObjects[i];
                if (poolable != null)
                {
                    poolable.ReturnToPool(isDestroying);
                }
                else
                {
                    inGameObjects.RemoveAt(i);
                }
            }
        }

        public void FinalizePool(ScenePoolController scenePoolController)
        {
            if (state == PoolState.Initialized && scenePoolController == controller)
            {
                state = PoolState.NotInitialized;
                controller = null;
            }
            else
            {
                Debugging.Logger.LogError("You can only finalize a pool with the owner of the pool and when it had initialized.", scenePoolController.gameObject);
            }
        }

        private System.Collections.Generic.IEnumerator<float> CreateInitialInstances(bool worldPositionStays)
        {
            if (m_useWorkSchedulerToInstantiate) {
                while (Asynchronous.WorkScheduler.Instance.IsToSkipToNextFrame) {
                    yield return MEC.Timing.WaitForOneFrame;
                }
            }

            CreateInstance(worldPositionStays);
        }

        //private IEnumerator<float> CreateInstance()
        private void CreateInstance(bool worldPositionStays)
        {
            int val = requestedSize++;
            //while (WorkScheduler.Instance.IsToSkipToNextFrame)
            //{
            //    yield return 0;
            //}
            currentInstanceGameObject = Instantiate(m_prefab.gameObject);
            Debugging.AssertionHelper.Assert(currentInstanceGameObject.activeInHierarchy, () => $"Prefab '{currentInstanceGameObject.name}' is disabled!", m_prefab);
            nameBuilder.Append(val);
            currentInstanceGameObject.name = nameBuilder.ToString();
            nameBuilder.Remove(removeStartIndex, nameBuilder.Length - removeStartIndex);
            currentInstanceGameObject.transform.SetParent(controller.transform, worldPositionStays);
            currentInstanceGameObject.SetActive(false);
            currentInstancePoolable = currentInstanceGameObject.GetComponent<Poolable>();
            //while (WorkScheduler.Instance.IsToSkipToNextFrame || isTakingInstance)
            //{
            //    yield return 0;
            //}
            currentInstancePoolable.OnInitializePool(this);
            pooledObjects.Enqueue(currentInstancePoolable);
            currentSize++;
        }
        private Poolable GetNewInstance()
        {
            Debugging.AssertionHelper.Assert(state == PoolState.Initialized, () => $"You cant get a instance if the pool has not initialized. Pool: {name}");
            var newInstance = GetInstanceAndCreateNewIfNecessary();
            if (newInstance == null)
            {
                return null;
            }
            return newInstance;
        }
        public UnityEngine.GameObject GetInstanceAt(UnityEngine.Vector3 position)
        {
            return GetNewInstance().TakeFromPoolAt(position);
        }

        public UnityEngine.GameObject GetInstance()
        {
            return GetNewInstance().TakeFromPool();
        }

        internal void ReturnToPool(Poolable poolObject, bool isDestroying)
        {
            pooledObjects.Enqueue(poolObject);
            inGameObjects.Remove(poolObject);
            if (state == PoolState.Initialized && !isDestroying)
                poolObject.transform.SetParent(controller.transform, m_worldPositionStays);
        }

        //private Poolable GetInstanceAndCreateNewIfNecessary()
        //{
        //    AssertionHelper.Assert(state == PoolState.Initialized, () => $"Pool is not initialized. Name: {name}", this);
        //    AssertionHelper.Assert(pooledObjects.Count > 0, $"No instance available for pool. Name: {name}", this);
        //    Poolable poolObject;
        //    if (pooledObjects.Count <= m_maxSizeToCreateMoreInstances)
        //    {
        //        Debug.Log($"Creating instance for pool: {name}");
        //        //Timing.RunCoroutine(CreateInstance());
        //        CreateInstance();
        //    }
        //    poolObject = pooledObjects.Dequeue();
        //    inGameObjects.Add(poolObject);
        //    return poolObject;
        //}

        private Poolable GetInstanceAndCreateNewIfNecessary()
        {
            Debugging.AssertionHelper.Assert(state == PoolState.Initialized, () => $"Pool is not initialized. Name: {name}", this);
            //AssertionHelper.Assert(pooledObjects.Count > 0, $"No instance available for pool. Name: {name}", this);
            Poolable poolObject;
            if (pooledObjects.Count <= 0)
            {
                Debugging.Logger.Log($"Creating instance for pool: {name}");
                CreateInstance(m_worldPositionStays);
            }
            poolObject = pooledObjects.Dequeue();
            inGameObjects.Add(poolObject);
            return poolObject;
        }
    }
}