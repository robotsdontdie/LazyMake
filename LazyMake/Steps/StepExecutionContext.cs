using LazyMake.Config;
using Serilog;

namespace LazyMake.Steps
{
    internal class StepExecutionContext
    {
        public required ILogger Logger { get; init; }

        public required IVariableManager VariableManager { get; init;}
    }
}
