namespace Funbites.Patterns.ScriptableVariables
{
    public abstract class BaseFloatVariable : UnityEngine.ScriptableObject
    {
        public float BaseValue;

        public int IntValue => (int)BaseValue;
    }
}