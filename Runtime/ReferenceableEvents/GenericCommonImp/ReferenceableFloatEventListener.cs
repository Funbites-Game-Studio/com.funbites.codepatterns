namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableFloatEventListener : GenericReferenceableEventListener<float, UnityUtils.Events.FloatEvent> {
        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private ReferenceableFloatEvent m_referenceableFloatEvent;

        protected override GenericReferenceableEvent<float, UnityUtils.Events.FloatEvent> Event
        {
            get
            {
                return m_referenceableFloatEvent;
            }
        }
    }
}
