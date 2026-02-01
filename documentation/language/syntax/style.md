---
title: Style Guide
slug: style
category: core
status: wip
version: 0.0.1
since: 0.0.1
summary: Code style and formatting conventions for nekonomicon scripts.
tags: [style, formatting, conventions, best-practices]
---

# Style Guide

## Description

Code style and formatting conventions for writing clean, readable, and maintainable nekonomicon scripts.

## Style Guidelines

## General Philosophy

- Prioritize readability over brevity.
- Use consistent lowercase for all keywords and identifiers.
- Indent nested structures with two spaces per level.

## Line Length

- Keep statements on a single line if they are short.
- Use multiline formatting if the line exceeds approximately 80–100 characters.

## Modifiers (`with` / `without` / `on`)

- Place modifiers on new lines, indented.
- Align modifiers vertically for improved readability.

## Grouping Rules

- Group multiple modifiers together using commas.
- Separate grouped modifiers onto new lines with proper indentation.

## Sealed

- Favor using the `!` suffix to either variables or containers to indicate immutability and clarity of intent.

## Examples

### Short single-line

```nekonomicon
module example with arg1, arg2 without flag
```

### Wrapped multiline with separators

```nekonomicon
module example with
  arg1,
  arg2,
  arg3
without
  flag1,
  flag2
```

### Complex module with grouped modifiers (`with`, `without`)

```nekonomicon
module complex
  with
    arg1,
    arg2,
    arg3
  without
    flag1,
    flag2,
    flag3
  select
    option1,
    option2
```

## Editor/Formatter Responsibility

- Automatically collapse simple cases into single lines.
- Expand long or complex statements into multiline formatting.
- Maintain alignment of modifiers for readability during auto-formatting.
