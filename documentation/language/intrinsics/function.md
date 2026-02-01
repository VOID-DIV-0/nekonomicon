---
title: Functions
slug: functions
category: core
status: stable
version: 0.3.0
since: 0.1.0
summary: Function declaration, invocation, and parameter handling with support for positional and named parameters.
tags: [function, core, intrinsic, parameters]
---

# Functions

## Description

Functions in nekonomicon allow you to define reusable logic blocks with parameters, optional parameters, and clear success/failure signaling. Functions support both positional and named parameters, enabling flexible and readable function calls while maintaining nekonomicon's explicit philosophy.

## Summary Table

| Pattern            | Syntax                                    | Effect                             | Notes                               |
| ------------------ | ----------------------------------------- | ---------------------------------- | ----------------------------------- |
| Definition         | `function name ... end`                   | Define reusable function           | Must end with success/fail          |
| Positional Call    | `func arg1 arg2.`                         | Call with ordered arguments        | Arguments match parameter order     |
| Named Call         | `func with param1 arg1 with param2 arg2.` | Call with explicit parameter names | Self-documenting, order-independent |
| Mixed Call         | `func arg1 with param2 arg2.`             | Positional first, then named       | Best of both approaches             |
| Function Injection | `module do func args.`                    | Pass function to module            | Module controls execution           |

## Anatomy

Functions use the `function <name> ... end` block structure. Parameters are declared inside the function using either numbered (positional) or named identifiers. Function calls match the parameter style defined in the function.

## Syntax

```
function name
  parameter 1 into @var.           # Positional parameter
  parameter name into @var.        # Named parameter
  safe parameter 2 into @optional. # Optional positional
  # function body
  success.
end
```

## Examples

### 1. Positional Parameters

```spell
function add
  parameter 1 into @a.
  parameter 2 into @b.

  solve @a + @b into @result.
  success @result.
end

add 5 10.  # Simple, ordered arguments
```

### 2. Named Parameters

```spell
function deploy_service
  parameter environment into @!env.
  parameter version into @!ver.
  parameter rollback into @can_rollback.

  say 'Deploying @{ver} to @{env}'.
  success.
end

deploy_service with environment 'production'
               with version '1.2.3'
               with rollback true.
```

### 3. Mixed Parameters

```spell
function greet
  parameter 1 into @!name.          # Required positional
  parameter 2 into @surname.        # Required positional
  parameter title into @title.      # Optional named
  parameter formal into @is_formal. # Optional named
end

greet 'John' 'Doe'.                                    # Positional only
greet 'John' 'Doe' with title 'Dr.'.                  # Mixed
greet 'John' 'Doe' with title 'Dr.' with formal true. # All parameters
```

### 4. Optional Parameters

```spell
function connect_db
  parameter 1 into @!host.
  safe parameter 2 into @port.      # Optional positional
  parameter timeout into @timeout.  # Optional named

  # Use defaults if not provided
  @port is unknown into @no_port.
  if @no_port
    '5432' into @port.
  end

  say 'Connecting to @{host}:@{port}'.
  success.
end

connect_db 'localhost'.                        # Uses default port
connect_db 'localhost' '3306'.                 # Custom port
connect_db 'localhost' with timeout '30s'.    # Default port, custom timeout
```

### 5. Sealed Parameters and Validation

```spell
import assert.

function secure_operation
  parameter 1 into @!api_key.       # Sealed (readonly)
  parameter action into @!action.   # Sealed named parameter

  assert @!api_key is &string.
  assert @!action is &string.

  # api_key and action cannot be modified within function
  say 'Executing @{action} with secure key'.
  success.
end

secure_operation 'secret123' with action 'deploy'.
```

## Edge Cases

- **Parameter order:** Positional parameters must come before named parameters in function calls
- **Missing positional:** Required positional parameters must be provided or function fails
- **Parameter gaps:** Positional parameters must be numbered sequentially (1, 2, 3...) without gaps
- **Mixed styles:** Cannot mix positional and named parameter declarations in the same function
- **Sealed parameters:** Using `@!` prevents modification within the function body

## Best Practices

- Use positional parameters for simple, obvious arguments (2-3 parameters max)
- Use named parameters for complex functions with many options
- Use mixed calls when you have required core parameters plus optional configuration
- Always seal security-sensitive parameters with `@!`
- End all functions with explicit `success` or `fail`
- Use descriptive parameter names that match the function's purpose
- Prefer named parameters for boolean flags and optional configuration

## Common Errors

| Error Code              | Description                                           |
| ----------------------- | ----------------------------------------------------- |
| E-FUNC-NOTFOUND         | Function name does not exist                          |
| E-FUNC-MISSING-POS      | Required positional parameter not provided            |
| E-FUNC-TOO-MANY-POS     | Too many positional arguments provided                |
| E-FUNC-NAMED-BEFORE-POS | Named parameter used before all positional parameters |
| E-FUNC-UNKNOWN-PARAM    | Named parameter not declared in function              |
| E-FUNC-NO-SIGNAL        | Function did not return success or fail               |
| E-FUNC-SEALED-MODIFY    | Attempt to modify sealed parameter                    |

## Related Pages

- [Sinks](./sinks.md)
- [Data Types](./data.md)
- [Flow Control](./flow.md)
- [Assert Module](../modules/assert.md)
