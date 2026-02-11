using Microsoft.Extensions.Hosting;
using System.CommandLine;

using Nekonomicon.Core.Models;
using Nekonomicon.Cli.Panics;
using Nekonomicon.Cli.Commands;

namespace Nekonomicon.Cli;

class Program
{
    static int Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
            })
            .Build();


        RootCommand rootCommand = new("Nekonomicon CLI - The neko interpreter")
        {
            new VersionCommand(),
            new CastCommand()
        };

        // Disable the built-in --version option (use 'neko version' instead)
        rootCommand.Options.RemoveAt(rootCommand.Options.Count - 1);

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }
}

