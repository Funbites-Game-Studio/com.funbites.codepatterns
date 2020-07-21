namespace Funbites.Debugging
{
    using System;
    using System.Diagnostics;

    public class DevDebug : Patterns.SingletonScriptableObject<DevDebug> {
        [UnityEngine.SerializeField, Sirenix.OdinInspector.ReadOnly]
        private System.Collections.Generic.List<string> activeGroups;

        private void AddGroup(string group) {
            if (!IsGroupActive(group))
                activeGroups.Add(group);
        }

        private void RemoveGroup(string group) {
            if (IsGroupActive(group))
                activeGroups.Remove(group);
        }

        public static void Add(System.Type group) {
            Instance.AddGroup(group.Name);
        }

        public static void Remove(System.Type group) {
            Instance.RemoveGroup(group.Name);
        }

        private bool IsGroupActive(string group) {
            if (activeGroups == null) activeGroups = new System.Collections.Generic.List<string>();
            return activeGroups.Contains(group);
        }

        public static bool IsActive(System.Type group) {
            return Instance.IsGroupActive(group.Name);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string message, System.Type caller, UnityEngine.Object context = null) {
            if (IsActive(caller))
                UnityEngine.Debug.Log(message, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string message, UnityEngine.Object context) {
            if (IsActive(context.GetType()))
                UnityEngine.Debug.Log(message, TryToGetGameObjectReference(context));
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(System.Func<string> message, UnityEngine.Object context) {
            Log(message.Invoke(), context);            
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(string message, UnityEngine.Object context) 
        {
            if (IsActive(context.GetType()))
                UnityEngine.Debug.LogWarning(message, TryToGetGameObjectReference(context));
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        private static UnityEngine.Object TryToGetGameObjectReference(UnityEngine.Object context)
        {
            return (context is UnityEngine.MonoBehaviour) ?
                    (context as UnityEngine.MonoBehaviour).gameObject : context;
        }
    }
}