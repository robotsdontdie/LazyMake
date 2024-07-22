namespace LazyMake.Config
{
    internal interface IVariableManager
    {
        IEnumerable<VariableEntry> Variables { get; }

        void Set(string name, string value);
    }
}
