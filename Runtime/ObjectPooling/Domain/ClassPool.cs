namespace Funbites.ObjectPooling {
    public class ClassPool<T> where T : new() {
        private System.Collections.Generic.Queue<T> poolOfObjects = null;
        public ClassPool(int initialSize) {
            poolOfObjects = new System.Collections.Generic.Queue<T>(initialSize);
        }

        public int CurrentSize { get { return poolOfObjects.Count; } }

        public T GetInstance() {
            T result;
            result = (poolOfObjects.Count > 0) ? poolOfObjects.Dequeue() : new T();
            return result;
        }

        public void ReturnToPool(T instance) {
            poolOfObjects.Enqueue(instance);
        }
    }
}