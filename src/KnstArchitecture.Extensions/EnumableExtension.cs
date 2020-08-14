using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumableExtension
    {
        public static IEnumerable < (int Index, T Value) > IndexValue<T>(this IEnumerable<T> enumable)
        {
            var result = enumable.Select((Value, Index) => (Index, Value));
            return result;
        }

        public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> enumable, int elementsCount)
        {
            var result = enumable.OrderBy(arg => Guid.NewGuid()).Take(elementsCount);
            return result;
        }

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> enumable, int chunkSize)
        {
            var result = enumable.IndexValue<T>().GroupBy(x => x.Index / chunkSize).Select(x => x.Select(y => y.Value));
            return result;
        }

        public static bool IsSingle<T>(this IEnumerable<T> enumable)
        {
            return enumable.Count() == 1;
        }

        public static IEnumerable<T> Swap<T>(this IEnumerable<T> source, int firstIndex, int secondIndex)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            T[] array = source.ToArray();
            return Swap<T>(array, firstIndex, secondIndex);
        }

        private static IEnumerable<T> Swap<T>(T[] array, int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException("firstIndex");
            }
            if (secondIndex < 0 || secondIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException("secondIndex");
            }
            T tmp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = tmp;
            return array;
        }

        public static IEnumerable<T> Swap<T>(this IEnumerable<T> source, T firstItem, T secondItem)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            T[] array = source.ToArray();
            int firstIndex = Array.FindIndex(array, i => i.Equals(firstItem));
            int secondIndex = Array.FindIndex(array, i => i.Equals(secondItem));
            return Swap(array, firstIndex, secondIndex);
        }
    }
}