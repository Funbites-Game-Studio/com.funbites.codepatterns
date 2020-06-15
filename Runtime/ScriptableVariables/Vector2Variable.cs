namespace Funbites.Patterns.ScriptableVariables
{
    [UnityEngine.CreateAssetMenu(menuName = "Funbites/Scriptable Variables/Vector2")]
    [System.Serializable]
    public class Vector2Variable : BaseVariable<UnityEngine.Vector2>
    {
        public float X {
            get => Value.x;
            set {
                var val = Value;
                val.x = value;
                Value = val;
            }
        }
        public float Y {
            get => Value.y;
            set {
                var val = Value;
                val.y = value;
                Value = val;
            }
        }

        public void IncrementXClampedByY(float increment)
        {
            X = UnityEngine.Mathf.Min(X + increment, Y);
        }
    }
}

