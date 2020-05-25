using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables
{
    public class ReferenceableStatusEffectEventListener : GenericReferenceableEventListener<StatusEffectEventArgs, StatusEffectEvent>
    {
        [SerializeField, Required]
        private ReferenceableStatusEffectEvent m_referenceableEvent;

        protected override GenericReferenceableEvent<StatusEffectEventArgs, StatusEffectEvent> Event
        {
            get
            {
                return m_referenceableEvent;
            }
        }
    }
}