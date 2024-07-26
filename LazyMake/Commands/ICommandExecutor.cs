using LazyMake.Language;

namespace LazyMake.Commands
{
    internal interface ICommandExecutor
    {
        void Execute(CommandExecutionContext context, List<IParsedStep> resolvedSteps);
    }

    internal class CommandDefinition : ICommandDefinition
    {
        public required string Name { get; init; }

        public required ICommandExecutor Executor { get; init; }
    }
}
