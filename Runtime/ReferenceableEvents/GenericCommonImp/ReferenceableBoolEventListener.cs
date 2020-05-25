namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableBoolEventListener : GenericReferenceableEventListener<bool, UnityUtils.Events.BooleanEvent> {
        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private ReferenceableBoolEvent m_referenceableBoolEvent = null;

        protected override GenericReferenceableEvent<bool, UnityUtils.Events.BooleanEvent> Event
        {
            get
            {
                return m_referenceableBoolEvent;
            }
        }
    }

}
