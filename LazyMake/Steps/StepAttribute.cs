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
