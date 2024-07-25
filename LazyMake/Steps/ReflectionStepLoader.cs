using Autofac;
using System.Reflection;

namespace LazyMake.Steps
{
    internal class ReflectionStepLoader
    {
        public void Load(ContainerBuilder builder)
        {
            foreach (var type in Assembly.GetExecutingAssembly().DefinedTypes)
            {
                if (!type.ImplementedInterfaces.Contains(typeof(IStepExecutor)))
                {
                    continue;
                }

                var attribute = type.GetCustomAttribute<StepAttribute>();
                if (attribute is null)
                {
                    continue;
                }

                var metadata = new ReflectionStepMetadata(attribute, type);
                builder.RegisterType(type)
                    .Keyed<IStepExecutor>(attribute.Name)
                    .WithMetadata<StepMetadata>(mc => mc.For(sm => sm.LoadMetadata, metadata));
            }
        }
    }
}
