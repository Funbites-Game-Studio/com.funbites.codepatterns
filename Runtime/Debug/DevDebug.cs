using Funbites.Patterns;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Funbites.Utils.Debugging {
    public class DevDebug : SingletonScriptableObject<DevDebug> {
        [SerializeField, ReadOnly]
        private List<string> activeGroups;

        private void AddGroup(string group) {
            if (!IsGroupActive(group))
                activeGroups.Add(group);
        }

        private void RemoveGroup(string group) {
            if (IsGroupActive(group))
                activeGroups.Remove(group);
        }

        public static void Add(Type group) {
            Instance.AddGroup(group.Name);
        }

        public static void Remove(Type group) {
            Instance.RemoveGroup(group.Name);
        }

        private bool IsGroupActive(string group) {
            if (activeGroups == null) activeGroups = new List<string>();
            return activeGroups.Contains(group);
        }

        public static bool IsActive(Type group) {
            return Instance.IsGroupActive(group.Name);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string message, Type caller, UnityEngine.Object context = null) {
            if (IsActive(caller))
                UnityEngine.Debug.Log(message, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string message, UnityEngine.Object context) {
            if (IsActive(context.GetType()))
                UnityEngine.Debug.Log(message, (context is MonoBehaviour) ? (context as MonoBehaviour).gameObject: context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(Func<string> message, UnityEngine.Object context) {
            if (IsActive(context.GetType()))
                UnityEngine.Debug.Log(message.Invoke(), (context is MonoBehaviour) ? (context as MonoBehaviour).gameObject : context);
        }
    }
}