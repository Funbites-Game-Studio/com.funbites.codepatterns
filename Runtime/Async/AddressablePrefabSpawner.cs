namespace Funbites.Patterns.Asynchronous {
    public class AddressablePrefabSpawner : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private bool m_spawnOnStart = true;

        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private UnityEngine.AddressableAssets.AssetReferenceGameObject m_prefab = null;

        private AsyncTimingOperation timingOperation;

        private void Start() {
            if (m_spawnOnStart) Spawn();
        }

        public void Spawn() {
            if (timingOperation == null)
            {
                timingOperation = new AsyncTimingOperation(SpawnCoroutine());
                timingOperation.Run();
            } else
            {
                throw new AsyncTimingOperationException(timingOperation, "You can't call Spawn when a timing operation has already started.");
            }
        }

        private System.Collections.Generic.IEnumerator<float> SpawnCoroutine() {
            var handle = m_prefab.InstantiateAsync();
            while (!handle.IsDone) yield return MEC.Timing.WaitForOneFrame;
            var newGameObject = handle.Result;
            newGameObject.transform.SetParent(transform.parent);
            newGameObject.name = name;
            Destroy(gameObject);
        }
    }
}