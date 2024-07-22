namespace LazyMake.Execution
{
    internal interface IExecutionPipeline
    {
        void Execute(string line);
    }
}
