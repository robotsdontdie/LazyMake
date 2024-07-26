namespace LazyMake.Steps
{
    internal class StateStepExecutor : IStepExecutor
    {
        public void Execute(StepExecutionContext context)
        {
            foreach (var variable in context.VariableManager.Variables)
            {
                context.Logger.Information($"{variable.Name} -> {variable.Value}");
            }
        }
    }
}
