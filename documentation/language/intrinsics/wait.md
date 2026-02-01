---
title: Wait
slug: wait
category: core
status: wip
version: 0.0.1
since: 0.0.1
summary: Synchronization and delay operations for async tasks.
tags: [wait, async, synchronization, delay]
---

# Wait

## Description

The Wait instruction provides synchronization and delay capabilities for nekonomicon scripts. It allows pausing execution for a specified time or blocking until async tasks complete.

## Summary Table

| Instruction | Syntax | Effect | Notes |
| ----------- | ------ | ------ | ----- |
| wait time | wait @time @unit. | Pause current script thread | Units: ms, seconds, minutes, hours |
| wait tag | wait @tag. | Block until all tasks in group finish | Success or failure |
| wait any | wait any @tag1 @tag2. | Return when one group has task finish | First completion |
| wait all | wait all @tag1 @tag2. | Block until all listed groups finish | All completions |
| wait all | wait all. | Block until all known async groups finish | Global wait |

## Syntax

```
wait <time> <unit>.
wait <tag>.
wait any <tag1> <tag2> ....
wait all <tag1> <tag2> ....
wait all.
```

## Examples

### 1. Simple Time Delay

```spell
say 'Starting process...'.
wait 5 seconds.
say 'Process complete!'.
```

### 2. Wait for Tagged Async Task

```spell
async 'upload' http post 'https://api' body ::payload.
say 'launched…'.
wait 'upload'.
say 'done'.
```

### 3. Wait for Multiple Tasks with Same Tag

```spell
repeat '5'
  async 'upload' http post 'https://api' body ::payload.
end
wait 'upload'.
```

### 4. Wait for First Completion (Race)

```spell
async 'mirror' http get 'https://fast.example.com/file'.
async 'mirror' http get 'https://backup.example.com/file'.
wait any 'mirror'.
stop 'mirror'.   ~ cancel remaining mirror requests
```

### 5. Wait for All Async Groups

```spell
async 'prep' script 'prepare.sh'.
async 'ship' script 'ship.sh'.
wait all.        ~ waits both 'prep' and 'ship'
```

## Edge Cases

- `wait` with time will pause the current thread for the specified duration.
- `wait` with a tag blocks until all tasks with that tag complete.
- `wait any` returns as soon as the first task in any specified group completes.
- `wait all` blocks until all tasks in all specified groups complete.
- If no async tasks exist, `wait all` returns immediately.

## Best Practices

- Use meaningful tag names for async tasks to make wait operations clear.
- Use `wait any` for race conditions where you need the fastest response.
- Use `wait all` to ensure all parallel operations complete before proceeding.
- Consider timeouts for long-running async operations.

## Related Pages

- [Clause](./clause.md) — Async clause for parallel execution
- [Signatures](./signatures.md) — Timeout signature
