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
    }
}
