namespace LazyMake.Steps
{
    internal interface IStepDefinition
    {
        IStepExecutor Executor { get; init; }

        string Name { get; init; }
    }
}
