namespace LazyMake.Config
{
    internal class VariablesDto
    {
        public Entry[] Entries { get; init; } = [];

        internal class Entry
        {
            public required string Name { get; init; }

            public string? Default { get; init; }

            public string? Value { get; set; }
        }
    }
}
