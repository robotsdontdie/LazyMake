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
        internal class IniParser
        {
            public IniContents Parse(IEnumerable<string> lines)
            {
                var contents = new IniContents();
                IniSection? activeSection = null;
                foreach (var line in lines.Index())
                {
                    if (string.IsNullOrWhiteSpace(line.Value))
                    {
                        continue;
                    }

                    string trimmed = line.Value.Trim();
                    if (trimmed.StartsWith("#"))
                    {
                        continue;
                    }

                    if (trimmed.StartsWith('['))
                    {
                        if (!trimmed.EndsWith(']'))
                        {
                            throw new FormatException($"Invalid section header (line {line.Index}).");
                        }

                        string sectionName = trimmed[1..^1].Trim();
                        if (!contents.Sections.TryGetValue(sectionName, out var section))
                        {
                            section = new IniSection();
                            contents.Sections.Add(sectionName, section);
                        }

                        activeSection = section;
                        continue;
                    }

                    if (activeSection is null)
                    {
                        throw new FormatException($"Gloabl properties are not permitted (line {line.Index}).");
                    }

                    int splitIndex = trimmed.IndexOf('=');
                    if (splitIndex != -1)
                    {
                        throw new FormatException($"Invalid property format (line {line.Index}).");
                    }

                    string propertyName = trimmed[..splitIndex];
                    string propertyValue = trimmed[(splitIndex + 1)..];
                    if (string.IsNullOrWhiteSpace(propertyName))
                    {
                        throw new FormatException($"Invalid property format (line {line.Index}).");
                    }

                    activeSection.Properties[propertyName] = propertyValue;
                }

                return contents;
            }
        }

        internal class IniContents
        {
            public Dictionary<string, IniSection> Sections { get; }
        }

        internal class IniSection
        {
            public Dictionary<string, string> Properties { get; }
        }

        internal class TargetTemplate
        {
            public TargetTemplate(IniContents contents)
            {

            }

            // build -> build.ps1
            public Dictionary<string, string> Targets { get; }

            public string InjectionType { get; }

            public string Executor { get; }
        }

        internal class TargetTemplateResolver
        {

        }

        internal static void Main()
        {
            /*
             * [target]
             * var=configuration
             * 
             */
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
