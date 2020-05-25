using System;
using UnityEngine;

namespace CommonReferenceables {
    [CreateAssetMenu(menuName = "Common Referencables/Float Event")]
    public class ReferenceableFloatEvent : GenericReferenceableEvent<float, FloatEvent>
    {
        public void RegisterListener(object onDeadEnemy)
        {
            throw new NotImplementedException();
        }
    }
}
