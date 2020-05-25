using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables {
    public class ReferenceableIntEventListener : GenericReferenceableEventListener<int, IntEvent> {
        [SerializeField, Required]
        private ReferenceableIntEvent m_referenceableIntEvent;

        protected override GenericReferenceableEvent<int, IntEvent> Event
        {
            get
            {
                return m_referenceableIntEvent;
            }
        }
    }
}
