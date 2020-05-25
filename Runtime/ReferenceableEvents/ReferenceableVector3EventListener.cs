using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables {
    public class ReferenceableVector3EventListener : GenericReferenceableEventListener<Vector3, Vector3Event> {
        [SerializeField, Required]
        private ReferenceableVector3Event m_referenceableVector3Event;

        protected override GenericReferenceableEvent<Vector3, Vector3Event> Event
        {
            get
            {
                return m_referenceableVector3Event;
            }
        }

        public void ChangeEvent(ReferenceableVector3Event newEvent) {
            m_referenceableVector3Event.UnregisterListener(this);
            m_referenceableVector3Event = newEvent;
            m_referenceableVector3Event.RegisterListener(this);
        }
    }
}
