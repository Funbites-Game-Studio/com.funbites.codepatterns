using UnityEngine;

namespace CommonReferenceables {
    public class IntVariablesSetter : MonoBehaviour {
        [SerializeField]
        private IntVariable[] m_variables;

        public void SetToValue(int value) {
            foreach (var variable in m_variables) {
                variable.Value = value;
            }
        }
    }
}