---
title: Ask Module
slug: ask
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Interactive user input prompts with validation and masking support.
tags: [ask, input, prompt, user-interaction]
---

# Ask

## Description

The Ask module provides interactive prompts for user input. It allows you to request input from users, optionally providing a default value, validating input against a schema, or hiding input (e.g., for passwords). The module blocks execution until user input is received.

## Summary Table

| Instruction | Minimal Syntax | Effect | Key Modifiers |
| ----------- | -------------- | ------ | ------------- |
| ask | ask @prompt into @input. | Prompt user for input | with default, with schema, with mask |

## Anatomy

Ask commands follow this structure:

```
ask <prompt> [with <modifier>] into <sink>.
```

- `<prompt>`: Message displayed to the user (record or literal)
- `[with <modifier>]`: Optional qualifiers (default, schema, mask)
- `into <sink>`: Target variable for user input

## Syntax

```
ask <prompt> [with default <value>] [with schema <type>] [with mask] into <target>.
```

## Examples

### 1. Simple Prompt

```spell
ask 'What is your name?' into @name.
say 'Hello, @{name}!'.
```

### 2. Prompt with Default Value

```spell
ask 'Favorite color?' with default 'blue' into @color.
say 'You chose: @{color}'.
```

### 3. Prompt with Schema Validation

```spell
ask 'Enter your age:' with schema integer into @age.
say 'You are @{age} years old.'.
```

### 4. Prompt with Boolean Schema

```spell
ask 'Subscribe to newsletter?' with schema bool into @subscribe.

if @subscribe
  say 'Thank you for subscribing!'.
end
```

### 5. Hidden Input (Password)

```spell
sensitive ask 'Enter your password:' with mask into @!password.
say 'Password accepted.'.
```

### 6. Combined Validation and Masking

```spell
sensitive ask 'Enter 4-digit PIN:' with schema integer with mask into @!pin.
say 'PIN set successfully.'.
```

### 7. Multiple Prompts

```spell
ask 'Enter username:' into @username.
sensitive ask 'Enter password:' with mask into @!password.

say 'Account created for @{username}.'.
success.
```

## Edge Cases

- If a default is provided and the user submits empty input, the default is returned.
- If a schema is provided, the input is validated and coerced to the schema type. Invalid input will re-prompt the user.
- When `with mask` is used, input is not shown on the screen (useful for sensitive data).
- The module blocks execution until valid input is received.
- Empty input without a default will re-prompt the user.

## Best Practices

- Use `sensitive` clause when prompting for passwords or secrets.
- Always store masked input in sealed variables (`@!`) to prevent accidental modification.
- Provide clear, concise prompt strings.
- Use schemas to validate input type early.
- Combine `with mask` with vault operations for secure secret handling.
- Consider providing defaults for optional configuration values.

## Common Errors

- **ASK-001**: Invalid schema type
- **ASK-002**: User cancelled prompt (Ctrl+C)
- **ASK-003**: Input validation failed

## Related Pages

- [Variables](../core/variables.md) — Record types and sealed variables
- [Vault](./vault-0.0.1.md) — Secret storage
- [Clause](../core/clause.md) — Sensitive clause usage
- [Schema](../core/schema.md) — Type validation
