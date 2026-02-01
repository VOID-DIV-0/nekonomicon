---
title: List Module
slug: list
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: List manipulation operations for ordered collections.
tags: [list, array, collection, data-structure]
---

# List

## Description

The List module provides operations for working with ordered collections of values stored in containers. Lists support adding, removing, and inspecting elements from both ends, making them suitable for stack and queue operations.

## Summary Table

| Instruction | Minimal Syntax | Effect | Key Modifiers |
| ----------- | -------------- | ------ | ------------- |
| append | list append ::L @value. | Add element to end of list | - |
| prepend | list prepend ::L @value. | Add element to beginning of list | - |
| pop | list pop ::L into @out. | Remove and return last element | - |
| shift | list shift ::L into @out. | Remove and return first element | - |
| peek | list peek ::L end into @out. | View last element without removal | end, front |
| length | list length ::L into @count. | Get number of elements | - |

## Anatomy

List commands follow this structure:

```
list <verb> <container> [<position>] [into <sink>].
```

- `<verb>`: The list operation (append, pop, peek, etc.)
- `<container>`: Target list container (::name)
- `[<position>]`: Optional position specifier (end, front)
- `[into <sink>]`: Target variable for result

## Syntax

```
list <operation> <container> [position] [into <target>].
```

## Examples

### 1. Creating and Appending to a List

```spell
[] into ::items.
list append ::items 'apple'.
list append ::items 'banana'.
list append ::items 'cherry'.

say ::items.  ~ Output: ['apple', 'banana', 'cherry']
```

### 2. Prepending Elements

```spell
[] into ::stack.
list prepend ::stack 'first'.
list prepend ::stack 'second'.

say ::stack.  ~ Output: ['second', 'first']
```

### 3. Removing Elements from End (Stack/Pop)

```spell
[] into ::stack.
list append ::stack 'a'.
list append ::stack 'b'.
list append ::stack 'c'.

list pop ::stack into @last.
say @last.  ~ Output: c
say ::stack.  ~ Output: ['a', 'b']
```

### 4. Removing Elements from Front (Queue/Shift)

```spell
[] into ::queue.
list append ::queue 'first'.
list append ::queue 'second'.
list append ::queue 'third'.

list shift ::queue into @first.
say @first.  ~ Output: first
say ::queue.  ~ Output: ['second', 'third']
```

### 5. Peeking at Elements

```spell
[] into ::items.
list append ::items 'one'.
list append ::items 'two'.
list append ::items 'three'.

list peek ::items end into @last.
say @last.  ~ Output: three

list peek ::items front into @first.
say @first.  ~ Output: one

say ::items.  ~ Output: ['one', 'two', 'three'] (unchanged)
```

### 6. Getting List Length

```spell
[] into ::items.
list append ::items 'a'.
list append ::items 'b'.
list append ::items 'c'.

list length ::items into @count.
say 'List has @{count} items'.  ~ Output: List has 3 items
```

### 7. Safe Operations on Empty Lists

```spell
[] into ::empty.

safe list pop ::empty into @value.  ~ Does not halt on error
safe list shift ::empty into @value.
safe list peek ::empty end into @value.
```

## Edge Cases

- `pop` and `shift` will cause an error if the list is empty (use `safe` clause to continue).
- `peek` operations return the element without modifying the list.
- `peek` on an empty list will also cause an error unless used with `safe`.
- Appending to or prepending to an empty list initializes the list.
- Lists maintain insertion order for all operations.

## Best Practices

- Use `safe` when operating on lists that might be empty to avoid script failure.
- Use `pop` and `append` for stack (LIFO) behavior.
- Use `shift` and `append` for queue (FIFO) behavior.
- Check list length before performing operations if emptiness is uncertain.
- Use `peek` when you need to inspect without modifying the list.
- Prefer explicit list initialization with `[]` for clarity.

## Common Errors

- **LIST-001**: Cannot pop from empty list
- **LIST-002**: Cannot shift from empty list
- **LIST-003**: Cannot peek at empty list

## Related Pages

- [Container](../core/container.md) — Container data structures
- [Variables](../core/variables.md) — Record and container variables
- [Map](./map.md) — Key-value collections
- [Filter](./filter.md) — Filtering and transforming collections
