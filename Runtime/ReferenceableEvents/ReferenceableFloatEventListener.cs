using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables {
    public class ReferenceableFloatEventListener : GenericReferenceableEventListener<float, FloatEvent> {
        [SerializeField, Required]
        private ReferenceableFloatEvent m_referenceableFloatEvent;

        protected override GenericReferenceableEvent<float, FloatEvent> Event
        {
            get
            {
                return m_referenceableFloatEvent;
            }
        }
    }
}
