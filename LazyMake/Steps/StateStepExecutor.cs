using LazyMake.Config;
using Serilog;

namespace LazyMake.Steps
{
    [Step("state")]
    internal class StateStepExecutor : IStepExecutor
    {
        private readonly ILogger logger;
        private readonly IVariableManager variableManager;

        public StateStepExecutor(ILogger logger, IVariableManager variableManager)
        {
            this.logger = logger;
            this.variableManager = variableManager;
        }

        public void Execute()
        {
            foreach (var variable in variableManager.Variables)
            {
                logger.Information($"{variable.Name} -> {variable.Value}");
            }
        }
    }
}
