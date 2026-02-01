---
title: Global Module
slug: global
category: module
status: stable
version: 0.0.1
since: 0.0.1
summary: Global-level definitions for nekonomicon scripts.
tags: [global, configuration, settings]
---

# Global

# Global

## Description

The Global module enables global-level configuration and module requirements for nekonomicon scripts.

nekonomicon supports a module that provide modifier options that have effects on the whole script. Some are unique keywords, other related to existing module. They applies the moment you enable the feature.

## Summary Table

| Global Instructions | Syntax Example                               | Effect                                       | Notes                                                    |
| ------------------- | -------------------------------------------- | -------------------------------------------- | -------------------------------------------------------- |
| require             | `global require module 'cabinet' >= '2.1.0'` | Request specific module version to be used   | Can also uses no module for nekonomicon versioning check |
| set                 | `global set silent 'off' `                   | Apply a runtime-wide setting                 |                                                          |
| reset               | `global reset silent`                        | Clear a runtime-wide setting back to default |                                                          |

### `require nekonomicon {version}`

**_Properties_**

- `version ['literal']`: Minimum nekonomicon version require.

**_Examples_**

```spell
global require nekonomicon >= '0.0.1'.
```

### `require module {module_name} <keyword_logical_operator> {version}`

**_Properties_**

- `module_name ['literal']`: Name of the module to version check.
- `<keyword_logical_operator>`: Can be either `>`, `>=`, `<`, `<=` and `=`.
- `version ['literal']`: Version semantic of the module.

**_Examples_**

```spell
global require 'cabinet' >= '0.0.1'.
global require 'text' >= '1.5.0'.
```

---

### `set {instruction}`

**_Properties_**

- `instruction`: Here is the following keyword global
  - `silent to {boolean}`: Set the script completely silent (no output IO).
  - `trace to {boolean}`: Write each commands as if using trace signature.
  - `timeout {time}`: Each command have a maximum time to run.
  - `elapsed {value}`: Print the elapsed time for each command.
  - `safe to {boolean}`: Make all command continue even if failure.
  - `elevated to {boolean}`: Make all the command defined as admin privileges.
  - `location {path}`: Change starting location of the script.
  - `logs {destination} [<log_type> ...]`: Provide logs to a specific files with only log type information.

**_Examples_**

```spell
global set silent to true.
global set timeout to 5 seconds.
global set safe to true.
```

---

### `reset <keyword_global>`

**_Properties_**

- `value ['literal']`: Value to set
- `<keyword_global>`:
  - `silent`: Script writes logs again.
  - `trace`: No more tracing.
  - `timeout`: Each command have no timeout.
  - `elapsed`: Don't print elapsed time for each command.
  - `safe`: Make all command continue even if failure.
  - `location`: reset to the calling original location.
  - `logs`: Disable logs.

**_Examples_**

```spell
global set trace to true.

~ ...

global reset trace.
```

---

## Related Pages

- [Modules **v0.0.1**](../modules-0.0.1.md)
