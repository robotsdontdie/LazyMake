using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Commands
{
    internal interface ICommandProvider
    {
        bool TryGetCommand(string stepName, [NotNullWhen(true)] out ICommand? command);

        ICommand MakeCommand { get; }
    }
}
