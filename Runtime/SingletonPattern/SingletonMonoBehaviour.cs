namespace Funbites.Patterns {
    public abstract class SingletonMonoBehaviour<TComponent> : UnityEngine.MonoBehaviour where TComponent : SingletonMonoBehaviour<TComponent>
    {
        static TComponent instance;
        static bool hasInstance;
        static int instanceId;
        static bool shuttingDown = false;
        static readonly object lockObject = new object();


        public static TComponent Instance {
            get {
                lock (lockObject) {
                    if (shuttingDown) {
                        Debugging.Logger.LogWarning("[Singleton] Instance '" + typeof(TComponent) +
                            "' already destroyed. Returning null.");
                        return null;
                    }
                    if (hasInstance) {
                        return instance;
                    }

                    instance = FindFirstInstance();
                    
                    if (instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new UnityEngine.GameObject();
                        instance = singletonObject.AddComponent<TComponent>();

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                        instance.OnCreateInstance();
                    }

                    hasInstance = true;
                    instanceId = instance.GetInstanceID();
                    return instance;
                }
            }
        }

        static TComponent[] FindInstances()
        {
            var objects = FindObjectsOfType<TComponent>();
            System.Array.Sort(objects, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
            return objects;
        }


        static TComponent FindFirstInstance()
        {
            var objects = FindInstances();
            return objects.Length > 0 ? objects[0] : null;
        }

        protected abstract void OnCreateInstance();


        /// <summary>
        /// Returns true if the object is NOT the singleton instance and should exit early from doing any redundant work.
        /// It will also log a warning if called from another instance in the editor during play mode.
        /// </summary>
        protected bool EnforceSingleton
        {
            get
            {
                if (GetInstanceID() == Instance.GetInstanceID()) {
                    return false;
                }

                if (UnityEngine.Application.isPlaying) {
                    enabled = false;
                }

                return true;
            }
        }

        /*
        /// <summary>
        /// Returns true if the object is the singleton instance.
        /// </summary>
        protected bool IsTheSingleton
        {
            get
            {
                lock (lockObject) {
                    // We compare against the last known instance ID because Unity destroys objects
                    // in random order and this may get called during teardown when the instance is
                    // already gone.
                    return GetInstanceID() == instanceId;
                }
            }
        }


        /// <summary>
        /// Returns true if the object is not the singleton instance.
        /// </summary>
        protected bool IsNotTheSingleton
        {
            get
            {
                lock (lockObject) {
                    return GetInstanceID() != instanceId;
                }
            }
        }
        */

        protected virtual void Start() {
            gameObject.name = typeof(TComponent).ToString() + " (Singleton)";
            if (UnityEngine.Application.isPlaying) {
                if (GetInstanceID() != instanceId) {
#if UNITY_EDITOR
                    Debugging.Logger.LogWarning($"A redundant instance ({name}) of singleton { typeof(TComponent) } is present in the scene.", this);
                    UnityEditor.EditorGUIUtility.PingObject(this);
#endif
                    gameObject.SetActive(false);
                } else
                {
                    OnCreateInstance();
                }
            }
            
        }

        private void OnApplicationQuit() {
            shuttingDown = true;
        }

        protected virtual void OnDestroy() {
            shuttingDown = true;
            lock (lockObject) {
                if (GetInstanceID() == instanceId) {
                    hasInstance = false;
                }
            }
        }
    }
}
