using System.Collections;
using System.Collections.Generic;

namespace CommonReferenceables {
    public class RefList<T> : IReadOnlyList<T> {
        public List<List<T>> lists;

        public RefList() {
            lists = new List<List<T>>();
        }

        public T this[int index]
        {
            get
            {
                if (index < 0) throw new System.IndexOutOfRangeException("Index cannot be less than 0. Provided:" + index);
                foreach (List<T> list in lists) {
                    if (index >= list.Count) {
                        index -= list.Count;
                    } else {
                        return list[index];
                    }
                }
                throw new System.IndexOutOfRangeException("Index is greater than all collections. Provided:" + index);
            }
        }

        public int Count
        {
            get
            {
                int result = 0;
                foreach (List<T> list in lists) {
                    result += list.Count;
                }
                return result;
            }
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (var list in lists)
                foreach (T element in list)
                    yield return element;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void AddRefList(List<T> list) {
            lists.Add(list);
        }

    }
}