using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables
{
    [CreateAssetMenu(menuName = "Josh/Events/Referenceable Status Effect Added")]
    public class ReferenceableStatusEffectEvent : GenericReferenceableEvent<StatusEffectEventArgs, StatusEffectEvent>
    {
    }

    public enum StatusEffectOperation
    {
        Added,
        Removed
    }

    public class StatusEffectEventArgs
    {
        public int PlayerNumber { get; set; }
        public string StatusID { get; set; }
        public StatusEffectOperation Operation { get; set; }
    }

    [System.Serializable]
    public class StatusEffectEvent : UnityEvent<StatusEffectEventArgs>
    {
    }
}