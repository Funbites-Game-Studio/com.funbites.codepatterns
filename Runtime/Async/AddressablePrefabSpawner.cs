namespace Funbites.Patterns.Asynchronous {

    using System.Threading.Tasks;

    public class AddressablePrefabSpawner : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private bool m_spawnOnStart = true;

        [UnityEngine.SerializeField, Sirenix.OdinInspector.Required]
        private UnityEngine.AddressableAssets.AssetReferenceGameObject m_prefab = null;

        private Task<UnityEngine.GameObject> task;

        private async void Start() {
            if (!m_spawnOnStart) 
            {
                return;
            }
            await SpawnTask();
        }

        public async void Spawn() {
            await SpawnTask();
        }

        private async Task SpawnTask()
        {
            if (task == null)
            {
                var handle = m_prefab.InstantiateAsync();
                task = handle.Task;
                var newGameObject = await task;
                newGameObject.transform.SetParent(transform.parent);
                newGameObject.name = name;
                Destroy(gameObject);
            }
            else
            {
                throw new System.InvalidOperationException("You can't call Spawn more than once.");
            }
        }
    }
}