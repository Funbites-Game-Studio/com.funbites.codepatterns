using System;
using UnityEngine;
using UnityEngine.Events;

namespace CommonReferenceables {
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> {
    }
}