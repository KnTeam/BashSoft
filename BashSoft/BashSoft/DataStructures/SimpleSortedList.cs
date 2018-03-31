using System.Runtime.CompilerServices;
using System.Text;

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
            if (this.innerCollection.Length == this.Size)
            {
                this.Resize();
            }

            this.innerCollection[this._size] = element;
            this._size++;
            Array.Sort(this.innerCollection, 0, this._size, this.comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if (this.Size + collection.Count >= this.innerCollection.Length)
            {
                this.MultiResize(collection);
            }

            foreach (var element in collection)
            {
                this.innerCollection[this._size] = element;
                this._size++;
            }

            Array.Sort(this.innerCollection, 0, this._size, this.comparison);
        }

        public string JoinWith(string joiner)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var element in this)
            {
                builder.Append(element);
                builder.Append(joiner);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.innerCollection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];
            Array.Copy(innerCollection, newCollection, this.Size);
            innerCollection = newCollection;
        }

        private void MultiResize(ICollection<T> collection)
        {
            int newSize = this.innerCollection.Length * 2;
            while (this._size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(innerCollection, newCollection, this._size);
            innerCollection = newCollection;
        }
    }
}
