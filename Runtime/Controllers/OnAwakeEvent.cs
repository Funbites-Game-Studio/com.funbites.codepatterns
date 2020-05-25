using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables
{
    public class OnAwakeEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent m_onAwakeEvent;

        private void Awake() {
            m_onAwakeEvent.Invoke();
        }
    }
}