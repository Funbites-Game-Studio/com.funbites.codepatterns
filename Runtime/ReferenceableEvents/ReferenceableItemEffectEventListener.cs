using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables {
    public class ReferenceableItemEffectEventListener : GenericReferenceableEventListener<ItemEffectEventArgs, ItemEffectEvent> {
        [SerializeField, Required]
        private ReferenceableItemEffectEvent m_referenceableItemEffectEvent;

        protected override GenericReferenceableEvent<ItemEffectEventArgs, ItemEffectEvent> Event
        {
            get
            {
                return m_referenceableItemEffectEvent;
            }
        }
    }
}
