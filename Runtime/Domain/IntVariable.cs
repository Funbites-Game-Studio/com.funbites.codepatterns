using UnityEngine;

namespace CommonReferenceables
{
    [CreateAssetMenu(menuName = "Common Referencables/Variables/Integer Variable")]
    public class IntVariable : BaseVariable<int>
    {
        public void Increment(int value) {
            Value += value;
        }
    }
}