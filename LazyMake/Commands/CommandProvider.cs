using Autofac.Features.Indexed;
using Autofac.Features.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Commands
{
    internal class CommandProvider : ICommandProvider
    {
        private readonly IIndex<string, Meta<ICommand, CommandMetadata>> commandIndex;

        public CommandProvider(IIndex<string, Meta<ICommand, CommandMetadata>> commandIndex)
        {
            this.commandIndex = commandIndex;
        }

        public ICommand MakeCommand => commandIndex["make"].Value;

        public bool TryGetCommand(string commandName, [NotNullWhen(true)] out ICommand? command)
        {
            if (!commandIndex.TryGetValue(commandName, out var meta))
            {
                command = null;
                return false;
            }

            command = meta.Value;
            return true;
        }
    }
}
