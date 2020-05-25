using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables {
    public abstract class GenericReferenceableEvent<ARGUMENT_TYPE, ARGUMENT_EVENT> : ScriptableObject where ARGUMENT_EVENT : UnityEvent<ARGUMENT_TYPE> {
        [ShowInInspector, ReadOnly]
        private readonly List<IGenericReferenceableEventListener<ARGUMENT_TYPE>> eventListeners = new List<IGenericReferenceableEventListener<ARGUMENT_TYPE>>();
        [Button]
        public void Raise(ARGUMENT_TYPE argument) {
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