using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables
{
    public abstract class GenericReferenceableEventListener<ARGUMENT_TYPE, ARGUMENT_EVENT> : MonoBehaviour, IGenericReferenceableEventListener<ARGUMENT_TYPE> where ARGUMENT_EVENT : UnityEvent<ARGUMENT_TYPE>
    {
        protected abstract GenericReferenceableEvent<ARGUMENT_TYPE, ARGUMENT_EVENT> Event { get; }

        [SerializeField, ToggleLeft]
        private bool m_raiseForSpecificArgument = false;

        [SerializeField, ShowIf("m_raiseForSpecificArgument")]
        private ARGUMENT_TYPE m_specificArgument;

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

    public interface IGenericReferenceableEventListener<ARGUMENT_TYPE>
    {
        void OnEventRaised(ARGUMENT_TYPE argument);
    }
}