namespace Funbites.Debugging
{
    public class Logger : Patterns.SingletonScriptableObject<Logger>
    {
        public static void Log(string message, UnityEngine.Object context = null) {
            UnityEngine.Debug.Log(message, context);
        }

        public void LogFromEvent(string message)
        {
            Log(message);
        }
        public void LogWarningFromEvent(string message)
        {
            LogWarning(message);
        }

        public void LogErrorFromEvent(string message)
        {
            LogError(message);
        }

        public static void LogWarning(string message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.LogWarning(message, context);
        }

        internal static void LogError(string message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.LogError(message, context);
        }
    }
}