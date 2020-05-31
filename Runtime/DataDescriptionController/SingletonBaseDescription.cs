namespace Funbites.Patterns.DDC {
    public abstract class SingletonBaseDescription<T> : SingletonScriptableObject<SingletonBaseDescription<T>>, IDescription
    {
        [System.NonSerialized]
        private bool isInitialized;
        [System.NonSerialized]
        private T data;

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