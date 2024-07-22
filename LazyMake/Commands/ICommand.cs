using LazyMake.Language;

namespace LazyMake.Commands
{
    internal interface ICommand
    {
        void Execute(List<IParsedStep> resolvedSteps);
    }
}
