using Autofac.Features.Indexed;
using Autofac.Features.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Steps
{
    internal class StepProvider : IStepProvider
    {
        private readonly IIndex<string, Meta<IStepExecutor, StepMetadata>> stepIndex;

        public StepProvider(IIndex<string, Meta<IStepExecutor, StepMetadata>> stepIndex)
        {
            this.stepIndex = stepIndex;
        }

        public bool TryGetStep(string stepName, [NotNullWhen(true)] out IStepExecutor? stepExecutor)
        {
            if (!stepIndex.TryGetValue(stepName, out var meta))
            {
                stepExecutor = null;
                return false;
            }

            stepExecutor = meta.Value;
            return true;
        }
    }
}
