using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables {
    public class ReferenceableEventListener : MonoBehaviour, IReferenceableEventListener {

        [SerializeField, Tooltip("Event to register with."),Required]
        private ReferenceableEvent m_event;

        [SerializeField, Tooltip("Response to invoke when Event is raised.")]
        private UnityEvent m_response;

        private void OnEnable() {
            m_event.RegisterListener(this);
        }

        private void OnDisable() {
            m_event.UnregisterListener(this);
        }

        public void OnEventRaised() {
            m_response.Invoke();
        }
    }

    public interface IReferenceableEventListener
    {
        void OnEventRaised();
    }
}
