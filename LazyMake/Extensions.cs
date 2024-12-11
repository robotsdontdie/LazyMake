using System.Runtime.CompilerServices;

namespace LazyMake
{
    internal static class Extensions
    {
        public static T CheckNotNull<T>(
            this T? param,
            [CallerArgumentExpression(nameof(param))] string? paramExpression = null)
            where T : class
        {
            return param ?? throw new ArgumentNullException(paramExpression);
        }

        public static IEnumerable<(int Index, T Value)> Index<T>(this IEnumerable<T> enumerable)
        {
            int index = 0;
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return (index++, enumerator.Current);
            }
        }
    }
}
