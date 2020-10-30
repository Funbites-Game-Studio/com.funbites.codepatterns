namespace Funbites.Patterns.ReferenceableEvents
{
    public abstract class GenericReferenceableEventListener<ARGUMENT_TYPE, ARGUMENT_EVENT> : 
        UnityEngine.MonoBehaviour, IGenericReferenceableEventListener<ARGUMENT_TYPE> 
        where ARGUMENT_EVENT : UnityEngine.Events.UnityEvent<ARGUMENT_TYPE>
    {
        protected abstract GenericReferenceableEvent<ARGUMENT_TYPE, ARGUMENT_EVENT> Event { get; }

        [UnityEngine.SerializeField, Sirenix.OdinInspector.ToggleLeft]
        private bool m_raiseForSpecificArgument = false;

        [UnityEngine.SerializeField, Sirenix.OdinInspector.ShowIf(nameof(m_raiseForSpecificArgument))]
        private ARGUMENT_TYPE m_specificArgument = default;

        public ARGUMENT_EVENT Response;

        private void OnEnable()
        {
            EnableListener();
        }

        private void OnDisable()
        {
            DisableListener();
        }

        public void EnableListener()
        {
            Event.RegisterListener(this);
        }

        public void DisableListener()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(ARGUMENT_TYPE argument)
        {
            if (!m_raiseForSpecificArgument || argument.Equals(m_specificArgument))
                Response.Invoke(argument);
        }
    }
}