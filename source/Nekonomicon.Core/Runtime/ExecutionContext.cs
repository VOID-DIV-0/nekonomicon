namespace Nekonomicon.Core.Runtime;

using Nekonomicon.Core.Parser.Ast;

/// <summary>
/// Manages runtime state during script execution including variables and modules.
/// </summary>
public class ExecutionContext
{
    private readonly Dictionary<string, Variable> _variables = new();

    /// <summary>
    /// Declares a new variable.
    /// </summary>
    public Result<Unit> DeclareVariable(string name, VariableModifier modifier, object? initialValue = null)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Unit>.Failure("Variable name must not be empty");

        if (_variables.ContainsKey(name))
            return Result<Unit>.Failure($"Variable @{name} is already declared");

        var variable = new Variable(name, modifier);
        
        if (initialValue != null)
        {
            var result = variable.SetValue(initialValue);
            if (!result.IsSuccess)
                return result;
        }

        _variables[name] = variable;
        return Result<Unit>.Success(Unit.Default);
    }

    /// <summary>
    /// Gets a variable from the context.
    /// </summary>
    public Result<Variable> GetVariable(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Variable>.Failure("Variable name must not be empty");

        if (_variables.TryGetValue(name, out var variable))
            return Result<Variable>.Success(variable);

        return Result<Variable>.Failure($"Variable @{name} is not defined");
    }

    /// <summary>
    /// Sets a variable's value.
    /// </summary>
    public Result<Unit> SetVariable(string name, object? value)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Unit>.Failure("Variable name must not be empty");

        var varResult = GetVariable(name);
        if (!varResult.IsSuccess)
            return Result<Unit>.Failure(varResult.ErrorMessage);

        return varResult.Value!.SetValue(value);
    }

    /// <summary>
    /// Checks if a variable exists.
    /// </summary>
    public bool VariableExists(string name) => _variables.ContainsKey(name);
}
