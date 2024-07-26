using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Steps
{
    internal class StepProvider : IStepProvider
    {
        private readonly Dictionary<string, IStepDefinition> steps;

        public StepProvider(IEnumerable<IStepDefinition> steps)
        {
            this.steps = steps.ToDictionary(step => step.Name);
        }

        public bool TryGetStep(string stepName, [NotNullWhen(true)] out IStepDefinition? step)
        {
            return steps.TryGetValue(stepName, out step);
        }
    }
}
