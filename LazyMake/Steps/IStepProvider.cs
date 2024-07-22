using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Steps
{
    internal interface IStepProvider
    {
        bool TryGetStep(string stepName, [NotNullWhen(true)] out IStepExecutor? stepExecutor);
    }
}
