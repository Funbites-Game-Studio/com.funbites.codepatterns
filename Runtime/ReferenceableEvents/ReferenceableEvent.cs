namespace Funbites.Patterns.ReferenceableEvents
{
    [UnityEngine.CreateAssetMenu(menuName = "Funbites/Referenceable/Event")]
    public class ReferenceableEvent : UnityEngine.ScriptableObject
    {
#if UNITY_EDITOR
        [Sirenix.OdinInspector.ShowInInspector]
        private bool _debugBreakOnRaise;
#endif
        [Sirenix.OdinInspector.ShowInInspector, Sirenix.OdinInspector.ReadOnly]
        private readonly System.Collections.Generic.List<IReferenceableEventListener> eventListeners = 
            new System.Collections.Generic.List<IReferenceableEventListener>();
        [Sirenix.OdinInspector.Button]
        public void Raise() {
#if UNITY_EDITOR
            if (_debugBreakOnRaise) {
                UnityEngine.Debug.LogWarning("Debug Breaking");
                UnityEngine.Debug.Break();
            }
#endif
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(IReferenceableEventListener listener) {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IReferenceableEventListener listener) {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}