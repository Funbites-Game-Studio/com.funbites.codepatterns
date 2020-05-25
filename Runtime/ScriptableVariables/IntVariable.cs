using UnityEngine;

namespace Funbites.Patterns.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Common Referencables/Variables/Integer Variable")]
    public class IntVariable : BaseVariable<int>
    {
        public void Increment(int value) {
            Value += value;
        }
    }
}