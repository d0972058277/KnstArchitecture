using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumableAsyncExtension
    {
        public static async Task<List<TSource>> ToListAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).ToList();
        }

        public static async Task<TSource[]> ToArrayAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).ToArray();
        }

        public static async Task<TSource> SingleAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).Single();
        }

        public static async Task<TSource> SingleOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).SingleOrDefault();
        }

        public static async Task<TSource> FirstAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).First();
        }

        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).FirstOrDefault();
        }

        public static async Task<TSource> LastAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).Last();
        }

        public static async Task<TSource> LastOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).LastOrDefault();
        }

        public static async Task<bool> AllAsync<TSource>(this Task<IEnumerable<TSource>> source, Func<TSource, bool> predicate)
        {
            return (await source).All(predicate);
        }
        public static async Task<bool> AnyAsync<TSource>(this Task<IEnumerable<TSource>> source)
        {
            return (await source).Any();
        }
        public static async Task<bool> AnyAsync<TSource>(this Task<IEnumerable<TSource>> source, Func<TSource, bool> predicate)
        {
            return (await source).Any(predicate);
        }
    }
}