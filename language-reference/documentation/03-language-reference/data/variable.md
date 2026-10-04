---
type: data
category: variable
status: complete
last-modified: 2026-09-20T13:20:00
version: 0.1.1
---
## Summary

Variables in Nekonomicon store and manipulate data throughout a script. They can only hold a single value of the following primitives: [[text]], [[number]] or [[documentation/03-language-reference/data/primitives/boolean]].

---

## Syntax

A variable is a type of data that is always identified with the primary sigil (`@`) and an optional qualifier (`!`, `?` and `*`). Variables must be composed of a unique name made of characters (alphabetical, numerical and underscores).

```spell
@<qualifier><name>

@!my_var
@?nullable_var
@*my_password
```

### Qualifier Ordering

When combining qualifiers, the type qualifier (`!` or `?`) must always precede the secret qualifier (`*`). The reverse order, such as `@*!var`, is a syntax error.

```spell
@!*my_secret   ~ Valid: sealed secret
@?*my_secret   ~ Valid: nullable secret
@*!my_secret   ~ Error: secret qualifier must come last
```

---

## Declaration and Assignation

Variables are always assigned, even during declaration. This is done by using the [[sink]] operator (`into`) to create or update a variable.

```spell
~ Examples of using the sink operator.

'Hello, World!' into @greeting.  ~ greeting is set to 'Hello, World!'.
42 into @answer.                 ~ answer is set to 42.
32 into @answer.                 ~ answer is updated to 32.
```

Variables always retain their sigil prefixes whenever they are used. Here are the possible combinations of variable definitions.

| Symbol   | Name            | Mutable | Nullable | Secret | Description         |
| -------- | --------------- | ------- | -------- | ------ | ------------------- |
| `@var`   | Record          | Yes     | No       | No     | Standard variable   |
| `@!var`  | Sealed Record   | No      | No       | No     | Immutable constant  |
| `@?var`  | Nullable Record | Yes     | Yes      | No     | Can hold `null`     |
| `@*var`  | Secret          | Yes     | No       | Yes    | Redacted value      |
| `@!*var` | Sealed Secret   | No      | No       | Yes    | Sealed + redacted   |
| `@?*var` | Nullable Secret | Yes     | Yes      | Yes    | Nullable + redacted |

### Rules

- `!` (sealed) and `?` (nullable) are mutually exclusive and cannot be combined on the same variable
- Sealed variables cannot be reassigned after creation
- Nullable variables must be checked before use
- Secret variables cannot expose their value through output
- Names may contain alphanumeric characters and underscores (`_`)
- Names are case-sensitive
- A name that is already declared cannot be redeclared with a different qualifier

### Redeclaration

Attempting to declare a variable whose name is already in use, including with a different qualifier, is an error. This prevents accidentally shadowing an existing variable.

```spell
'First' into @message.
'Again' into @message.    ~ Valid: reassignment of a mutable record.

'First' into @message.
'Again' into @?message.   ~ Error: @message is already declared as a record.
```

---

## Special Variable Types

### Sealed Variables (Constants)

Use `@!` for sealed, that is, immutable values:

```spell
3.14 into @!PI.

'New value' into @!PI.  ~ Error: sealed records cannot be reassigned.
```

**Use Case:** Configuration values, mathematical constants, or any value that should not change during execution.

### Nullable Variables

Use `@?` for variables that may hold `null`:

```spell
null into @?maybe_value.

~ Check for null
decide @?maybe_value is null into @is_null.

if @is_null;
  say 'Value is null'.
else;
  say @?maybe_value.
end
```

**Assignment Flow:**

```spell
null into @?optional.                  ~ Initialize as null
'Now I have a value' into @?optional.  ~ Assign a value
null into @?optional.                  ~ Reset to null
```

### Secret Variables

Use `@*` for sensitive values that should not be exposed:

```spell
'my-api-key-123' into @*api_key.
```

A secret variable can be assigned like any other variable and used in operations that consume its value, such as authenticating a request. However, its value is redacted in any output: a `say` statement prints a placeholder rather than the value itself.

```spell
'my-api-key-123' into @*api_key.

say @*api_key.  ~ Outputs: [REDACTED]
```

**Use Case:** Passwords, API keys, tokens, and other sensitive data. See the [[security]] page for the complete set of permitted and forbidden operations on secrets.

---

## Variable Validation

You can validate that a value's primitive by using [[blueprint]] with the [[decide]] [[module]].

```spell
42 into @input.

decide @input is &number into @is_number.
decide @input is &boolean into @is_boolean.
decide @input is &text into @is_text.

if @is_number
  say 'Input is a number'.
else if @is_boolean
  say 'Input is a boolean'.
else if @is_text
  say 'Input is text'.
end

success.
```

For fail-fast validation, you can use the [[assert]] [[module]] instead:

```spell
42 into @value.
null into @?maybe.

assert @value is &number.   ~ Passes: @value holds a number.
assert @?maybe is not null. ~ Fails: @?maybe holds null value.

success. ~ Will not reach du to previous failure
```

See the [[decide]] and [[assert]] pages for the complete reference on value validation.

---

## Best Practices

**Do:**

- Use sealed variables (`@!`) for configuration and constants
- Check nullable variables (`@?`) before use with `decide ... is null`
- Use descriptive names: `@user_count` not `@x`
- Use `@*` for passwords, API keys, tokens, and other sensitive data

**Do not:**

- Overuse sealed variables; reserve them for true constants
- Access nullable variables without null checks
- Use [[reserved keywords]] as variable names
- Store secrets in non-secret variables