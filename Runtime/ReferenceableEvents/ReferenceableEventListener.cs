namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableEventListener : UnityEngine.MonoBehaviour, IReferenceableEventListener {

        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private ReferenceableEvent m_event = null;

        [UnityEngine.SerializeField]
        private UnityEngine.Events.UnityEvent m_response;

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


}
