using LazyMake.Config;
using LazyMake.Language;
using LazyMake.Steps;

namespace LazyMake.Commands
{

    internal class MakeCommand : ICommand
    {
        private readonly IVariableManager variableManager;
        private readonly IStepProvider stepProvider;

        public MakeCommand(IVariableManager variableManager, IStepProvider stepProvider)
        {
            this.variableManager = variableManager;
            this.stepProvider = stepProvider;
        }

        public void Execute(List<IParsedStep> resolvedSteps)
        {
            foreach (var step in resolvedSteps)
            {
                switch (step)
                {
                    case ParsedSetVariableStep setVariableStep:
                        ExecuteSetVariable(setVariableStep);
                        break;
                    case ParsedNamedStep namedStep:
                        ExecuteNamedStep(namedStep);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ExecuteNamedStep(ParsedNamedStep namedStep)
        {
            if (!stepProvider.TryGetStep(namedStep.Name, out var step))
            {
                throw new ExecutionException();
            }

            step.Execute();
        }

        private void ExecuteSetVariable(ParsedSetVariableStep setVariableStep)
        {
            variableManager.Set(setVariableStep.Name, setVariableStep.Value);
        }
    }
}
