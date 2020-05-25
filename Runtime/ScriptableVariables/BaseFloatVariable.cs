using UnityEngine;

namespace Funbites.Patterns.ScriptableVariables
{
    public abstract class BaseFloatVariable : ScriptableObject
    {
        public float BaseValue;

        public int IntValue => (int)BaseValue;
    }
}