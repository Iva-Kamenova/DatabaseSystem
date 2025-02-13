using System.Collections;

namespace DatabaseProject.MyUtils
{
    public class MyList<T> : IList<T>
    {
        private readonly EqualityComparer<T> _equalityComparer = EqualityComparer<T>.Default;
        private T[] _array;
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public MyList()
        {
            _array = Array.Empty<T>();
            Count = 0;
        }

        public MyList(int capacity)
        {
            _array = new T[capacity];
            Count = 0;
        }

        public MyList(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new Exception("The parameter collection cannot be null.");
            }

            int count = 0;
            foreach (var _ in source)
            {
                count++;
            }

            _array = new T[count];
            Count = 0;


            foreach (var item in source)
            {
                _array[Count++] = item;
            }
        }


        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public void Add(T item)
        {
            EnsureCapacity();
            _array[Count++] = item;
        }

        public void Clear()
        {
            Array.Clear(_array, 0, Count);
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, 0, array, arrayIndex, Count);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_equalityComparer.Equals(_array[i], item))
                    return i;

            }
            return -1;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0) return false;
            Array.Copy(_array, index + 1, _array, index, Count - index - 1);
            Count--;
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0) throw new Exception("The element index is out of range.");
            Array.Copy(_array, index + 1, _array, index, Count - index - 1);
            Count--;
        }

        public void Insert(int index, T item)
        {
            EnsureCapacity();
            Array.Copy(_array, index, _array, index + 1, Count - index);
            _array[index] = item;
            Count++;
        }

        private void EnsureCapacity()
        {
            if (Count < _array.Length) return;

            var newLength = Count == 0 ? 4 : Count * 2;
            var newArray = new T[newLength];

            Array.Copy(_array, newArray, Count);
            _array = newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}


