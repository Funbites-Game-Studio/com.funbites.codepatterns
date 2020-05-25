using Sirenix.OdinInspector;
using UnityEngine;

namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableIntEventListener : GenericReferenceableEventListener<int, UnityUtils.Events.IntEvent> {
        [SerializeField, Required]
        private ReferenceableIntEvent m_referenceableIntEvent;

        protected override GenericReferenceableEvent<int, UnityUtils.Events.IntEvent> Event
        {
            get
            {
                return m_referenceableIntEvent;
            }
        }
    }
}
