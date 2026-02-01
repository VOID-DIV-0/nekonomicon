---
title: Separators
slug: separators
category: core
status: wip
version: 0.0.1
since: 0.0.1
summary: Command terminators and delimiters that structure nekonomicon code.
tags: [separators, terminators, delimiters, syntax]
---

# Separators

In nekonomicon, separators are special characters or sequences that define the structure and flow of commands. They help delineate different parts of your code and make it more readable.

---

## Command Terminators

### Period (`.`)

The period is the standard command terminator. It indicates the end of a complete command.

```nekonomicon
say "Hello, World."
set x to 10.
repeat 5 times
  say "Iteration {x}."
  set x to x + 1.
end repeat.
```

### Newline

Newlines also serve as command terminators, allowing commands to span multiple lines for readability.

```nekonomicon
say "First command"
set name to "nekonomicon"
say "Hello, {name}!"
```

---

## Statement Separators

### Semicolon (`;`)

The semicolon allows multiple commands on the same line.

```nekonomicon
set x to 10; set y to 20; say "Sum is ${x + y}"
```

While technically supported, semicolons are discouraged in favor of line breaks for better readability.

---

## Block Delimiters

### Start/End Keywords

Blocks in nekonomicon use `start`/`end` keywords or specific block-type pairs:

```nekonomicon
# Generic start/end
if condition then
  start
    say "Inside block."
  end
end if

# Specific block types
repeat 3 times
  say "Iteration {i}."
end repeat

if condition then
  say "True case."
else
  say "False case."
end if
```

---

## Expression Delimiters

### Parentheses (`(` `)`)

Parentheses are used for grouping expressions and defining function parameters.

```nekonomicon
# Arithmetic grouping
set result to (10 + 5) * 2.

# Function parameters
process_data("input.txt", true)

# Complex expressions
if (x > 5) and (y < 10) then
  say "Condition met."
end if
```

### Brackets (`[` `]`)

Brackets are used for list indexing and slicing.

```nekonomicon
set fruits to ["apple", "banana", "cherry"]
set first to fruits[0]
set subset to fruits[1:3]  # Elements 1 and 2
```

### Braces (`{` `}`)

Braces are used for variable substitution and object literals.

```nekonomicon
# Variable substitution
say "Hello, {name}!"

# Expression substitution
say "Result: ${x + y}"

# Object literal (JSON-like)
set config to {
  "host": "localhost",
  "port": 8080,
  "ssl": true
}
```

---

## String Delimiters

### Single Quotes (`'`)

Single quotes define string literals.

```nekonomicon
set message to 'Hello, World!'
say 'This is a string.'
```

### Escaping

Use backslash (`\`) to escape special characters within strings:

```nekonomicon
say 'It\'s a beautiful day.'
say 'Path: C:\\Windows\\System32'
say 'Quote: "Hello"'
```

---

## Comments

### Single-line Comments

Hash (`#`) starts a single-line comment.

```nekonomicon
set x to 10  # This is a comment
say x          # This prints 10
```

### Multi-line Comments

Use `/*` to start and `*/` to end multi-line comments.

```nekonomicon
/*
  This is a
  multi-line comment
  that can span
  several lines
*/

set x to 10  # Code continues after comment block
```

---

## Whitespace

### Spaces and Tabs

nekonomicon is whitespace-insensitive for the most part, but consistent indentation is recommended for readability.

```nekonomicon
# All of these are equivalent:
set x to 10
say x

set    x    to    10
say    x

set
x
to
10
say
x
```

### Indentation

While not required by the language, consistent indentation (2 or 4 spaces) is strongly recommended for maintainability.

```nekonomicon
# Recommended style:
if condition then
  set x to 10
  if x > 5 then
    say "X is greater than 5"
  end if
end if
```

---

## Line Continuation

### Backslash (`\`)

Use backslash at the end of a line to continue a command on the next line.

```nekonomicon
set very_long_variable_name to \
  "This is a very long string " + \
  "that spans multiple lines"
```

---

## Operator Separators

### Assignment Operators

- `to` - Standard assignment
- `into` - Alternative assignment (sink syntax)

```nekonomicon
set x to 10
'Hello' into @message
```

### Comparison Operators

- `is` - Equality comparison
- `is not` - Inequality comparison
- `is at least` - Greater than or equal
- `is at most` - Less than or equal
- `is greater than` - Greater than
- `is less than` - Less than

```nekonomicon
if x is 10 then say "Equal"
if x is not 5 then say "Not equal"
if x is at least 10 then say "Greater or equal"
if x is greater than 5 then say "Greater"
```

---

## Best Practices

### 1. Consistent Style

Choose a style and stick with it:

```nekonomicon
# Good: Consistent use of terminators
set name to "Alice".
say "Hello, {name}!".

# Good: Newline terminators
set name to "Bob"
say "Hello, {name}!"

# Avoid: Inconsistent mixing
set name to "Carol"; say "Hello, {name}!". set age to 25
```

### 2. Readability Over Brevity

Use separators to make code readable:

```nekonomicon
# Good: Clear structure
if (temperature > 30) and (humidity < 50) then
  say "Hot and dry conditions."
end if

# Poor: Hard to read
if temperature>30and humidity<50then say"Hot and dry"endif
```

### 3. Proper Indentation

Even though not required, indentation helps:

```nekonomicon
# Good: Indented structure
repeat 5 times
  set i to i + 1
  if i is 3 then
    say "Halfway there!"
  end if
end repeat

# Poor: No indentation
repeat 5 times
set i to i + 1
if i is 3 then
say "Halfway there!"
end if
end repeat
```

---

## Common Pitfalls

### 1. Missing Terminators

```nekonomicon
# Wrong: Missing terminator
set x to 10
say x

# Correct: Complete termination
set x to 10.
say x.
```

### 2. Mismatched Delimiters

```nekonomicon
# Wrong: Mismatched brackets
set list = [1, 2, 3}

# Wrong: Unclosed string
say "Hello, World

# Correct: Properly matched
set list to [1, 2, 3]
say "Hello, World!"
```

### 3. Incorrect Escaping

```nekonomicon
# Wrong: Unescaped quote
say 'It's a test'

# Correct: Proper escaping
say 'It\'s a test'
```

---

## Advanced Usage

### Nested Structures

```nekonomicon
set config to {
  "database": {
    "host": "localhost",
    "port": 5432,
    "credentials": {
      "username": "admin",
      "password": "secret123"
    }
  },
  "features": ["auth", "logging", "cache"],
  "debug": true
}
```

### Complex Expressions

```nekonomicon
set result to ((x + y) * (z - w)) / 2.

if ((user.age is at least 18) and (user.has_permission)) then
  grant_access(user.id, "admin_panel")
end if
```

---

## Summary

Separators in nekonomicon are designed to make code:
- **Readable** with clear delimiters
- **Consistent** with predictable rules
- **Flexible** allowing multiple valid styles
- **Maintainable** with clear structure

Master these separators to write clean, professional nekonomicon scripts!