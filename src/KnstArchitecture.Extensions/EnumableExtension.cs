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
    }
}