using LazyMake.Commands;
using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Language
{

    public class ParsedNamedStep : IParsedStep
    {
        public required string Name { get; init; }
    }
}
