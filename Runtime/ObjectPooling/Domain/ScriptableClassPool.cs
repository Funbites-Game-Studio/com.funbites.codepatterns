namespace Funbites.ObjectPooling {
    public class ScriptableClassPool<T> : UnityEngine.ScriptableObject where T : new() {
        [UnityEngine.SerializeField]
        private int m_initialSize = 5;
        [Sirenix.OdinInspector.ShowInInspector]
        public int CurrentSize { get { return classPool.CurrentSize; } }

        private ClassPool<T> classPool;

        public void Initialize() {
            classPool = new ClassPool<T>(m_initialSize);
        }

        public void FinalizePool() {
            classPool = null;
        }
       

        public T GetInstance() {
            return classPool.GetInstance();
        }

        public void ReturnToPool(T instance) {
            classPool.ReturnToPool(instance);
        }
    }
}