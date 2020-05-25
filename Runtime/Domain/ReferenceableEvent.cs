using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace CommonReferenceables {
    [CreateAssetMenu(menuName = "Common Referencables/Event")]
    public class ReferenceableEvent : ScriptableObject {
#if UNITY_EDITOR
        [ShowInInspector]
        private bool _debugBreakOnRaise;
#endif
        [ShowInInspector, ReadOnly]
        private readonly List<IReferenceableEventListener> eventListeners = new List<IReferenceableEventListener>();
        [Button]
        public void Raise() {
#if UNITY_EDITOR
            if (_debugBreakOnRaise) {
                Debug.LogWarning("Debug Breaking");
                Debug.Break();
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