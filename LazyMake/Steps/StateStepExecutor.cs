using Autofac;
using Autofac.Features.Indexed;
using Autofac.Features.Metadata;
using LazyMake.Commands;
using LazyMake.Config;
using LazyMake.Execution;
using LazyMake.Language;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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
