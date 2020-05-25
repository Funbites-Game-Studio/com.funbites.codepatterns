using Sirenix.OdinInspector;
using UnityEngine;

namespace CommonReferenceables
{
    public abstract class BaseVariable<T> : ScriptableObject
    {
        [SerializeField, DisableIf("m_isConstant")]
        private T m_value;
#if UNITY_EDITOR
        [ShowInInspector, HideIf("m_isConstant")]
        private bool debug;
#endif
        [SerializeField]
        private bool m_isConstant = false;
        public T Value {
            get
            {
                return m_value;
            }
            set
            {
                if (m_isConstant) throw new System.Exception("You can not change a constant variable " + name);
#if UNITY_EDITOR
                if (debug) {
                    Debug.Log($"Variable ({name}) was changed to value: {value}");
                }
#endif
                m_value = value;
            }
        }
        /*
        public void ForceSerialization() {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        */
    }
}