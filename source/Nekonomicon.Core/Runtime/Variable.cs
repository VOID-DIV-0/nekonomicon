namespace Nekonomicon.Core.Runtime;

using Nekonomicon.Core.Parser.Ast;

/// <summary>
/// Represents a runtime variable instance.
/// </summary>
public class Variable
{
    /// <summary>
    /// Variable name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Type modifier (Mutable, Constant, Nullable).
    /// </summary>
    public VariableModifier Modifier { get; }

    /// <summary>
    /// Current value of the variable.
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Runtime type of the variable (inferred from first assignment).
    /// </summary>
    public Type? ValueType { get; set; }

    public Variable(string name, VariableModifier modifier)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name must not be empty", nameof(name));

        Name = name;
        Modifier = modifier;
        Value = null;
        ValueType = null;
    }

    /// <summary>
    /// Sets the variable value. Respects constant/nullable restrictions.
    /// </summary>
    public Result<Unit> SetValue(object? newValue)
    {
        // Constants cannot be reassigned after initialization
        if (Modifier == VariableModifier.Constant && ValueType != null)
        {
            return Result<Unit>.Failure($"Constant variable @!{Name} cannot be reassigned");
        }

        // Non-nullable variables cannot be null
        if (Modifier != VariableModifier.Nullable && newValue == null)
        {
            return Result<Unit>.Failure($"Variable @{Name} cannot be null");
        }

        // Track type from first assignment
        if (ValueType == null && newValue != null)
        {
            ValueType = newValue.GetType();
        }

        Value = newValue;
        return Result<Unit>.Success(Unit.Default);
    }
}

/// <summary>
/// Represents a unit value (no value).
/// </summary>
public readonly struct Unit
{
    public static readonly Unit Default = default;
}
