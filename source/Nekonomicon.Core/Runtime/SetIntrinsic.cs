using Nekonomicon.Core.Parser.Ast;

namespace Nekonomicon.Core.Runtime;

/// <summary>
/// Implements the 'set' intrinsic: set @variable to <value>.
/// </summary>
public class SetIntrinsic : IIntrinsic
{
    public Result<Unit> Execute(List<object?> arguments, ExecutionContext context)
    {
        if (arguments.Count < 3)
        {
            return Result<Unit>.Failure("set requires 3 arguments: variable name, modifier, and value");
        }

        // First argument should be variable name
        var varName = arguments[0] as string;
        if (string.IsNullOrEmpty(varName))
        {
            return Result<Unit>.Failure("Invalid variable name");
        }

        // Second argument should be variable modifier
        if (!(arguments[1] is VariableModifier modifier))
        {
            return Result<Unit>.Failure("Invalid variable modifier");
        }

        var value = arguments[2];

        // Check if variable exists
        var existing = context.GetVariable(varName);
        if (existing.IsSuccess)
        {
            // Variable exists, update it (this will check constant restriction)
            return context.SetVariable(varName, value);
        }

        // Variable doesn't exist, declare it with the correct modifier
        var declareResult = context.DeclareVariable(varName, modifier, value);
        return declareResult.IsSuccess ? Result<Unit>.Success(Unit.Default) : declareResult;
    }
}
