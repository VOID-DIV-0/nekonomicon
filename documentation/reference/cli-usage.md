---
version: 0.2.0
title: cli-usage
status: stable
---

# CLI Usage

## Overview

| Command          | Description                              |
| ---------------- | ---------------------------------------- |
| `neko version`   | Get the version of the neko interpreter  |
| `neko help`      | Display help information                 |
| `neko cast`      | Execute a nekonomicon script (.spell)    |
| `neko install`   | Install a nekonomicon module             |
| `neko uninstall` | Uninstall a nekonomicon module           |
| `neko list`      | List installed nekonomicon modules       |
| `neko inspect`   | Validate/lint a nekonomicon script       |
| `neko groom`     | Format a nekonomicon script or directory |
| `neko configure` | Configure Nekonomicon globally |

## Getting Version Info

To check the version of the neko interpreter installed on your system, use the following command:

```bash
neko version [OPTIONS]
```

This displays the version as a message. The version command also supports:
- `--simple`: Print only the version number without text. **(e.g. v1.5.0)**
- `--history`: Print the full changelog history.

## Display Help Information

For a list of available commands and options as cli you can display the help information with:

```bash
neko help 
neko help <COMMAND>

# examples:
neko help version
```

This command also allow asking information related to coding in Nekonomicon:

```bash
neko help code 
neko help code <CONCEPT>
```

## Casting a Nekonomicon Script

To cast a Nekonomicon script from the command line, ensure the neko interpreter is installed and accessible in your `$PATH` (see [installation](installation.md)).
Once installed, you can execute any .spell file with:

```bash
neko cast path/to/your/script.spell
```

This evaluates the script from top to bottom, producing any side effects or results defined within it.

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
neko cast path/to/your/script.spell with debug verbose, without silence colors
```

## Installing modules

Nekonomicon allows you to install additional modules to extend its functionality. To install a module, use the following command:

```bash
neko install <module-name>
```

Replace `<module-name>` with the name of the module you wish to install. This command will download and install the specified module, making its features available for use in your nekonomicon scripts.

### Module Versioning

Nekonomicon distinguishes between **Standard Library (STD) modules** and **external modules**:

**STD Modules (versionless):**
Built-in modules managed by nekonomicon releases. Always use the version bundled with your nekonomicon installation.

```bash
neko install cabinet      # STD module, no version needed
neko install vault        # STD module, no version needed
neko install network      # STD module, no version needed
```

**External Modules (versioned):**
Third-party modules require explicit version specification using semantic versioning.

```bash
# Specific version
neko install mymodule:1.2.3
neko install company/auth:2.0.0
neko install github/user/tool:0.5.1

# Latest version
neko install mymodule:latest
neko install mymodule              # Defaults to :latest
```

**Version format:**

- STD modules: `module-name` (no version)
- External modules: `module-name:version` or `namespace/module-name:version`
- Omitting version for external modules defaults to `:latest`

### Removing Modules

You can also remove an installed module using:

```bash
neko uninstall <module-name>
```

Similarly, replace `<module-name>` with the name of the module you wish to remove.

### Listing Installed Modules

To get list of all installed modules, use:

```bash
neko list
```

This displays all installed modules with their versions (for external modules).

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

Nekonomicon includes a built-in formatter to ensure your scripts are consistently styled and easy to read. Use the following command to format a script:

```bash
neko format path/to/your/script.spell
```

You can also format multiple scripts at once by specifying a directory:

```bash
neko format path/to/your/directory/
```

or using a glob pattern:

```bash
neko format 'path/to/your/scripts/*.spell'
```

## Using Nekonomicon in github action

```yaml
  jobs:
  run-script:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout code
        uses: actions/checkout@v4

      - name: Install Nekonomicon
        run: | 
        curl? https://nekonomicon.com/v1 && untar it && ./nekonomicon install

      - name: Cast Nekonomicon script
        run: |
          neko cast
          ~~~~~~~~~~~~~
          ~ Scripting
          ~~~~~~~~~~~~~
          vault unlock 'username' into @!username.
          vault unlock 'email' into @!email.

          decide @!username equals '' into @!username_is_empty.
          decide @!email equals '' or @!email is missing '@' into @!email_is_invalid.

          if @!email_is_invalid or @!username_is_empty
            success 'Email cannot be empty.'.
          end

          failure 
```