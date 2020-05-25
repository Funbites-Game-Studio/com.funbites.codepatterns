using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Funbites.Utils.Debugging
{
    public static class AssertionHelper
    {
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertNotNull(object target, string errorMessage, Object context = null)
        {
            Assert(target != null, errorMessage, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertNull(object target, string errorMessage, Object context = null) {
            Assert(target == null, errorMessage, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertNotNull(object target, System.Func<string> errorMessage, Object context = null) {
            Assert(target != null, errorMessage, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(bool assertion, string errorMessage, Object context = null)
        {
            if (!assertion)
                Error(errorMessage, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(bool assertion, System.Func<string> errorMessage, Object context = null) {
            if (!assertion)
                Error(errorMessage.Invoke(), context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Error(string errorMessage, Object context = null)
        {
            Debug.LogException(new System.Exception(errorMessage), context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertEquals(int a, int b, string message, Object context) {
            Assert(a == b, message, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertEquals(float a, float b, string message, Object context)
        {
            Assert(Mathf.Abs(a - b) <= Mathf.Epsilon, message, context);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertEquals(float a, float b, System.Func<string> message, Object context) {
            Assert(Mathf.Abs(a - b) <= Mathf.Epsilon, message, context);
        }
    }
}