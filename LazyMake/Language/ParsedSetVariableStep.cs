using LazyMake.Commands;
using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Language
{

    public class ParsedSetVariableStep : IParsedStep
    {
        public required string Name { get; init; }

        public required string Value { get; init; }
    }
}
