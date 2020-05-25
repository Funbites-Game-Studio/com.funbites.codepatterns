using System;

namespace Funbites.Debugging
{
    [UnityEngine.CreateAssetMenu(menuName = "Funbites/Debugging/Logger")]
    public class Logger : Patterns.SingletonScriptableObject<Logger>
    {
        public static void Log(string message, UnityEngine.Object context = null) {
            UnityEngine.Debug.Log(message, context);
        }

        public void LogFromEvent(string message)
        {
            Log(message);
        }

        public static void LogWarning(string message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.LogWarning(message, context);
        }
    }
}