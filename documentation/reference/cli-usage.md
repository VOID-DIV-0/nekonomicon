---
version: 0.1.0
title: cli-usage
status: stable
---

# CLI Usage

## Overview

| Command         | Description                              |
| --------------- | ---------------------------------------- |
| `neko story`    | Get the version of the neko interpreter  |
| `neko help`     | Display help information                 |
| `neko conjure`  | Run a nekonomicon script (.spell file)   |
| `neko summon`   | Install a nekonomicon module             |
| `neko unsummon` | Uninstall a nekonomicon module           |
| `neko grimoire` | List installed nekonomicon modules       |
| `neko groom`    | Validate/lint a nekonomicon script       |
| `neko attune`   | Format a nekonomicon script or directory |
| `neko brew`     | Start the interactive nekonomicon REPL   |

## Getting Version Info

To check the version of the neko interpreter installed on your system, use the following command:

```bash
neko story
```

## Display Help Information

For a list of available commands and options, you can display the help information with:

```bash
neko help
```

## Running a Nekonomicon Script

To run a Nekonomicon script from the command line, ensure the neko interpreter is installed and accessible in your $PATH.
Once installed, you can conjure (execute) any .spell file with:

```bash
neko conjure path/to/your/script.spell
```

This evaluates the spell from top to bottom, producing any side effects or results defined within the script.

It supports modifiers like:

- `with(out) debug`: enables or disables debug logging
- `with(out) verbose`: enables or disables verbose output
- `with(out) silence`: enables or disables silent mode
- `with(out) colors`: enables or disables colored output
- `with(out) timestamps`: enables or disables timestamps in logs
- `with mode 'type'`: enables or disables pretty printing of results, where 'type' can be `minimal`, `pretty`, `educative` or `json`

### An example with educative display mode

```
╭─ ERROR ────────────────────────────────────────────────────────╮
│                                                                │
│   'hello' into @age &integer.                                  │
│   ~~~~~~~           ~~~~~~~~                                   │
│      │                  │                                      │
│      │                  └─ schema requires: integer            │
│      └─ this value: 'hello'                                    │
│                                                                │
├─ WHY? ─────────────────────────────────────────────────────────┤
│                                                                │
│   The &integer schema expects a string containing only         │
│   digits (0-9), optionally with a leading minus sign.          │
│                                                                │
│   'hello' contains letters, which are not valid.               │
│                                                                │
├─ FIX ──────────────────────────────────────────────────────────┤
│                                                                │
│   • Use a valid integer: '42' into @age &integer.              │
│   • Or remove the schema: 'hello' into @age.                   │
│                                                                │
╰────────────────────────────────────────────────────────────────╯
```

A cli example with modifiers:

```bash
neko conjure path/to/your/script.spell with debug verbose, without silence colors
```

## Installing modules

Nekonomicon allows you to install additional modules to extend its functionality. To install a module, use the following command:

```bash
neko summon <module-name>
```

Replace `<module-name>` with the name of the module you wish to install. This command will download and install the specified module, making its features available for use in your nekonomicon scripts.

### Module Versioning

Nekonomicon distinguishes between **Standard Library (STD) modules** and **external modules**:

**STD Modules (versionless):**
Built-in modules managed by nekonomicon releases. Always use the version bundled with your nekonomicon installation.

```bash
neko summon cabinet      # STD module, no version needed
neko summon vault        # STD module, no version needed
neko summon network      # STD module, no version needed
```

**External Modules (versioned):**
Third-party modules require explicit version specification using semantic versioning.

```bash
# Specific version
neko summon mymodule:1.2.3
neko summon company/auth:2.0.0
neko summon github/user/tool:0.5.1

# Latest version
neko summon mymodule:latest
neko summon mymodule              # Defaults to :latest
```

**Version format:**

- STD modules: `module-name` (no version)
- External modules: `module-name:version` or `namespace/module-name:version`
- Omitting version for external modules defaults to `:latest`

### Removing Modules

You can also remove an installed module using:

```bash
neko unsummon <module-name>
```

Similarly, replace `<module-name>` with the name of the module you wish to remove.

### Listing Installed Modules

To get list of all installed modules, use:

```bash
neko grimoire
```

This displays all summoned modules with their versions (for external modules).

## Validating a Nekonomicon Script

Before conjuring a script, you can verify that it is syntactically sound and free of unexpected mishaps.
Use groom to lint and validate a spell:

> **Note:**
> Lint means to check for syntax errors and potential issues without executing the script.

```bash
neko groom path/to/your/script.spell
```

If the spell is malformed, groom will report errors and highlight where the magic destabilizes.

## Formatting a Nekonomicon Script

Nekonomicon includes a built-in formatter to ensure your spells are consistently styled and easy to read. Use the following command to format a script:

```bash
neko attune path/to/your/script.spell
```

You can also format multiple scripts at once by specifying a directory:

```bash
neko attune path/to/your/directory/
```

or using a glob pattern:

```bash
neko attune 'path/to/your/scripts/*.spell'
```

## Entering the REPL

Nekonomicon includes an interactive REPL (Read–Eval–Print Loop), ideal for testing snippets, experimenting, and summoning small magical constructs without writing a full script.
Start it with:

```bash
neko brew
```
