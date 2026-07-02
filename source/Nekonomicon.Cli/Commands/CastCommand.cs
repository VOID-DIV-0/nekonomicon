using System.CommandLine;
using Nekonomicon.Core.Runtime;

namespace Nekonomicon.Cli.Commands;

public class CastCommand : Command
{
    private readonly ScriptEngine _scriptEngine;

    public CastCommand(ScriptEngine scriptEngine) : base("cast", "Execute a nekonomicon script (.spell)")
    {
        _scriptEngine = scriptEngine;

        var fileArgument = new Argument<string>("file")
        {
            Arity = ArgumentArity.ExactlyOne,
            Description = "Path to the .spell file to cast"
        };



        Add(fileArgument);

        SetAction((parseResult) =>
        {
            var spellPath = parseResult.GetRequiredValue(fileArgument);
            if (!File.Exists(spellPath))
            {
                Console.Error.WriteLine($"Error: File '{spellPath}' does not exist.");
                return 1;
            }

            _scriptEngine.ExecuteScript(spellPath);
            return 0;
        });
    }
}

