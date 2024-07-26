using LazyMake.Commands;
using LazyMake.Config;
using LazyMake.Execution;
using LazyMake.Language;
using LazyMake.Logging;
using LazyMake.Steps;

namespace LazyMake
{
    internal class Program
    {
        internal static void Main()
        {
            var logger = new Logger();

            var steps = new List<IStepDefinition>();
            steps.AddRange(new ReflectionStepLoader().Load());
            var stepProvider = new StepProvider(steps);

            var commandProvider = new CommandProvider(new[]
            {
                new CommandDefinition
                {
                    Name = "make",
                    Executor = new MakeCommand(stepProvider),
                },
            });

            var aliasResolver = new AliasResolver();
            var lexer = new Lexer();
            var parser = new Parser();
            var variableManager = new VariableManager();
            var pipeline = new ExecutionPipeline(
                logger,
                lexer,
                parser,
                aliasResolver,
                commandProvider,
                variableManager);

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
