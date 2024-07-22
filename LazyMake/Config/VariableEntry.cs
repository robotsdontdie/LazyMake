namespace LazyMake.Config
{
    internal class VariableEntry
    {
        public required string Name { get; init; }

        public string? Default { get; init; }

        public string? Value { get; set; }
    }
}
