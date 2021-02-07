using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructs.Structs
{
    [Serializable]
    public class ArrayList<T> : IList<T>
    {
        private const int DefaultCapacity = 4;
        private const int MaxListSize = int.MaxValue;

        private T[] _items;
        private int _size;
#pragma warning disable CA1825
        private static readonly T[] s_emptyArray = new T[0];
#pragma warning restore CA1825

        public ArrayList()
        {
            _items = s_emptyArray;
        }

        public ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(capacity));
            }

            if (capacity == 0)
            {
                _items = s_emptyArray;
            }
            else
            {
                _items = new T[capacity];
            }
        }

        public ArrayList(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection is ICollection<T> c)
            {
                int collectionSize = c.Count;
                
                if (collectionSize == 0)
                {
                    _items = s_emptyArray;
                }
                else
                {
                    _items = new T[collectionSize];
                    c.CopyTo(_items, 0);
                    _size = collectionSize;
                }
            }
            else
            {
                _items = s_emptyArray;

                using IEnumerator<T> en = collection!.GetEnumerator();
                while (en.MoveNext())
                {
                    Add(en.Current);
                }
            }
        }

        public int Count => _size;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public int Capacity
        {
            get => _items.Length;
            set
            {
                if (value < _size)
                {
                    throw new ArgumentException("Capacity is to small.");
                }

                if (value != _items.Length)
                {
                    if (value != 0)
                    {
                        T[] tempArr = new T[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, tempArr, _size);
                        }
                        _items = tempArr;
                    }
                    else
                    {
                        _items = s_emptyArray;
                    }
                }
            }
        }

        public T this[int index] 
        {
            get
            {
                if ((uint)index > (uint)_size)
                {
                    throw new IndexOutOfRangeException("Index out of a range.");
                }

                return _items[index];
            }
            set
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new IndexOutOfRangeException("Index out of range.");
                }

                _items[index] = value;
            }
        }

        public int IndexOf(T item)
            => Array.IndexOf(_items, item, 0, _size);

        public void Insert(int index, T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if ((uint)index > (uint)_size)
            {
                throw new IndexOutOfRangeException("Index out of a range.");
            }

            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }

            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = item;
            _size++;
        }

        public void RemoveAt(int index)
        {
            if ((uint)index > (uint)_size)
                throw new IndexOutOfRangeException("Index out of a range.");

            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
        }

        public void Add(T item)
        {
            if ((uint)_size < (uint)_items.Length)
            {
                _items[_size] = item;
                _size++;
            }
            else
            {
                AddWithResize(item);
            }
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
            }

            _size = 0;
        }

        public bool Contains(T item)
        {
            return _size != 0 && IndexOf(item) != -1;
        }

        public void CopyTo(T[] array)
            => CopyTo(array, 0);

        public void CopyTo(T[] array, int arrayIndex)
        {
            if ((array != null) && (array.Rank != 1))
            {
                throw new ArgumentException("Multiple dimensional arrays aren't supported");
            }
            
            try
            {
                Array.Copy(_items, 0, array!, arrayIndex, _size);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException("Invalid array type.");
            }
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
            => new Enumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator<T>(this); 

        private void AddWithResize(T item)
        {
            EnsureCapacity(_size + 1);
            _items[_size] = item;
            _size++;
        }

        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
                
                if ((uint)newCapacity > MaxListSize)
                {
                    newCapacity = MaxListSize;
                }
                Capacity = newCapacity;
            }
        }

        private void Reset()
        {
            _items = s_emptyArray;
        }

        internal struct Enumerator<T> : IEnumerator<T>, IEnumerator
        {
            private readonly ArrayList<T> _list;
            private int _index;
#nullable enable
            private T? _current;
#nullable disable

            public Enumerator(ArrayList<T> list)
            {
                _list = list;
                _index = 0;
                _current = default;
            }

            public object Current => _current;
            T IEnumerator<T>.Current => _current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                ArrayList<T> localList = _list;

                if ((uint)_index < (uint)localList.Count)
                {
                    _current = localList[_index];
                    _index++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _index = 0;
                _current = default;
            }
        }
    }
}
