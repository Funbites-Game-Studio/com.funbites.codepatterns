namespace Funbites.Patterns.ScriptableVariables
{
    public abstract class BaseVariable<T> : UnityEngine.ScriptableObject
    {
        [UnityEngine.SerializeField, Sirenix.OdinInspector.DisableIf("m_isConstant")]
        private T m_value;
#if UNITY_EDITOR
        [Sirenix.OdinInspector.ShowInInspector, Sirenix.OdinInspector.HideIf("m_isConstant")]
        private bool _debug = false;
#endif
        [UnityEngine.SerializeField]
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
                if (_debug) {
                    Debugging.Logger.Log($"Variable ({name}) was changed to value: {value}");
                }
#endif
                m_value = value;
            }
        }
    }
}