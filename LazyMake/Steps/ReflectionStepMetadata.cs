namespace LazyMake.Steps
{
    internal class ReflectionStepMetadata : IStepLoadMetadata
    {
        private readonly StepAttribute attribute;
        private readonly Type type;

        public ReflectionStepMetadata(StepAttribute attribute, Type type)
        {
            this.attribute = attribute;
            this.type = type;
        }

        public string Name => attribute.Name;
    }
}
