---
version: 0.2.0
title: decide
status: stable
---

# Decide

## Description

`decide` is nekonomicon's intrinsic for performing comparisons and composing boolean logic. It enables clear, English-like expression of conditions, combining comparisons and boolean records into readable statements.

Unlike arithmetic (`calculate`), which computes numeric results, `decide` is used exclusively for evaluating truth—answering "should this action proceed?" or "does this condition hold?" in your scripts.

---

## Philosophy

nekonomicon treats comparisons and boolean logic as a distinct phase of reasoning, separate from arithmetic. Use `calculate` for numeric operations, and `decide` for all comparisons and logical composition—AND, OR, NOT, XOR, and multi-branch conditions. This separation keeps scripts explicit, readable, and easy to audit.

- **Explicit:** No hidden type coercion or implicit truthiness. Operands must be pre-calculated values (variables or literals), not expressions.
- **English-like:** Reads naturally—`decide @a > @b and not @blocked into @ok.`
- **Minimal:** No operator precedence confusion; logic is composed left-to-right.

---

## Syntax

```
decide <condition_expr> into <@result>.
```

Where `<condition_expr>` is:

- A boolean record (`@flag`)
- A relational comparison (`@a > @b`, `@x == 'foo'`)
- Logical operators: `and`, `or`, `xor`
- Unary negation: `not`
- Any combination of the above, composed left-to-right

**Important:** All operands must be pre-calculated values (variables or literals). Arithmetic expressions are not allowed inside `decide`—use `calculate` first.

**No parentheses or nested expressions.**  
For complex logic, break into multiple lines for clarity.

---

## Supported Operators

### Comparison Operators

| Operator | Meaning          | Example          |
| -------- | ---------------- | ---------------- |
| >        | Greater than     | `@a > @b`        |
| <        | Less than        | `@a < @b`        |
| >=       | Greater or equal | `@a >= @b`       |
| <=       | Less or equal    | `@a <= @b`       |
| ==       | Equality         | `@x == @y`       |
| !=       | Inequality       | `@flag != 'yes'` |

### Logical Operators

| Operator | Meaning             | Example     |
| -------- | ------------------- | ----------- |
| and      | Logical AND         | `@a and @b` |
| or       | Logical OR          | `@a or @b`  |
| xor      | Logical XOR         | `@a xor @b` |
| not      | Logical NOT (unary) | `not @a`    |

### Schema & Null Operators

| Operator   | Meaning              | Example                 |
| ---------- | -------------------- | ----------------------- |
| is         | Schema/null check    | `@?x is null`           |
| is not     | Negated schema check | `@?x is not null`       |
| is integer | Type check           | `@val is integer`       |
| is bool    | Type check           | `@flag is bool`         |
| is &Schema | Container schema     | `::user is &UserSchema` |

### String Comparisons



---

## Examples

### 1. Simple Comparison

```spell
decide @age > 18 into @is_adult.
decide @status == 'active' into @is_active.
decide @score >= 90 into @high_score.
```

### 2. Comparison with Pre-calculated Values

```spell
calculate @price * @qty into @total.
decide @total > 100 into @qualifies_for_discount.
```

### 3. Boolean Composition

```spell
decide @age > 18 into @is_adult.
decide @country == 'US' into @is_domestic.
decide @is_adult and @is_domestic into @eligible.
```

### 4. Multi-Branch Logic

```spell
decide @balance > 0 into @has_funds.
decide @status == 'active' into @active.
decide @has_funds and @active or @override into @can_proceed.
```

### 5. Using NOT and XOR

```spell
decide @score >= 90 into @high_score.
decide @bonus == 'yes' into @has_bonus.
decide @high_score xor @has_bonus into @has_one_not_both.
decide not @blocked and @has_one_not_both into @award.
```

### 6. String Equality

```spell
decide @user == 'admin' into @is_admin.
decide @is_admin and not @suspended into @can_access.
```

### 7. Chaining for Readability

```spell
calculate @x + @y into @sum.
calculate @a * @b into @product.

decide @sum > 10 into @sum_check.
decide @product < 50 into @product_check.
decide @sum_check and @product_check into @both_valid.
```

### 8. Null Checking

```spell
null into @?maybe_value.

decide @?maybe_value is null into @is_null.
decide @?maybe_value is not null into @has_value.

if @has_value
  say @?maybe_value.
end
```

### 9. Type/Schema Checking

```spell
'42' into @input.

decide @input is integer into @is_int.
decide @input is bool into @is_bool.

if @is_int
  say 'Input is a valid integer'.
end
```

### 10. Container Schema Validation

```spell
schema &User
  field 'name' is string.
  field 'age' is integer.
  field :?email is string.  ~ Optional field
end

container
  string 'Alex' into :name
  string '25' into :age
into ::user.

decide ::user is &User into @is_valid.

if @is_valid
  say 'User is valid!'.
end
```

---

## Best Practices

1. **Pre-calculate arithmetic:** Use `calculate` for any arithmetic, then use the result in `decide`.
2. **Keep logic flat:** Avoid deeply nested or overly long `decide` lines. Break into steps for clarity.
3. **Be explicit:** Only use pre-calculated values (variables or literals) as operands.
4. **No implicit truthiness:** Strings, numbers, or containers are not automatically treated as booleans.
5. **Left-to-right evaluation:** Logic is evaluated in order; no operator precedence. For complex conditions, use intermediate variables.
6. **Separate concerns:** Comparisons produce booleans, logic combines booleans—keep them clear.

---

## Error Handling

| Error Code            | Description                               |
| --------------------- | ----------------------------------------- |
| E-DECIDE-NONBOOL      | Operand is not a boolean or comparison    |
| E-DECIDE-TYPEMISMATCH | Incompatible types in comparison          |
| E-DECIDE-CHAIN        | Improper chaining of relational operators |
| E-DECIDE-EMPTY        | No operands supplied                      |
| E-DECIDE-OPUNKNOWN    | Unknown logical operator                  |
| E-DECIDE-NULLACCESS   | Accessing non-nullable variable as null   |
| E-DECIDE-SCHEMAUNK    | Unknown schema referenced                 |

---

## Related Pages

- [Calculate (arithmetic)](./calculate.md)
- [Flow Control](./flow.md)
- [Bool Module (advanced logic)](../modules/bool.md)
- [Schema](./schema.md)
- [Variables](./variables.md)
- [Container](./container.md)

---

## Version

- 0.3.0 — Added schema and null checking with `is` operator.
- 0.2.0 — Comparisons moved from `calculate` to `decide`; requires pre-calculated operands.
- 0.1.0 — Initial version

---
