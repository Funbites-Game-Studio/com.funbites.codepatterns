using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables
{
    public class OnTriggerEnterEvent : MonoBehaviour
    {
        [SerializeField]
        private ColliderEvent on_TriggerEnter;
        [SerializeField, ValueDropdown("GetTags")]
        private string m_tag = "";
#if UNITY_EDITOR
        public string[] GetTags() {
            return UnityEditorInternal.InternalEditorUtility.tags;
        }
#endif
        [SerializeField]
        private bool m_triggerOnceInFrame;
        [SerializeField]
        private bool m_triggerOnceInLifeTime;

        private bool hasTriggered;
        private bool alreadyTriggeredInFrame;

        private void OnEnable() {
            hasTriggered = false;
            alreadyTriggeredInFrame = false;
        }

        private bool CanTrigger => (!m_triggerOnceInFrame || (m_triggerOnceInFrame && !alreadyTriggeredInFrame)) &&
                    (!m_triggerOnceInLifeTime || (m_triggerOnceInLifeTime && !hasTriggered));

        private void OnTriggerEnter(Collider other)
        {
            if (string.IsNullOrEmpty(m_tag) || other.CompareTag(m_tag)) {
                if (CanTrigger) {
                    alreadyTriggeredInFrame = true;
                    hasTriggered = true;
                    on_TriggerEnter.Invoke(other);
                } 
            }
        }

        private void FixedUpdate()
        {
            alreadyTriggeredInFrame = false;
        }
    }
}