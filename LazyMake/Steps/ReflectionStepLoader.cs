namespace LazyMake.Steps
{
    internal class ReflectionStepLoader
    {
        public IEnumerable<IStepDefinition> Load()
        {
            yield return new StepDefinition
            {
                Name = "state",
                Executor = new StateStepExecutor(),
            };
        }
    }
}
