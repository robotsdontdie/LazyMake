using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Commands
{
    internal interface ICommandProvider
    {
        ICommandDefinition MakeCommand { get; }

        bool TryGetCommand(string commandName, [NotNullWhen(true)] out ICommandDefinition? command);
    }
}
