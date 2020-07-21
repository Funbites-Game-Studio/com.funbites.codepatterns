namespace Funbites.Patterns.ObjectPooling {
    using SerializeField = UnityEngine.SerializeField;
    using Animator = UnityEngine.Animator;

    [UnityEngine.DisallowMultipleComponent(), System.Serializable]
    public class Poolable : UnityEngine.MonoBehaviour
    {
        
        [SerializeField, Sirenix.OdinInspector.ToggleLeft]
        private bool m_returnToPoolOnParticleStop = false;

        [SerializeField, Sirenix.OdinInspector.ToggleLeft]
        [Sirenix.OdinInspector.InfoBox("Use this only you want your object to respond to the pool callbacks, but dont actually use a pool. " +
            "This is useful if you have a poolable prefab, but you want to put it directly to the scene or other prefab without using the pool", "m_dontUsePool", 
            InfoMessageType = Sirenix.OdinInspector.InfoMessageType.Warning)]
        private bool m_dontUsePool = false;

        [SerializeField]
        private UnityEngine.Events.UnityEvent m_onResetFromPool = null;

        [SerializeField]
        private UnityEngine.Events.UnityEvent m_onReturnToPool = null;

        [Sirenix.OdinInspector.ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        private IPoolListener[] listeners;
        [System.NonSerialized]
        private Animator gameObjectAnimator;
        private bool hasAnimator;

        [Sirenix.OdinInspector.ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        private Pool myPool;

        private bool hasPool;

        public UnityEngine.Events.UnityEvent OnReturnToPool => m_onReturnToPool;
        public UnityEngine.Events.UnityEvent OnResetFromPool => m_onResetFromPool;

        private bool isReturnedToPool;

        private void Awake()
        {
            gameObjectAnimator = GetComponent<Animator>();
            hasAnimator = gameObjectAnimator != null;
            listeners = GetComponentsInChildren<IPoolListener>();
            hasPool = false;
        }

        private void Start()
        {
            if (!hasPool)
            {
                if (!m_dontUsePool)
                    //TODO: maybe a info would be better than a warning because user optin to do not use pool, so it's the expected behaviour
                    Debugging.Logger.LogWarning($"Poolable game object does not have a pool, calling TakeFromPool: {name}", gameObject);
                TakeFromPool();
            }
        }

        internal void OnInitializePool(Pool pool)
        {
            Debugging.AssertionHelper.Assert(!m_dontUsePool, "Your poolable is marked to not use pool, but you are initializing it with a pool", gameObject);
            Debugging.AssertionHelper.AssertNotNull(pool, "Trying to start a poolable with a null pull", gameObject);
            myPool = pool;
            hasPool = true;
            gameObject.SetActive(false);
        }

        public void ReturnToPool()
        {
            ReturnToPool(false);
        }

        public void ReturnToPool(bool isDestroying)
        {
            if (isReturnedToPool)
                return;
            isReturnedToPool = true;

            for (int i = 0; i < listeners.Length; i++)
            {
                listeners[i].OnReturnToPool();
            }

            m_onReturnToPool.Invoke();

            if (!hasPool)
            {
                if (!m_dontUsePool)
                    Debugging.Logger.LogWarning($"Trying to return a object to pool, but it's not initialized by a pool: {gameObject.name}, the game object will be destroyed.", gameObject);
                Destroy(gameObject);
            }
            else
            {
                if (gameObject != null)
                {
                    gameObject.SetActive(false);
                    myPool.ReturnToPool(this, isDestroying);
                }
            }
        }

        internal void OnParticleSystemStopped()
        {
            if (m_returnToPoolOnParticleStop)
                ReturnToPool();
        }

        internal UnityEngine.GameObject TakeFromPoolAt(UnityEngine.Vector3 position)
        {
            transform.position = position;
            foreach (var positionSetter in GetComponents<IPoolPositionSetter>())
            {
                positionSetter.SetPositionFromPool(position);
            }
            return TakeFromPool();
        }

        internal UnityEngine.GameObject TakeFromPool()
        {
            gameObject.SetActive(true);
            if (hasAnimator)
            {
                foreach (ReturnToPoolBehaviour behaviour in gameObjectAnimator.GetBehaviours<ReturnToPoolBehaviour>())
                {
                    behaviour.Poolable = this;
                }
            }

            foreach (IPoolListener listener in listeners)
            {
                if (listener.enabled)
                    listener.OnLeavePool();
            }
            m_onResetFromPool.Invoke();
            isReturnedToPool = false;
            return gameObject;
        }
    }
}