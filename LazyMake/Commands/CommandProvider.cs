using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Commands
{
    internal class CommandProvider : ICommandProvider
    {
        private readonly Dictionary<string, ICommandDefinition> commands;

        public CommandProvider(IEnumerable<ICommandDefinition> commands)
        {
            this.commands = commands.ToDictionary(command => command.Name);
        }

        public ICommandDefinition MakeCommand => commands["make"];

        public bool TryGetCommand(string commandName, [NotNullWhen(true)] out ICommandDefinition? command)
        {
            return commands.TryGetValue(commandName, out command);
        }
    }
}
