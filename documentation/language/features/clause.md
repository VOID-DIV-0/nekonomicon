---
version: 0.3.0
title: clause
status: stable
---

# Clause

## Description

Clauses are optional instructions placed at the start of a command. They alter the behavior of the targeted command until the terminator. Clauses let you control error handling, parallelism, security, and permissions for each command in a simple, readable way.

## Summary Table

| Clause        | Minimal Syntax           | Effect                                              | When to Use                                           |
| ------------- | ------------------------ | --------------------------------------------------- | ----------------------------------------------------- |
| safe          | safe <command>.          | Continue on error, do not halt flow                 | Cross-platform scripts, optional operations           |
| async         | async <command>.         | Run command in parallel                             | I/O-bound tasks, independent operations (can tag)     |
| sensitive     | sensitive <command>.     | Allow/taint variables as sensitive          | Working with vault data, passwords, secrets           |
| <!> sensitive | <!> sensitive <command>. | Allow use of sensitive variables for egress context | Controlled logging/output of sensitive data (unmasked) |
| elevated      | elevated <command>.      | Run with admin rights                               | System modifications, privileged operations           |

## Anatomy

There is no required order when using multiple clauses. However, for readability, it is recommended to keep a consistent order. The effect of each clause ends at the command terminator.

The recommended order is:

1. sensitive / <!> sensitive
2. safe
3. async
4. elevated

### Combining Clauses

Clauses can be combined in a single command:

```spell
sensitive safe async elevated cabinet read file '/secure/data.txt' into @sensitive_content.
```

Common combinations:

- `safe async` — Non-critical parallel tasks
- `sensitive safe` — Optional sensitive data manipulation
- `elevated safe` — Privileged operations that may not be available on all platforms
- `<!> sensitive elevated` — Secure privileged egress operations

## Clause `safe`

Safe clause allows a command to fail without halting the entire script. Instead of stopping execution, the command logs a warning, prints the failure reason, and continues with the next instruction.

### Example Safe Clause (Cross-platform battery check)

Here's an example of using the `safe` clause to check battery status across different operating systems without halting the script if a command fails:

```spell
2 into @!count. ~ sealed record.

~ While loop to check battery status every 5 seconds.
repeat @!count
  safe script 'pmset -g batt' on mac.
  safe script 'WMIC Path Win32_Battery Get EstimatedChargeRemaining' on windows.
  safe script 'acpi -b' on linux.
  wait '5' seconds.
end

success.
```

The result would be something like this on a Mac:

```spell
[WARN][FAIL] Command 'WMIC Path Win32_Battery Get EstimatedChargeRemaining' must be run on Windows.
[WARN][FAIL] Command 'acpi -b' must be run on Linux.
Now checking battery status...
Battery status: 85% (on battery)
[WARN][FAIL] Command 'WMIC Path Win32_Battery Get EstimatedChargeRemaining' must be run on Windows.
[WARN][FAIL] Command 'acpi -b' must be run on Linux.
Now checking battery status...
Battery status: 85% (on battery)
[SUCC] Script completed successfully.
```

## Clause `async`

The `async` clause allows a command to run asynchronously in the background, enabling parallel execution of multiple tasks. This is particularly useful for I/O-bound operations or tasks that can be performed independently without waiting for each other to complete.

When a command is marked with `async`, it starts executing immediately, and the script continues to the next instruction without waiting for the async command to finish. You can use the `wait` command later in the script to synchronize and ensure that all async tasks have completed before proceeding.

### Async Clause

Here is an example of using the `async` clause to run two long-running tasks in parallel, the wait command is used to wait all currently running async tasks to complete before proceeding:

```spell
async 'task1' script 'long_running_task_1.sh'.
async 'task2' script 'long_running_task_2.sh'.
say 'Both tasks started asynchronously.'.
wait. ~ Waits for all async tasks to finish
success.
```

Here's an example of using the `async` clause to run commands in parallel. This example demonstrates fetching data from two different URLs simultaneously:

```spell
function fn_world
  wait 5 seconds.
  say 'world'.
end

async 'world' fn_world. ~ Tagged async task
say 'hello'.
wait 'world'. ~ Wait for tagged task to complete

success.
```

This would result with hello being printed first, followed by world after a 5-second delay, even if fn_world is started before say 'hello'.

### Async with wait all/any

It's possible to wait for a specific set of async tasks to complete using `wait all` or `wait any`:

`wait all` waits for all specified async tasks to complete before proceeding.

`wait any` waits for the first specified async task to complete before proceeding.

Here are examples of both:

```spell
invoke cabinet.

async 'file1' cabinet read file 'my_file1.txt'.
async 'file2' cabinet read file 'my_file2.txt'.
wait all 'file1' 'file2'.
success.
```

