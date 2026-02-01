---
title: Math Module
slug: math
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Mathematical operations and utilities for numeric calculations.
tags: [math, arithmetic, calculation, numbers]
---

# Math

## Description

The Math module provides all the necessary logic to operate on records in a mathematical way. Since records are all strings, it lenses as integer or decimal internally, providing the right logic and validation logic automatically.

## Summary Table

| Instruction | Minimal Syntax | Effect | Key Modifiers |
| ----------- | -------------- | ------ | ------------- |
| eval | math eval @expr into @result. | Evaluate arithmetic/compare expression with precedence | with rounding |
| add | math add @a @b into @c. | Add two numbers | - |
| sub | math sub @a @b into @c. | Subtract two numbers | - |
| mul | math mul @a @b into @c. | Multiply two numbers | - |
| div | math div @a @b into @c. | Divide two numbers | - |
| mod | math mod @a @b into @r. | Calculate modulo (remainder) | - |
| abs | math abs @n into @out. | Get absolute value | - |
| round | math round @n into @out. | Round to nearest integer | - |
| floor | math floor @n into @out. | Round down to integer | - |
| ceil | math ceil @n into @out. | Round up to integer | - |
| min | math min @a @b into @out. | Get minimum of two values | - |
| max | math max @a @b into @out. | Get maximum of two values | - |
| clamp | math clamp @n min @low max @high into @out. | Constrain value to range | min, max |
| pow | math pow @n @exp into @sq. | Raise to power | - |
| compare | math compare @a > @b into @result. | Compare two numbers | Operators: >, <, >=, <=, ==, != |
| random | math random min @low max @high into @r. | Generate random number | min, max |

## Anatomy

Math commands follow this structure:

```
math <verb> <operand(s)> [with <qualifier>] [into <sink>].
```

- `<verb>`: The mathematical operation (add, sub, mul, etc.)
- `<operand(s)>`: One or more values (records or literals)
- `[with <qualifier>]`: Optional modifiers (e.g., `with rounding`)
- `[into <sink>]`: Target variable for result (record or container)

## Syntax

```
math <operation> <arguments> [modifiers] into <target>.
```

## Examples

### 1. Basic Arithmetic

```spell
math add '10' '5' into @sum.
say @sum.  ~ Output: 15

math sub '10' '5' into @difference.
say @difference.  ~ Output: 5

math mul '10' '5' into @product.
say @product.  ~ Output: 50

math div '10' '5' into @quotient.
say @quotient.  ~ Output: 2
```

### 2. Modulo and Absolute Value

```spell
math mod '10' '3' into @remainder.
say @remainder.  ~ Output: 1

'-42' into @negative.
math abs @negative into @positive.
say @positive.  ~ Output: 42
```

### 3. Rounding Operations

```spell
'3.7' into @value.

math round @value into @rounded.
say @rounded.  ~ Output: 4

math floor @value into @floored.
say @floored.  ~ Output: 3

math ceil @value into @ceiled.
say @ceiled.  ~ Output: 4
```

### 4. Min, Max, and Clamp

```spell
math min '10' '5' into @minimum.
say @minimum.  ~ Output: 5

math max '10' '5' into @maximum.
say @maximum.  ~ Output: 10

math clamp '15' min '0' max '10' into @clamped.
say @clamped.  ~ Output: 10
```

### 5. Power and Comparison

```spell
math pow '2' '3' into @result.
say @result.  ~ Output: 8

math compare '10' > '5' into @is_greater.
say @is_greater.  ~ Output: true
```

### 6. Random Numbers

```spell
math random min '1' max '10' into @random_num.
say 'Random number: @{random_num}'.
```

### 7. Expression Evaluation

```spell
math eval '(10 + 5) * 2' into @result.
say @result.  ~ Output: 30
```

## Edge Cases

- Division by zero will cause an error unless used with `safe` clause.
- Records are automatically lensed to numbers; invalid formats will fail.
- Comparison operations return boolean records ('true' or 'false').
- Random number generation is inclusive of both min and max values.
- Modulo with negative numbers follows standard mathematical convention.

## Best Practices

- Use `safe` for operations that might fail (e.g., division, invalid inputs).
- Always validate numeric inputs when working with user data.
- Use `clamp` instead of manual min/max checks for range constraints.
- Prefer `compare` over `eval` for simple comparisons (clearer intent).
- Store commonly used values in sealed records (`@!`) for constants.

## Common Errors

- **MATH-001**: Division by zero
- **MATH-002**: Invalid numeric format
- **MATH-003**: Overflow or underflow

## Related Pages

- [Calculate](../core/calculate.md) — Core calculation syntax
- [Variables](../core/variables.md) — Record types and storage
- [Decide](../core/decide.md) — Boolean logic and comparisons
- [Decimal](./decimal.md) — Decimal type operations
