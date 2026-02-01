---
title: Variables
slug: variables
category: core
status: stable
version: 0.2.0
since: 0.0.1
summary: Variable types and data storage with records, sealed, and nullable variants.
tags: [variables, records, data, storage]
---

# Variables

## Description

Variables in nekonomicon are used to store and manipulate data throughout the script. They can hold different types of values, including strings, numbers, and booleans. Variables are defined using the `@` symbol followed by the variable name.

## Variable Types

| Symbol  | Type            | Mutable? | Nullable? | Example                  |
| ------- | --------------- | -------- | --------- | ------------------------ |
| `@var`  | Record          | ✅ Yes   | ❌ No     | `'hello' into @name.`    |
| `@!var` | Sealed Record   | ❌ No    | ❌ No     | `'constant' into @!MAX.` |
| `@?var` | Nullable Record | ✅ Yes   | ✅ Yes    | `null into @?maybe.`     |

**Rules:**

- `!` = sealed (constant, cannot be reassigned)
- `?` = nullable (can hold `null`)
- `!` and `?` are mutually exclusive

---

## Defining Variables

Use the `into` sink to assign a value to a variable. Variable names can consist of alphanumeric characters and underscores (`_`).

```spell
'Hello, World!' into @greeting.
42 into @answer.
true into @is_active.
```

---

## Using Variables

Reference variables using the `@` symbol:

```spell
say @greeting.   ~ Outputs: Hello, World!
say @answer.     ~ Outputs: 42
say @is_active.  ~ Outputs: true
```

---

## Modifying Variables

Reassign values using `into`:

```spell
'Goodbye, World!' into @greeting.
```

---

## Sealed (Constant) Variables

Use `@!` for values that cannot be changed after assignment:

```spell
'This is constant' into @!constant_var.

~ This would cause an error:
~ 'New value' into @!constant_var.  ~ ERROR: Cannot reassign sealed variable
```

---

## Nullable Variables

Use `@?` for variables that can hold `null`:

```spell
null into @?maybe_value.

~ Check if null using decide
decide @?maybe_value is null into @is_null.

if @is_null
  say 'Value is null'.
else
  say @?maybe_value.
end
```

### Assigning Values to Nullable Variables

```spell
null into @?optional.           ~ Start as null
'Now I have a value' into @?optional.  ~ Assign a value
null into @?optional.           ~ Back to null
```

---

## Type Checking with Schema

Use `decide` with `is` to check variable types:

```spell
'42' into @input.

decide @input is integer into @is_int.
decide @input is bool into @is_bool.
decide @input is string into @is_str.

if @is_int
  say 'Input is an integer'.
end
```

---

## Assert for Validation

Use `assert` to fail-fast if a condition is not met:

```spell
'42' into @value.

assert @value is integer.       ~ Passes
assert @?maybe is not null.     ~ Fails if null
```

---

## Related Pages

- [Container](./container.md) — Structured data with `::` symbol
- [Schema](./schema.md) — Type definitions and validation
- [Decide](./decide.md) — Boolean logic and comparisons
- [Literals](./literals.md) — String, number, and boolean literals
