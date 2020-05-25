using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables
{
    [CreateAssetMenu(menuName = "Josh/Events/Referenceable Item Effect")]
    public class ReferenceableItemEffectEvent : GenericReferenceableEvent<ItemEffectEventArgs, ItemEffectEvent>
    {
    }

    public class ItemEffectEventArgs
    {
        public int PlayerNumber;
        public string ItemID;
    }

    [System.Serializable]
    public class ItemEffectEvent : UnityEvent<ItemEffectEventArgs>
    {
    }
}