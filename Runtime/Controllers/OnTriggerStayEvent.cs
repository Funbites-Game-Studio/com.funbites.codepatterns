using Sirenix.OdinInspector;
using UnityEngine;
//using ScriptUtils;

namespace CommonReferenceables
{
    public class OnTriggerStayEvent : MonoBehaviour
    {
        [SerializeField]
        private ColliderEvent m_onTriggerStay;
        [ShowInInspector]
        public bool IsActive { get; set; }
        [Tooltip("In Seconds")]
        [SerializeField]
        private float m_interval;
        [SerializeField, ToggleLeft]
        private bool m_triggerOnceInFrame;
        [SerializeField, ValueDropdown("GetTags")]
        private string m_tag = "";
#if UNITY_EDITOR
        public string[] GetTags() {
            return UnityEditorInternal.InternalEditorUtility.tags;
        }
#endif
        [SerializeField]
        private LayerMask layerMask = -1;

        private float elapsedTime;
        private bool alreadyTriggered;

        private void Awake() {
            IsActive = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!IsActive) return;
            if (ValidateCollider(other)) {
                elapsedTime += Time.deltaTime;

                var canTrigger = m_triggerOnceInFrame ? !alreadyTriggered : true;

                if (canTrigger)
                    if (elapsedTime >= m_interval) {
                        elapsedTime = 0;
                        alreadyTriggered = true;
                        m_onTriggerStay.Invoke(other);
                    }
            }
        }
        private readonly int untaggedHash = "Untagged".GetHashCode();
        private bool ValidateCollider(Collider other) {
            return false;
            //TODO: fix this
            //return layerMask.HasLayer(other.gameObject.layer) &&
            //    (string.IsNullOrEmpty(m_tag) || m_tag.GetHashCode() == untaggedHash || other.CompareTag(m_tag));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ValidateCollider(other) && IsActive) {
                var canTrigger = m_triggerOnceInFrame ? !alreadyTriggered : true;

                if (canTrigger)
                    elapsedTime = 0;
            }
        }

        private void FixedUpdate()
        {
            alreadyTriggered = false;
        }
    }
}