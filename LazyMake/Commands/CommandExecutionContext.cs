using LazyMake.Config;
using LazyMake.Steps;
using Serilog;

namespace LazyMake.Commands
{
    internal class CommandExecutionContext
    {
        public required ILogger Logger { get; init; }

        public required IVariableManager VariableManager { get; init; }

        public StepExecutionContext CreateStepExecutionContext()
        {
            return new StepExecutionContext
            {
                Logger = Logger,
                VariableManager = VariableManager,
            };
        }
    }
}
