using LazyMake.Config;
using LazyMake.Logging;

namespace LazyMake.Steps
{
    internal class StepExecutionContext
    {
        public required ILogger Logger { get; init; }

        public required IVariableManager VariableManager { get; init; }
    }
}
