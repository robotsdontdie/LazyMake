using System.Text.Json;

namespace LazyMake.Config
{
    internal class VariableManager : IVariableManager
    {
        private readonly Dictionary<string, VariableEntry> entries;
        private readonly string configPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "LazyMake",
                ".lmconfig");

        public VariableManager()
        {
            string? configDir = Path.GetDirectoryName(configPath);
            if (!string.IsNullOrWhiteSpace(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                var dto = JsonSerializer.Deserialize(json, VariablesDtoSerializationContext.Default.VariablesDto)
                    ?? new VariablesDto();

                entries = dto.Entries.Select(e => new VariableEntry
                {
                    Name = e.Name,
                    Default = e.Default,
                    Value = e.Value,
                }).ToDictionary(e => e.Name);
            }
            else
            {
                entries = new Dictionary<string, VariableEntry>();
            }
        }

        public IEnumerable<VariableEntry> Variables => entries.Values;

        public void Set(string name, string value)
        {
            if (entries.TryGetValue(name, out var entry))
            {
                entry.Value = value;
            }
            else
            {
                entries[name] = new VariableEntry
                {
                    Name = name,
                    Value = value,
                };
            }

            Save();
        }

        private void Save()
        {
            var dto = new VariablesDto
            {
                Entries = entries.Values.Select(e => new VariablesDto.Entry
                {
                    Name = e.Name,
                    Default = e.Default,
                    Value = e.Value,
                }).ToArray(),
            };
            string json = JsonSerializer.Serialize(dto, VariablesDtoSerializationContext.Default.VariablesDto);
            File.WriteAllText(configPath, json);
        }
    }
}
