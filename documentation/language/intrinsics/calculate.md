---
version: 0.2.0
title: calculate
status: stable
---

# Calculate

## Description

`calculate` is nekonomicon's intrinsic for evaluating arithmetic expressions. It is used to compute numeric results, producing a numeric value to be sinked into a variable or constant. All comparisons, logic, and string operations are handled elsewhere. The keyword `calculate` is strictly for numbers.

---

## Philosophy

nekonomicon separates arithmetic from comparisons, logic, and string manipulation. Use `calculate` for all numeric calculations. Use `decide` for comparisons and boolean logic, and the `text` module for any string operations.

- **Explicit:** Only numeric operations are allowed.
- **Minimal:** No string concatenation, no comparisons, no boolean logic, no side effects.
- **Readable:** Each `calculate` line does one thing—compute a number.

---

## Syntax

```
calculate <arithmetic_expr> into <@result>.
```

Where:

- `<arithmetic_expr>` uses numbers, records, and arithmetic operators: `+ - * / % ** //`

The order of operations follows standard mathematical precedence (PEMDAS). It is evaluated left to right for operators of the same precedence level. It supports parentheses for grouping but it's recommended to keep expressions simple.

---

## Supported Operators

| Operator | Meaning        | Example     |
| -------- | -------------- | ----------- |
| +        | Addition       | `@a + @b`   |
| -        | Subtraction    | `@a - @b`   |
| \*       | Multiplication | `@a * @b`   |
| /        | Division       | `@a / @b`   |
| %        | Modulus        | `@a % @b`   |
| \*\*     | Power          | `@a ** @b`  |
| \/\/     | Root           | `@a // @b`  |
| ()       | Parentheses    | `(@a + @b)` |

---

## Type Rules

- All operands must be numeric (unquoted numbers or records containing numbers).
- Mixing non-numeric types results in an error (`E-CALCULATE-NONNUM`).
- No comparisons (`>`, `<`, `==`, etc.) are allowed—use `decide` for comparisons.
- No boolean logic (`and`, `or`, `not`) is allowed—use `decide` for logic.

---

## Examples

### Basic Arithmetic

```spell
calculate 5 + 3 into @sum.              ~ 8
calculate @price * @qty into @total.
calculate @balance - @cost into @remain.
calculate 10 % 3 into @mod.             ~ 1
calculate 100 / 4 into @quarter.        ~ 25
```

### Using Parentheses

```spell
calculate (@a + @b) * @c into @result.
calculate @a * (@b + @c) into @result2.
calculate (@x + @y) / (@z - 1) into @ratio.
```

### Power and Root

```spell
calculate 2 ** 8 into @power.           ~ 256
calculate 16 // 2 into @root.           ~ 4 (square root of 16)
calculate 27 // 3 into @cube_root.      ~ 3 (cube root of 27)
```

### Chaining Calculations

```spell
calculate @a + @b into @sum.
calculate @sum * 2 into @double.
calculate @double - 5 into @final.
```

---

## Best Practices

1. **Keep each calculation simple:** Break complex expressions into multiple lines.
2. **Use only numeric operands:** Cast or convert values before using them in `calculate`.
3. **Use `decide` for comparisons and logic:** Do not use comparison or boolean operators in `calculate`.
4. **Handle errors explicitly:** Check for division by zero and type mismatches.
5. **Use parentheses sparingly:** Prefer intermediate variables for complex expressions.

---

## Error Handling

| Error Code            | Description                            |
| --------------------- | -------------------------------------- |
| E-CALCULATE-NONNUM    | Non-numeric operand in arithmetic      |
| E-CALCULATE-DIVZERO   | Division by zero                       |
| E-CALCULATE-NEGROOT   | Cannot compute root of negative number |
| E-CALCULATE-BADOP     | Unsupported operator                   |
| E-CALCULATE-PARENMISM | Mismatched parentheses                 |

---

## Related Pages

- [Decide (boolean evaluation)](./decide.md)
- [Math Module (advanced math)](../modules/math.md)
- [Flow Control](./flow.md)
- [Data Types](./data.md)

---

## Version

- 0.2.0 — Arithmetic only; comparisons moved to `decide`.
- 0.1.0 — Arithmetic and comparison only; all string and logic operations removed.

---
