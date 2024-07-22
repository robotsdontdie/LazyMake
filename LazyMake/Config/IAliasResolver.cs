using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Config
{
    internal interface IAliasResolver
    {
        bool TryResolve(string name, [NotNullWhen(true)] out StepAlias? stepAlias);
    }
}
