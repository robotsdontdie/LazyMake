using Autofac;
using LazyMake.Commands;
using LazyMake.Config;
using LazyMake.Execution;
using LazyMake.Language;
using LazyMake.Steps;
using Serilog;

namespace LazyMake
{
    internal class Program
    {
        internal static void Main()
        {
            // load configuration
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var builder = new ContainerBuilder();
            var steps = new List<IStepDefinition>();
            steps.AddRange(new ReflectionStepLoader().Load());

            var stepProvider = new StepProvider(steps);

            builder.RegisterType<CommandProvider>().As<ICommandProvider>();
            builder.RegisterInstance(Log.Logger).As<ILogger>();
            builder.RegisterType<AliasResolver>().As<IAliasResolver>();
            builder.RegisterType<ExecutionPipeline>().As<IExecutionPipeline>();
            builder.RegisterType<Lexer>().As<ILexer>();
            builder.RegisterType<Parser>().As<IParser>();
            builder.RegisterType<MakeCommand>().Keyed<ICommand>("make");

            var variableManager = new VariableManager();

            builder.RegisterInstance(variableManager).As<IVariableManager>();

            var container = builder.Build();

            var pipeline = container.Resolve<IExecutionPipeline>();
            while (true)
            {
                Console.Write(">");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }

                pipeline.Execute(line);
            }
        }
    }
}
