namespace LazyMake.Steps
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class StepAttribute : Attribute
    {
        public StepAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
