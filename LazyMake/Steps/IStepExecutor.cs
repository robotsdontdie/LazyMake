namespace LazyMake.Steps
{
    internal interface IStepExecutor
    {
        void Execute(StepExecutionContext context);
    }
}
