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
