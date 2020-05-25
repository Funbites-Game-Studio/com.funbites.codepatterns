namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableIntEventListener : GenericReferenceableEventListener<int, UnityUtils.Events.IntEvent> {
        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private ReferenceableIntEvent m_referenceableIntEvent = null;

        protected override GenericReferenceableEvent<int, UnityUtils.Events.IntEvent> Event
        {
            get
            {
                return m_referenceableIntEvent;
            }
        }
    }
}
