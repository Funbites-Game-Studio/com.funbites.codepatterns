namespace Funbites.Patterns.ReferenceableEvents
{
    public abstract class GenericReferenceableEvent<ARGUMENT_TYPE, ARGUMENT_EVENT> : UnityEngine.ScriptableObject 
        where ARGUMENT_EVENT : UnityEngine.Events.UnityEvent<ARGUMENT_TYPE> {
#if UNITY_EDITOR
        [Sirenix.OdinInspector.ShowInInspector]
        private bool _debugBreakOnRaise;
#endif
        [Sirenix.OdinInspector.ShowInInspector, Sirenix.OdinInspector.ReadOnly]
        private readonly System.Collections.Generic.List<IGenericReferenceableEventListener<ARGUMENT_TYPE>> eventListeners = 
            new System.Collections.Generic.List<IGenericReferenceableEventListener<ARGUMENT_TYPE>>();
        [Sirenix.OdinInspector.Button]
        public void Raise(ARGUMENT_TYPE argument) {
#if UNITY_EDITOR
            if (_debugBreakOnRaise)
            {
                UnityEngine.Debug.LogWarning("Debug Breaking");
                UnityEngine.Debug.Break();
            }
#endif
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(argument);
        }

        public void RegisterListener(IGenericReferenceableEventListener<ARGUMENT_TYPE> listener) {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IGenericReferenceableEventListener<ARGUMENT_TYPE> listener) {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}