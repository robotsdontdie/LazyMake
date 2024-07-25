using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Commands
{
    internal interface ICommandProvider
    {
        ICommand MakeCommand { get; }

        bool TryGetCommand(string stepName, [NotNullWhen(true)] out ICommand? command);
    }
}
