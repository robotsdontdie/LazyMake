namespace LazyMake.Logging
{
    internal class Logger : ILogger
    {
        public void Info(string line)
        {
            Console.Out.WriteLine(line);
        }
    }
}
