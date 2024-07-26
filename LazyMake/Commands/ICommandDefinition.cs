namespace LazyMake.Commands
{
    internal interface ICommandDefinition
    {
        ICommandExecutor Executor { get; init; }

        string Name { get; init; }
    }
}
