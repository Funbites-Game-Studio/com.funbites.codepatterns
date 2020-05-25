using Sirenix.OdinInspector;
using UnityEngine;

namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableStringEventListener : GenericReferenceableEventListener<string, UnityUtils.Events.StringEvent> {
        [SerializeField, Required]
        private ReferenceableStringEvent m_referenceableStringEvent;

        protected override GenericReferenceableEvent<string, UnityUtils.Events.StringEvent> Event
        {
            get
            {
                return m_referenceableStringEvent;
            }
        }
    }
}
