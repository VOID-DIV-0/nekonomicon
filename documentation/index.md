# Homepage

Welcome to the nekonomicon documentation. Here you'll find guides and references for all major language features and modules.
Only "versioned" component means they are "final" documentation

## Reference

- **Stable** [Installation Guide](./reference/installation.md) — Install nekonomicon on your system.
- **Stable** [Quickstart Guide](./reference/quickstart.md) — Get started with nekonomicon scripting in minutes.
- **Stable** [System Requirements](./reference/system-requirements.md) — Minimum and recommended system requirements.
- **WIP** [CLI Usage](./reference/cli-usage.md) — Command-line interface and tooling.

## Language

### Syntax

[Commands](./language/syntax/command.md) — The structure and anatomy of nekonomicon commands with unified modifiers.

- **WIP** [Comments](./language/syntax/comment.md) — Single-line and multiline comment syntax.
- **WIP** [Literals](./language/syntax/literals.md) — String, number, and boolean literals.
- **Stable** [Separators](./language/syntax/separators.md) — Command terminators and delimiters.
- **WIP** [Style Guide](./language/syntax/style.md) — Formatting and naming conventions.

### Types

- **WIP** [Variables](./language/types/variables.md) — Variable declaration, modifiers, and scoping.
- **WIP** [Containers](./language/types/container.md) — Container data structures and navigation.
- **Stable** [Records](./language/types/record.md) — Record data types.
- **WIP** [Schemas](./language/types/schema.md) — Schema definition and validation.

### Intrinsics

- **WIP** [Calculate](./language/intrinsics/calculate.md) — Arithmetic expressions and operations.
- **WIP** [Decide](./language/intrinsics/decide.md) — Comparisons, boolean logic, and schema checks.
  [Flow](./language/intrinsics/flow.md) — Conditionals, loops, and control flow with context-based logic.
- **WIP** [Functions](./language/intrinsics/function.md) — Defining and using functions, parameters, and return values.
- **WIP** [Input](./language/intrinsics/input.md) — Script and function parameter handling.
- [Results](./language/intrinsics/results.md) — How nekonomicon handles script outcomes and result patterns.
- **WIP** [Wait](./language/intrinsics/wait.md) — Delays and timing control.

### Features

- [Clause](./language/features/clause.md) — Modifiers that change command behavior (safe, sensitive, elevated).
- [Invoke](./language/features/invoke.md) — Module importing and dependency management.
- [Modifiers](./language/features/modifiers.md) — Enhancing commands with options (with, without, on platform).
- **WIP** [Sinks](./language/features/sinks.md) — Output targets and variable assignment.

## Modules

### Data

- **WIP** [Container](./modules/data/container.md) — Container data structures and utilities.
- **WIP** [Enum](./modules/data/enum.md) — Enumeration types and operations.
- **WIP** [JSON](./modules/data/json.md) — JSON parsing and generation.
- **WIP** [List](./modules/data/list.md) — List operations and utilities.
- **WIP** [Map](./modules/data/map.md) — Map/dictionary data structures.
- **WIP** [Table](./modules/data/table.md) — Tabular data handling.

### I/O

- **WIP** [Archive](./modules/io/archive.md) — Archive and compression operations.
- **WIP** [Cabinet](./modules/io/cabinet.md) — File and folder management.
- **WIP** [Directory](./modules/io/directory.md) — Directory management and navigation.
- **WIP** [File](./modules/io/file.md) — File operations and utilities.
- **WIP** [Path](./modules/io/path.md) — Path manipulation and utilities.

### Text

- **WIP** [Bool](./modules/text/bool.md) — Boolean conversion and operations.
- **WIP** [Decimal](./modules/text/decimal.md) — Decimal number handling.
- **WIP** [Text](./modules/text/text.md) — Text manipulation and utilities.

### System

- **WIP** [Environment](./modules/system/environment.md) — Environment variable access and control.
- **WIP** [Program](./modules/system/program.md) — Program execution and management.
- **WIP** [Schedule](./modules/system/schedule.md) — Task scheduling and automation.
- **WIP** [System](./modules/system/system.md) — System-level operations and information.
- **WIP** [Watch](./modules/system/watch.md) — File and directory watching.

### Network

- **WIP** [HTTP](./modules/network/http.md) — HTTP requests and web communication.
- **WIP** [Network](./modules/network/network.md) — Networking tools and commands.

### Security

- **WIP** [Authentication](./modules/security/authentication.md) — Authentication mechanisms.
- **WIP** [Crypto](./modules/security/crypto.md) — Cryptographic operations.
- **WIP** [Vault](./modules/security/vault.md) — Secrets and sensitive data handling.

### Control

- **WIP** [Error](./modules/control/error.md) — Error handling and management.
- **WIP** [Resilience](./modules/control/resilience.md) — Retry logic and fault tolerance.
- **WIP** [Result](./modules/control/result.md) — Result types and pattern matching.
- **WIP** [Rule](./modules/control/rule.md) — Rule-based logic and automation.
- **WIP** [Test](./modules/control/test.md) — Testing utilities and assertions.

### Interaction

- **WIP** [Ask](./modules/interaction/ask.md) — User input prompts.
- **WIP** [Say](./modules/interaction/say.md) — Output and logging.

### Utility

- **WIP** [Binary](./modules/utility/binary.md) — Binary data operations.
- **WIP** [Explore](./modules/utility/explore.md) — Data exploration and querying.
- **WIP** [Filter](./modules/utility/filter.md) — Filtering data and results.
- **WIP** [Global](./modules/utility/global.md) — Global-level definition for nekonomicon's script.
- **WIP** [Math](./modules/utility/math.md) — Mathematical operations and utilities.
- **WIP** [Random](./modules/utility/random.md) — Random number and data generation.
- **WIP** [Schema](./modules/utility/schema.md) — Schema utilities.
- **WIP** [Script](./modules/utility/script.md) — Running native scripts and commands.
- **WIP** [Time](./modules/utility/time.md) — Time and date operations.
- **WIP** [Variable](./modules/utility/variable.md) — Variable manipulation utilities.
