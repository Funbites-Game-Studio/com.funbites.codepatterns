using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables {
    public class ReferenceableStringEventListener : GenericReferenceableEventListener<string, StringEvent> {
        [SerializeField, Required]
        private ReferenceableStringEvent m_referenceableStringEvent;

        protected override GenericReferenceableEvent<string, StringEvent> Event
        {
            get
            {
                return m_referenceableStringEvent;
            }
        }
    }
}
