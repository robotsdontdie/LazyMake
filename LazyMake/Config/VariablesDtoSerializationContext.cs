using System.Text.Json.Serialization;

namespace LazyMake.Config
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(VariablesDto))]
    internal partial class VariablesDtoSerializationContext : JsonSerializerContext
    {
    }
}
