---
title: Result Module
slug: result-module
category: module
status: stable
version: 0.0.1
since: 0.0.1
summary: Result type and error handling patterns.
tags: [result, error, success, fail]
---

# Result

# Result

## Description

The Result module provides result type patterns and error handling utilities for expressing success and failure states.

nekonomicon scripts must explicitly declare their outcome. This keeps scripts predictable and self-documenting.

- Use `success` to mark the script as completed successfully.
- Use `failure` to mark the script as failed.

> [!Tip]
>
> Result value: `success` and `failure` support any kind of result value, including scalars, text, containers, and container projections. See [data](./data-0.0.1.md) for more details.

## Summary Table

| Result  | Syntax Example            | Effect                                         | Notes                   |
| ------- | ------------------------- | ---------------------------------------------- | ----------------------- |
| success | `success 'Hello World!'`  | Stops execution and marks script as successful | Must be last signature  |
| failure | `failure ::error:message` | Stops execution and marks script as failed     | Can combine with others |

If a nekonomicon script or function reaches the end **without calling `success`**, it will automatically be marked as a failure. This enforces explicit intent and ensures that scripts never exit silently.

The `safe` modifier integrates with this pattern: any instructions of a command marked `safe` will not stop the script if it fails.

The result pattern is also applicable to [functions](./functions.md).

### `success {value}`

The `success` instruction marks the script or the function as successfully completed and stops execution while returning the content associated with the result.

- It can be called with no parameters to indicate success.
- It can return either a literal, a record or a container.
- If a nekonomicon script does not call `success` anywhere, it will automatically be marked as a failure when it reaches the end.

Use `success` to explicitly signal that the script or the function has finished and everything worked as expected.

**_Properties_**

- `value [ANY]`: Result content of the success.

**_Examples_**

```spell
~ Script.purr
~~~~~~~~~~~~~
global require modules 'bool' 'say'.

true into @is_enabled.

if @is_enabled
   success @is_enabled.
end

failure.
```

```bash
> [SUCC] The script has been successful, Result: 'true'.
```

---

### `failure {value}`

The `failure` instruction indicates the script or the function as failed either due to an error.

If the result is not defined at the end of a spell script or of a function, it's automatically assumed that it was a failure.

**_Properties_**

- `value [ANY]`: Result content of the failure.

**_Examples_**

```spell
~ Script.purr
~~~~~~~~~~~~~
global require modules 'say'.

say 'Hello World!'. ~ script will fails due to missing success.
```

```text
[INFO] Hello World!
[ERRO] The script has failed, no result defined.
```

```spell
~ Script.purr
~~~~~~~~~~~~~
global require modules 'say'.

say 'Hello World!'. ~ script will succeed because of the success call.
success.
```

```text
[INFO] Hello World!
[ERRO] The script has been successful.
```

## Related Pages

- [Data **v0.0.1**](../data-0.0.1.md)
- [Functions **v0.0.1**](../functions-0.0.1.md)
