using Autofac;
using Autofac.Features.Indexed;
using Autofac.Features.Metadata;
using LazyMake.Commands;
using LazyMake.Execution;
using LazyMake.Language;
using Serilog;
using System.Diagnostics.CodeAnalysis;
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
