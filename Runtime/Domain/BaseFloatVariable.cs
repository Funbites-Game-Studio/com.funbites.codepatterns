using UnityEngine;

namespace CommonReferenceables
{
    public abstract class BaseFloatVariable : ScriptableObject
    {
        public float BaseValue;

        public int IntValue => (int)BaseValue;
    }
}