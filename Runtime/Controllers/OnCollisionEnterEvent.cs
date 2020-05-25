using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables {
    public class OnCollisionEnterEvent : MonoBehaviour {
        [SerializeField]
        private CollisionEvent m_onCollisionEnter;
        [SerializeField]
        private string m_tag = "";

        private void OnCollisionEnter(Collision collision)
        {
            if (string.IsNullOrEmpty(m_tag) || collision.gameObject.CompareTag(m_tag)) {
                m_onCollisionEnter.Invoke(collision);
            }
        }
    }

    [System.Serializable]
    public class CollisionEvent : UnityEvent<Collision> { }
}