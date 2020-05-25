namespace Funbites.Patterns.ScriptableVariables
{
    public class IntVariablesSetter : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private IntVariable[] m_variables;

        public void SetToValue(int value) {
            foreach (var variable in m_variables) {
                variable.Value = value;
            }
        }
    }
}