namespace Funbites.Patterns.ScriptableVariables { 
    public abstract class VariableArray <T> : UnityEngine.ScriptableObject
    {
        public T[] Values;

        public T this[int i]
        {
            get
            {
                return Values[i];
            }
            set
            {
                Values[i] = value;
            }
        }
    }
}