```spell
invoke cabinet.

async 'file1' cabinet read file 'my_file1.txt'.
async 'file2' cabinet read file 'my_file2.txt'.
wait any 'file1' 'file2'.
stop
success.
```

## Clause `sensitive` and `<!> sensitive`

### What is egress context?

Egress context refers to any situation where sensitive data could potentially leave the secure environment of the nekonomicon script and become observable or accessible outside of it. This includes, but is not limited to:

- Outputting to the console (e.g., printing, logging)
- Writing to files (including temp files, logs, or exports)
- Sending over the network (HTTP requests, sockets, etc.)
- Injecting into or calling other modules (if the module is not marked as sensitive-aware)
- Passing to external scripts or programs (e.g., script, exec)
- Storing in environment variables or other process-wide state
- Any other action that makes data observable outside the current secure context

Egress-friendly commands are explicitly designed to handle sensitive data without risking exposure. These commands have built-in safeguards to ensure that sensitive information is never logged, displayed, or transmitted insecurely. In nekonomicon, a command is only considered egress-friendly if it is clearly marked as sensitive-aware in its documentation.
By default, all commands are treated as egress-hostile, which is unsafe for handling sensitive data in an egress context unless they are explicitly documented as egress-friendly.

#### Why use the sensitive clause?

The `sensitive` clause is used to explicitly mark commands that handle sensitive data, allowing the use of vaulted variables and ensuring that such data is treated with the necessary security precautions. It helps prevent accidental exposure of sensitive information by enforcing restrictions on how and where this data can be used.

```spell
invoke text.

sensitive ask 'What is my password?' with mask into @!ephemeral_pw.

~ No egress context here, so safe to use sensitive variable
sensitive text length @!ephemeral_pw into @!length.

~ This is marked due to outputting sensitive data.
<!> sensitive say 'You entered a password of length @!length characters.'.
success.
```

```spell
sensitive vault lock 'my_password' with 'Password123$'.

sensitive vault unlock 'my_password' into @!retrieved_password.

<!> sensitive say 'Retrieved password from vault: ' @!retrieved_password.

success.
```

## Clause `elevated`

The `elevated` clause serves as an explicit marker and validator for commands that require administrative or elevated privileges. It documents privilege requirements in the script and provides clear, fail-fast error handling.

### How it works

When nekonomicon encounters an `elevated` command:

1. It checks if the script is running with elevated privileges (admin/root/sudo).
2. If privileges are insufficient, it fails immediately with a clear error message.
3. If privileges are sufficient, the command executes normally.

### Purpose

- **Documentation**: Makes privilege requirements explicit and visible in the script.
- **Fail-fast**: Provides helpful error messages upfront instead of cryptic OS-level "Permission denied" errors.
- **Auditable**: Easy to identify which commands require admin rights during code review.
- **Safety**: Encourages running only necessary commands with elevated privileges.

### Platform behavior

- **Linux/macOS**: Script must be run with `sudo` or as root.
- **Windows**: Script must be run in an elevated Command Prompt or PowerShell session (Run as Administrator).

### Examples

```spell
~ Install system packages with elevated permissions
elevated script 'apt update && apt install curl -y' on linux.
elevated script 'choco install curl -y' on windows.

~ Create system directory
elevated cabinet create folder '/opt/myapp' on linux.
elevated cabinet create folder 'C:\Program Files\MyApp' on windows.

success.
```

If the script is not running with elevated privileges, it will fail with:

```
[ERR ] Command requires elevated privileges. Please run this script with sudo or as administrator.
```

---

## Platform Considerations

- **`safe` with `on <os>`**: Failures on non-matching platforms are automatically ignored without needing `safe`
  ```spell
  safe script 'pmset -g batt' on mac.  ~ Safe + platform filter
  ```
- **`async` limitations**: Some modules may not support async execution. Check module documentation.
- **`elevated` prompts**: May trigger UAC/sudo prompts depending on OS configuration.
- **`sensitive` scope**: Sensitive taint propagates through variables and must be explicitly allowed in egress contexts.

## Best Practices

✅ **DO:**

- Use `safe` for cross-platform commands that may not be available everywhere
- Apply `sensitive` whenever working with vault variables or user input that may contain secrets
- Combine `async` with `wait` for controlled parallelism
- Document why `elevated` is required using comments
- Use consistent clause ordering for readability

❌ **DON'T:**

- Forget to handle async task results with `wait`
- Use `elevated` for commands that don't truly need admin rights

## Related Pages

- [Signatures](./signatures.md) — Command modifiers like `trace`, `timeout`, `on <os>`
- [Vault](../modules/vault-0.0.1.md) — Secure storage requiring `sensitive` clause
- [Wait](./wait.md) — Synchronization for async operations
- [Function](./function.md) — Applying clauses to function calls
