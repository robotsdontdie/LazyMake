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

    internal class StepMetadata
    {
        public required IStepLoadMetadata LoadMetadata { get; init; }
    }
}
