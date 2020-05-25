using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables
{
    public class OnStartEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent m_onStartEvent;

        private void Start() {
            m_onStartEvent.Invoke();
        }
    }
}