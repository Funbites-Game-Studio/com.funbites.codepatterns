namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableStringEventListener : GenericReferenceableEventListener<string, UnityUtils.Events.StringEvent> {
        [UnityEngine.SerializeField]
        [Sirenix.OdinInspector.Required]
        private ReferenceableStringEvent m_referenceableStringEvent = null;

        protected override GenericReferenceableEvent<string, UnityUtils.Events.StringEvent> Event
        {
            get
            {
                return m_referenceableStringEvent;
            }
        }
    }
}
