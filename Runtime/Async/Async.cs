using System;

namespace Funbites.Patterns.Asynchronous
{
    public static class Async
    {
        [Obsolete]
        public static void Run(System.Collections.Generic.IEnumerator<float> coroutine)
        {
            MEC.Timing.RunCoroutine(coroutine);
        }
        [Obsolete]
        public static float RunAndWait(System.Collections.Generic.IEnumerator<float> coroutine)
        {
            return MEC.Timing.WaitUntilDone(MEC.Timing.RunCoroutine(coroutine));
        }

    }
}