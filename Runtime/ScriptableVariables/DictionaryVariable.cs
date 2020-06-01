namespace Funbites.Patterns.ScriptableVariables {

    public abstract class DictionaryVariable<T> : UnityEngine.ScriptableObject, System.Collections.Generic.IEnumerable<T> where T : UnityEngine.ScriptableObject
    {
        private System.Collections.Generic.Dictionary<string, T> m_items = default;
        
        public T this[string key]
        {
            get
            {
                return m_items[key];
            }
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator() {
            return m_items.Values.GetEnumerator();
        }

        protected virtual void OnRegister(T thing) { }

        public void Register(T thing) {
            if (!m_items.ContainsKey(thing.name)) {
                OnRegister(thing);
                m_items.Add(thing.name, thing);
            } else {
                throw new System.ArgumentException("Element with name: " + thing.name + " already registered");
            }
        }


        protected virtual void OnRemove(T thing) { }

        public void Remove(T thing) {
            if (m_items.ContainsKey(thing.name)) {
                OnRemove(thing);
                m_items.Remove(thing.name);
            }
                
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool Contains(string key) {
            return m_items.ContainsKey(key);
        }
    }
}