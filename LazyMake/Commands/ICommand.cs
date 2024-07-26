using LazyMake.Language;

namespace LazyMake.Commands
{
    internal interface ICommand
    {
        void Execute(CommandExecutionContext context, List<IParsedStep> resolvedSteps);
    }
}
