using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Config
{
    internal class AliasResolver : IAliasResolver
    {
        private readonly Dictionary<string, StepAlias> aliases;

        public AliasResolver()
        {
            aliases = new Dictionary<string, StepAlias>
            {
                ["ship"] = new StepAlias { Name = "ship", Value = "--configuration=Shipping" },
            };
        }

        public bool TryResolve(string name, [NotNullWhen(true)] out StepAlias? stepAlias)
        {
            return aliases.TryGetValue(name, out stepAlias);
        }
    }
}
