namespace Funbites.Patterns {
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;


    // ReSharper disable StaticMemberInGenericType
    public abstract class SingletonScriptableObject<TComponent> : ScriptableObject
        where TComponent : ScriptableObject {

        static TComponent instance;
        static bool hasInstance;
        static int instanceId;
        static readonly object lockObject = new object();

        public static TComponent Instance
        {
            get
            {
                lock (lockObject) {
                    if (hasInstance) {
                        return instance;
                    }

                    if (instance == null) {
                        string typeName = typeof(TComponent).Name;
                        instance = Resources.Load(typeName) as TComponent;
                        if (instance == null) {
                            
                            Debug.Log($"{typeName}: cannot find integration settings, creating default settings");
                            instance = ScriptableObject.CreateInstance<TComponent>();
                            instance.name = $"{typeName} Settings";

#if UNITY_EDITOR
                            if (!System.IO.Directory.Exists("Assets/Resources")) {
                                AssetDatabase.CreateFolder("Assets", "Resources");
                            }
                            AssetDatabase.CreateAsset(instance, "Assets/Resources/" + typeName + ".asset");
#endif
                        }
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
    }
}
