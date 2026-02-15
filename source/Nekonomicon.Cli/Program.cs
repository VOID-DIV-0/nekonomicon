using Microsoft.Extensions.Hosting;
using System.CommandLine;

using Nekonomicon.Cli.Commands;

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

try
{
    return await parseResult.InvokeAsync();
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}