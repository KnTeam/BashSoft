namespace BashSoftTesting.DataStructures
{
    using BashSoft.DataStructures;
    using NUnit.Framework;
    using System;
    using System.Collections;

    [TestFixture]
    public class SimpleSortedListTests
    {
        [Test]
        public void Constructor_WithoutItems_SizeIsCorrect()
        {
            var list = new SimpleSortedList<int>();
            Assert.That(list.Size, Is.EqualTo(0));
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        public void Constructor_WithoutItems_CapacityIsCorrect(int capacity)
        {
            var list = new SimpleSortedList<int>(capacity);
            Assert.That(list.Capacity, Is.EqualTo(capacity));
        }

        [Test]
        public void Size_WithItems_IsCorrect()
        {
            int[] intCollection = new int[] { 1, 2, 3, 4, 5, 8, 7, 6 };
            var list = new SimpleSortedList<int>();
            list.AddAll(intCollection);
            Assert.That(list.Size, Is.EqualTo(intCollection.Length));
        }

        [Test]
        public void Size_WithoutItems_IsCorrect()
        {
            var list = new SimpleSortedList<int>();
            Assert.That(list.Size, Is.EqualTo(0));
        }

        [Test]
        public void Add_WithItems_IsCorrect()
        {
            int[] intCollection = new int[] { 1, 2, 3, 4, 5, 8, 7, 6 };
            var list = new SimpleSortedList<int>();
            foreach (var number in intCollection)
            {
                list.Add(number);
            }

            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            Assert.That(list.Size, Is.EqualTo(intCollection.Length));
        }

        [Test]
        public void Add_WithItemsInReverseOrder_IsCorrect()
        {
            int[] intCollection = new int[] { 5, 4, 3, 2, 1 };
            var list = new SimpleSortedList<int>();
            foreach (var number in intCollection)
            {
                list.Add(number);
            }

            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            Assert.That(list.Size, Is.EqualTo(intCollection.Length));
        }

        [Test]
        public void AddAll_WithItems_IsCorrect()
        {
            int[] intCollection = new int[] { 1, 2, 3, 4, 5 };
            var list = new SimpleSortedList<int>();
            list.AddAll(intCollection);
            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            Assert.That(list.Size, Is.EqualTo(intCollection.Length));
        }

        [Test]
        public void AddAll_WithItemsInReverseOrder_IsCorrect()
        {
            int[] intCollection = new int[] { 5, 4, 3, 2, 1 };
            var list = new SimpleSortedList<int>();
            list.AddAll(intCollection);
            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            Assert.That(list.Size, Is.EqualTo(intCollection.Length));
        }

        [Test]
        public void JoinWith_WithItems_IsCorrect()
        {
            string joiner = ", ";
            int[] intCollection = new int[] { 1, 2, 3, 4, 5 };
            var list = new SimpleSortedList<int>();
            list.AddAll(intCollection);
            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            Assert.That(list.JoinWith(joiner), Is.EqualTo(string.Join(joiner, intCollection)));
        }

        [Test]
        public void JoinWith_WithoutItems_IsCorrect()
        {
            string joiner = ", ";
            int[] intCollection = new int[0];
            var list = new SimpleSortedList<int>();
            list.AddAll(intCollection);
            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            Assert.That(list.JoinWith(joiner), Is.EqualTo(string.Join(joiner, intCollection)));
        }

        [Test]
        public void Remove_WithItems_IsCorrect()
        {
            int[] intCollection = new int[] { 1, 2, 3, 4, 5 };
            var list = new SimpleSortedList<int>();
            list.AddAll(intCollection);
            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            foreach (var number in intCollection)
            {
                Assert.That(list.Remove(number), Is.EqualTo(true));
            }
        }

        [Test]
        public void Remove_WithItemsThatAreNotInCollection_IsCorrect()
        {
            int[] intCollection = new int[] { 1, 2, 3, 4, 5 };
            int[] intsNotInCollection = new int[] { -1, 0, 12, 17 };
            var list = new SimpleSortedList<int>();
            list.AddAll(intCollection);
            Array.Sort(intCollection);
            int index = 0;
            foreach (var item in list)
            {
                Assert.That(item, Is.EqualTo(intCollection[index]));
                index++;
            }

            foreach (var number in intsNotInCollection)
            {
                Assert.That(list.Remove(number), Is.EqualTo(false));
            }
        }

        [Test]
        public void Remove_WithoutItems_IsCorrect()
        {
            int[] intCollection = new int[0];
            var list = new SimpleSortedList<int>();

            Assert.That(list.Remove(5), Is.EqualTo(false));
        }
    }
}
