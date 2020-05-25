namespace Funbites.Patterns.ScriptableVariables
{
    [UnityEngine.CreateAssetMenu(menuName = "Funbites/Scriptable Variables/Integer")]
    public class IntVariable : BaseVariable<int>
    {
        public void Increment(int value) {
            Value += value;
        }
    }
}