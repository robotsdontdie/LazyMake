namespace LazyMake.Steps
{
    internal class StepDefinition : IStepDefinition
    {
        public required string Name { get; init; }

        public required IStepExecutor Executor { get; init; }
    }
}
