---
version: 0.1.0
title: modifiers
status: stable
---

# Modifiers

## Description

nekonomicon uses readable keywords `with`, `without`, and `on` so modifiers stay intuitive, memorable, and easy to chain. Modifiers control both command-specific behavior and global execution settings.

## Types of Modifiers

### Command-Specific Modifiers

These modifiers are specific to individual commands or modules:

- Use `with <option>` to enable an option or specify its value
- Use `without <option>` to disable an option
- Repeating the same option is a compile-time error

### Global Modifiers

These modifiers work with any command and control execution behavior:

- `with silence` / `without silence` - Control output verbosity
- `with trace` / `without trace` - Enable/disable command tracing
- `with timeout '<duration>'` - Set execution time limits
- `with retry '<count>'` - Set retry attempts for failed commands

### Platform Selectors

Control which platforms a command runs on:

- `on linux` - Execute only on Linux systems
- `on mac` - Execute only on macOS systems
- `on windows` - Execute only on Windows systems

## Examples

### Command-Specific Modifiers

```spell
cabinet file list
  with depth '3'
  with hidden
  without extension
  filter '[A-Za-z]'.
```

### Global Modifiers

```spell
vault unlock 'api-key'
  with timeout '10s'
  with retry '3'
  with silence
  on linux
  into @key.
```

### Combined Usage

```spell
cabinet copy file '/important/data' to '/backup/location'
  with compression
  with verification
  with trace
  with timeout '5m'
  on linux.
```

## Syntax Rules

- All modifiers appear after the main command operands
- Global modifiers can be combined with command-specific modifiers
- Platform selectors (`on <platform>`) should appear last
- Modifiers can be chained inline or across multiple lines for readability
