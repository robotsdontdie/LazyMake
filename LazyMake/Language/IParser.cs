namespace LazyMake.Language
{
    internal interface IParser
    {
        IParsedStep[] Parse(IEnumerable<Token> tokens);
    }
}
