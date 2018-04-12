namespace BashSoft.DataStructures
{
    using System.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using BashSoft.Contracts;
    using BashSoft.Helper;

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

        public int Capacity => this.innerCollection.Length;

        /// <summary>
        /// Adds a single element to the collection.
        /// </summary>
        /// <param name="element">Element to add.</param>
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

        /// <summary>
        /// Adds a collection of elements to this collection.
        /// </summary>
        /// <param name="collection">Collection of elements to add.</param>
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

            QuickSort.Sort(this.innerCollection, this.Size, this.comparison);
        }

        /// <summary>
        /// Joins the collection with a specific joiner string (something like string.Join()).
        /// </summary>
        /// <param name="joiner">String that will be the separater.</param>
        /// <returns>Returns a string representing the collection separated with the given symbol.</returns>
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

        /// <summary>
        /// Resizes the collection's size by 2.
        /// </summary>
        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];
            Array.Copy(innerCollection, newCollection, this.Size);
            innerCollection = newCollection;
        }

        /// <summary>
        /// Resizes the collection's size as much as needed for the given collection.
        /// </summary>
        /// <param name="collection">Collection that needs to be imported.</param>
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

        /// <summary>
        /// Remove method that receives a <T> element and returns true or false - whether it has been removed or not.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        /// <returns>Returns true if the element is removed.</returns>
        public bool Remove(T element)
        {
            bool hasBeenRemoved = false;
            int indexOfRemovedElement = 0;
            for (int i = 0; i < this.Size; i++)
            {
                if (this.innerCollection[i].Equals(element))
                {
                    indexOfRemovedElement = i;
                    this.innerCollection[i] = default(T);
                    hasBeenRemoved = true;
                    break;
                }
            }

            if (hasBeenRemoved)
            {
                for (int i = indexOfRemovedElement; i < this.Size - 1; i++)
                {
                    this.innerCollection[i] = this.innerCollection[i + 1];
                }

                this.innerCollection[this.Size - 1] = default(T);
            }

            return hasBeenRemoved;
        }
    }
}
