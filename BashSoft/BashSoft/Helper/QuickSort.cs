namespace BashSoft.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class QuickSort
    {
        public static void Sort<T>(T[] arr, IComparer<T> comparator) where T : IComparable<T>
        {
            Shuffle(arr);
            Sort(arr, 0, arr.Length - 1, comparator);
        }

        private static void Sort<T>(T[] arr, int lo, int hi, IComparer<T> comparator) where T : IComparable<T>
        {
            if (lo >= hi)
            {
                return;
            }

            int pivot = Partition(arr, lo, hi, comparator);
            Sort(arr, lo, pivot - 1, comparator);
            Sort(arr, pivot + 1, hi, comparator);
        }

        private static int Partition<T>(T[] arr, int lo, int hi, IComparer<T> comparator) where T : IComparable<T>
        {
            if (lo >= hi)
            {
                return lo;
            }

            int i = lo;
            int j = hi + 1;

            while (true)
            {
                while (Less(arr[++i], arr[lo], comparator))
                {
                    if (i == hi)
                    {
                        break;
                    }
                }

                while (Less(arr[lo], arr[--j], comparator))
                {
                    if (j == lo)
                    {
                        break;
                    }
                }

                if (i >= j)
                {
                    break;
                }

                Swap(arr, i, j);
            }

            Swap(arr, lo, j);
            return j;
        }

        private static void Swap<T>(T[] arr, int first, int second)
        {
            var temp = arr[first];
            arr[first] = arr[second];
            arr[second] = temp;
        }

        private static bool Less<T>(T current, T other, IComparer<T> comparator) where T : IComparable<T>
        {
            return comparator.Compare(current, other) < 0;
        }

        private static void Shuffle<T>(T[] source)
        {
            Random rnd = new Random();

            for (int i = 0; i < source.Length; i++)
            {
                // Exchange array[i] with random element in array[i … n-1]

                int r = i + rnd.Next(0, source.Length - i);

                T temp = source[i];
                source[i] = source[r];
                source[r] = temp;
            }
        }
    }
}
