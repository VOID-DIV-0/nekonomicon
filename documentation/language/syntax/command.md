---
version: 0.1.0
title: command
status: stable
---

# Command

In nekonomicon, a command is a fundamental building block that performs specific actions or operations. There is four types of command:

- Intrinsic command.
- Module command.
- Block command.
- Function command.

## Anatomy of commands

### Intrinsic Command

Intrinsic commands are built-in commands that provide core functionality within nekonomicon. They perform specific tasks and can be used directly in scripts.

#### Syntax

```spell
[clause(s)] <intrinsic> [instruction(s)...] [modifiers...] [on platform].
```

Where:

[clause]: optional prepositions that modify command's execution context.
[intrinsic]: The core action to be performed.
[instruction(s)]: inputs required by the command.
[modifier(s)]: optional keywords that alter the command's behavior using `with`/`without`.
[on platform]: optional platform selector that must appear last.
[.]: Terminator indicating the end of the command.

#### Example

```spell
say 'Hello, World!' with silence with trace on linux.
```

### Module Command

Module commands are provided by specific modules and extend the functionality of nekonomicon. They follow a similar syntax to intrinsic commands but are prefixed by the module name. They are only available when the corresponding module is summoned (installed) and imported in the script (using `import <module>`).

#### Syntax

```spell
[clause(s)] <module> <action> [instruction(s)...] [modifier(s)...] [on platform].
```

Where:

- [clause(s)]: optional prepositions that modify command's execution context.
- [module]: The module providing the action.
- [action]: The specific operation the module performs.
- [instruction(s)]: inputs required by the action.
- [modifier(s)]: optional keywords that alter the action's behavior using `with`/`without`.
- [on platform]: optional platform selector that must appear last.
- [.]: Terminator indicating the end of the command.

#### Example

```spell
import cabinet.

cabinet read file 'config.txt' into @content.
cabinet write file 'output.txt' with content @content.
safe cabinet delete file 'temp.txt' with silence on linux.
```

### Block Command

Block commands are used to group multiple commands together with specific behavior, allowing for structured and organized code. They are defined using indentation and must end with the `end` keyword.

#### Syntax

```spell
<Intrinsic> [instruction(s)...]
    [command 1...]
    [command 2...]
    ...
end
```

#### Examples

```spell
if @condition is true
    say 'Condition met!'.
    calculate @a + @b into @result.
end
```

```spell
repeat 5 times
    say 'This will be printed 5 times.'.
end
```

### Function Command

Function commands are user-defined commands that encapsulate reusable logic. They are defined using the `function` keyword and can be invoked like intrinsic or module commands.

#### Syntax

```spell
function <function_name> [parameter(s)...]
    [command 1...]
    [command 2...]
    ...
end

[clause(s)] <function_name> [argument(s)...] [modifier(s)...] [on platform].
```

## Syntaxic Sugar

### Multiline

```spell
safe sensitive cabinet delete file '/root/very/long/folder' with force into ::operation_result with silence with trace on linux.
say 'that is a long command!'.
success.
```

Compared to

```spell
safe sensitive cabinet delete file '/root/very/long/folder' with force
                                                            into ::result
                                                            with silence
                                                            with trace
                                                            on linux.
```
