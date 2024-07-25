namespace LazyMake.Language
{
    internal class ParsedSetVariableStep : IParsedStep
    {
        public required string Name { get; init; }

        public required string Value { get; init; }
    }
}
