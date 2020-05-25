namespace Funbites.Patterns {
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;


    // ReSharper disable StaticMemberInGenericType
    public abstract class SingletonMonoBehavior<TComponent> : MonoBehaviour
        where TComponent : MonoBehaviour {
        static TComponent instance;
        static bool hasInstance;
        static int instanceId;
        static bool shuttingDown = false;
        static readonly object lockObject = new object();


        public static TComponent Instance
        {
            get
            {
                lock (lockObject) {
                    if (shuttingDown) {
                        Debug.LogWarning("[Singleton] Instance '" + typeof(TComponent) +
                            "' already destroyed. Returning null.");
                        return null;
                    }
                    if (hasInstance) {
                        return instance;
                    }

                    if (instance == null) {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<TComponent>();
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }

                    hasInstance = true;
                    instanceId = instance.GetInstanceID();
                    return instance;
                }
            }
        }


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

                if (Application.isPlaying) {
                    enabled = false;
                }

                return true;
            }
        }


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
                    // We compare against the last known instance ID because Unity destroys objects
                    // in random order and this may get called during teardown when the instance is
                    // already gone.
                    return GetInstanceID() != instanceId;
                }
            }
        }


        // ReSharper disable once VirtualMemberNeverOverridden.Global
        protected virtual void Start() {
            gameObject.name = typeof(TComponent).ToString() + " (Singleton)";
            if (Application.isPlaying) {
                if (GetInstanceID() != instanceId) {
#if UNITY_EDITOR
                    Debug.LogWarning("A redundant instance (" + name + ") of singleton " + typeof(TComponent) + " is present in the scene.", this);
                    EditorGUIUtility.PingObject(this);
#endif
                    Destroy(gameObject);
                }
            }
            
        }

        private void OnApplicationQuit() {
            shuttingDown = true;
        }


        // ReSharper disable once VirtualMemberNeverOverridden.Global
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
