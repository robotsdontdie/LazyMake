using LazyMake.Commands;
using LazyMake.Config;
using LazyMake.Language;
using System.Diagnostics.CodeAnalysis;

namespace LazyMake.Execution
{
    internal class ExecutionPipeline : IExecutionPipeline
    {
        private readonly ILexer lexer;
        private readonly IParser parser;
        private readonly IAliasResolver aliasResolver;
        private readonly ICommandProvider commandProvider;

        public ExecutionPipeline(
            ILexer lexer,
            IParser parser,
            IAliasResolver aliasResolver,
            ICommandProvider commandProvider)
        {
            this.lexer = lexer;
            this.parser = parser;
            this.aliasResolver = aliasResolver;
            this.commandProvider = commandProvider;
        }

        public void Execute(string line)
        {
            var tokens = lexer.Tokenize(line);
            var steps = parser.Parse(tokens);

            var resolvedSteps = new List<IParsedStep>();
            foreach (var step in steps)
            {
                if (TryResolveAlias(step, out var resolved))
                {
                    resolvedSteps.AddRange(resolved);
                }
                else
                {
                    resolvedSteps.Add(step);
                }
            }

            if (resolvedSteps.Count == 0)
            {
                throw new NotImplementedException();
            }

            var firstStep = resolvedSteps[0];
            if (firstStep is ParsedNamedStep namedStep
                && commandProvider.TryGetCommand(namedStep.Name, out var command))
            {
                command.Execute(resolvedSteps);
            }
            else
            {
                commandProvider.MakeCommand.Execute(resolvedSteps);
            }
        }

        private bool TryResolveAlias(IParsedStep step, [NotNullWhen(true)] out IEnumerable<IParsedStep>? resolvedSteps)
        {
            if (step is not ParsedNamedStep namedStep)
            {
                resolvedSteps = null;
                return false;
            }

            if (!aliasResolver.TryResolve(namedStep.Name, out var resolved))
            {
                resolvedSteps = null;
                return false;
            }

            resolvedSteps = parser.Parse(lexer.Tokenize(resolved.Value));
            return true;
        }
    }
}
