namespace Funbites.Patterns.DDC {
    public abstract class SingletonBaseDescription<T> : UnityEngine.ScriptableObject, IDescription
    {
        [System.NonSerialized]
        private bool isInitialized = false;
        [System.NonSerialized]
        private T data = default;

        protected abstract T MakeInstance();

        public T Data {
            get
            {
                if (!isInitialized) {
                    data = MakeInstance();
					isInitialized = true;
                }
                return data;
            }
        }
    }
}