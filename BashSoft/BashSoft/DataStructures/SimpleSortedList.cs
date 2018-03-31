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

        public SimpleSortedList(IComparer<T> comparison, int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative");
            }

            this.comparison = comparison;
            this.innerCollection = new T[capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T element)
        {
            throw new NotImplementedException();
        }

        public void AddAll(ICollection<T> collection)
        {
            throw new NotImplementedException();
        }

        public int Size => this._size;

        public string JoinWith(string joiner)
        {
            throw new NotImplementedException();
        }
    }
}
