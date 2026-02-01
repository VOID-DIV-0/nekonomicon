---
title: Say Module
slug: say
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Output and logging operations.
tags: [say, output, print, logging]
---

# Say

## Description

The Say module provides operations for outputting text and data to the console. It is the primary way to display information, log messages, and communicate results to users.

## Summary Table

| Instruction | Minimal Syntax | Effect | Key Modifiers |
| ----------- | -------------- | ------ | ------------- |
| say | say @message. | Output message to console | - |

## Anatomy

Say commands follow this structure:

```
say <value> [signatures].
```

- `<value>`: Message, variable, or container to output
- `[signatures]`: Optional modifiers (trace, silent, etc.)

## Syntax

```
say <value>.
```

## Examples

### 1. Basic Output

```spell
say 'Hello, World!'.
```

### 2. Variable Interpolation

```spell
'Alice' into @name.
say 'Hello, @{name}!'.
```

### 3. Multiple Values

```spell
say 'The answer is:'.
42 into @answer.
say @answer.
```

### 4. Container Output

```spell
['apple', 'banana', 'cherry'] into ::fruits.
say ::fruits.
```

## Edge Cases

- Null values are displayed as 'null'.
- Container projections can be displayed directly.
- Very long strings may wrap based on terminal width.

## Best Practices

- Use variable interpolation for clearer messages.
- Use `say` for user-facing output.
- Use `trace` signature for debugging output.
- Avoid sensitive data in `say` unless using `<!> sensitive` clause.

## Common Errors

- **SAY-001**: Cannot output uninitialized variable

## Related Pages

- [Variables](../core/variables.md) — Variable types and interpolation
- [Signatures](../core/signatures.md) — Command modifiers
- [Clause](../core/clause.md) — Sensitive clause for secure output
