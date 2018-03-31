using System.Runtime.CompilerServices;

namespace BashSoft.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using BashSoft.Contracts;

    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private T[] innerCollection;
        private int _size;
        private IComparer<T> comparison;
        private const int DefaultCapacity = 4;

        public SimpleSortedList(IComparer<T> comparison, int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative");
            }

            this.comparison = comparison;
            this.innerCollection = new T[capacity];
        }

        public SimpleSortedList(int capacity)
           : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), capacity)
        {
        }

        public SimpleSortedList(IComparer<T> comparison)
            : this(comparison, DefaultCapacity)
        {
        }


        public SimpleSortedList()
            : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), DefaultCapacity)
        {
        }

        public int Size => this._size;

        public void Add(T element)
        {
            throw new NotImplementedException();
        }

        public void AddAll(ICollection<T> collection)
        {
            throw new NotImplementedException();
        }

        public string JoinWith(string joiner)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
