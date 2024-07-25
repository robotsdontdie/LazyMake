namespace LazyMake.Steps
{
    internal class StepDefinition
    {
        public required string Name { get; init; }

        public required IStepExecutor Executor { get; init; }
    }
}
