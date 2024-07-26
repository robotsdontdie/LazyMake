using LazyMake.Language;
using LazyMake.Steps;

namespace LazyMake.Commands
{
    internal class MakeCommand : ICommandExecutor
    {
        private readonly IStepProvider stepProvider;

        public MakeCommand(IStepProvider stepProvider)
        {
            this.stepProvider = stepProvider;
        }

        public void Execute(CommandExecutionContext context, List<IParsedStep> resolvedSteps)
        {
            foreach (var step in resolvedSteps)
            {
                switch (step)
                {
                    case ParsedNamedStep namedStep:
                        ExecuteNamedStep(context, namedStep);
                        break;
                    case ParsedSetVariableStep setVariableStep:
                        ExecuteSetVariable(context, setVariableStep);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ExecuteNamedStep(CommandExecutionContext context, ParsedNamedStep namedStep)
        {
            if (!stepProvider.TryGetStep(namedStep.Name, out var step))
            {
                throw new ExecutionException();
            }

            step.Executor.Execute(context.CreateStepExecutionContext());
        }

        private void ExecuteSetVariable(CommandExecutionContext context, ParsedSetVariableStep setVariableStep)
        {
            context.VariableManager.Set(setVariableStep.Name, setVariableStep.Value);
        }
    }
}
