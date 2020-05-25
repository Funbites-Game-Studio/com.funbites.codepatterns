using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables {
    public class ReferenceableBoolEventListener : GenericReferenceableEventListener<bool, BooleanEvent> {
        [SerializeField, Required]
        private ReferenceableBoolEvent m_referenceableBoolEvent;

        protected override GenericReferenceableEvent<bool, BooleanEvent> Event
        {
            get
            {
                return m_referenceableBoolEvent;
            }
        }
    }
}
