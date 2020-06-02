namespace Funbites.Patterns {
    public static class SingletonScriptableObjectFunctions
    {

        public static void CreateAsset(string typeName, UnityEngine.ScriptableObject instance)
        {
#if UNITY_EDITOR
            if (!System.IO.Directory.Exists("Assets/Resources"))
            {
                UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");
            }
            UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/Resources/" + typeName + ".asset");
#else
            throw new System.Exception("Can't create in runtime");
#endif
        }
    }
    public abstract class SingletonScriptableObject<TComponent> : UnityEngine.ScriptableObject
        where TComponent : UnityEngine.ScriptableObject
    {

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
                        instance = UnityEngine.Resources.Load(typeName) as TComponent;
                        if (instance != null) {
                            
                            Debugging.Logger.Log($"{typeName}: cannot find integration settings, creating default settings");
                            instance = CreateInstance<TComponent>();
                            instance.name = $"{typeName} Settings";
                            SingletonScriptableObjectFunctions.CreateAsset(typeName, instance);
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
