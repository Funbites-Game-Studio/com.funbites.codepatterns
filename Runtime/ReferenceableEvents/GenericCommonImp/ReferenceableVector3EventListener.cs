namespace Funbites.Patterns.ReferenceableEvents
{
    public class ReferenceableVector3EventListener : GenericReferenceableEventListener<UnityEngine.Vector3, UnityUtils.Events.Vector3Event> {
        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private ReferenceableVector3Event m_referenceableVector3Event;

        protected override GenericReferenceableEvent<UnityEngine.Vector3, UnityUtils.Events.Vector3Event> Event
        {
            get
            {
                return m_referenceableVector3Event;
            }
        }
    }
}
