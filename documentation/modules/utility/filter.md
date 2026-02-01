---
title: Filter Module
slug: filter
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Data filtering and transformation operations for collections.
tags: [filter, transform, collection, query]
---

# Filter

## Description

The Filter module provides operations for filtering, transforming, and querying collections of data. It enables powerful data manipulation patterns for containers and lists, supporting conditional filtering, mapping, and reduction operations.

## Summary Table

| Instruction | Minimal Syntax | Effect | Key Modifiers |
| ----------- | -------------- | ------ | ------------- |
| where | filter where ::collection with @condition into ::result. | Filter elements matching condition | with |
| map | filter map ::collection with @transform into ::result. | Transform each element | with |
| reduce | filter reduce ::collection with @operation into @result. | Reduce collection to single value | with, initial |
| select | filter select ::collection fields @field1 @field2 into ::result. | Select specific fields | fields |
| distinct | filter distinct ::collection into ::result. | Remove duplicate values | - |
| sort | filter sort ::collection by @field into ::result. | Sort collection | by, ascending, descending |
| limit | filter limit ::collection count @n into ::result. | Limit number of results | count |
| skip | filter skip ::collection count @n into ::result. | Skip first n elements | count |

## Anatomy

Filter commands follow this structure:

```
filter <verb> <container> [with <qualifier>] [modifiers] into <sink>.
```

- `<verb>`: The filter operation (where, map, reduce, etc.)
- `<container>`: Source data container (::name)
- `[with <qualifier>]`: Condition or transformation expression
- `[modifiers]`: Additional parameters (fields, count, by, etc.)
- `into <sink>`: Target variable for result

## Syntax

```
filter <operation> <container> [with <qualifier>] [modifiers] into <target>.
```

## Examples

### 1. Filtering with Conditions

```spell
[
  {name: 'Alice', age: '30'},
  {name: 'Bob', age: '25'},
  {name: 'Charlie', age: '35'}
] into ::people.

math compare ::people:age > '28' into @condition.
filter where ::people with @condition into ::adults.

say ::adults.  ~ Output: [{name: 'Alice', age: '30'}, {name: 'Charlie', age: '35'}]
```

### 2. Mapping Transformations

```spell
['apple', 'banana', 'cherry'] into ::fruits.

text upper @item into @uppercase.
filter map ::fruits with @uppercase into ::upper_fruits.

say ::upper_fruits.  ~ Output: ['APPLE', 'BANANA', 'CHERRY']
```

### 3. Reducing Collections

```spell
['1', '2', '3', '4', '5'] into ::numbers.

math add @acc @item into @sum.
filter reduce ::numbers with @sum initial '0' into @total.

say @total.  ~ Output: 15
```

### 4. Selecting Specific Fields

```spell
[
  {name: 'Alice', age: '30', city: 'New York'},
  {name: 'Bob', age: '25', city: 'London'}
] into ::people.

filter select ::people fields 'name' 'age' into ::names_ages.

say ::names_ages.  ~ Output: [{name: 'Alice', age: '30'}, {name: 'Bob', age: '25'}]
```

### 5. Removing Duplicates

```spell
['apple', 'banana', 'apple', 'cherry', 'banana'] into ::items.

filter distinct ::items into ::unique_items.

say ::unique_items.  ~ Output: ['apple', 'banana', 'cherry']
```

### 6. Sorting Collections

```spell
['3', '1', '4', '1', '5', '9', '2', '6'] into ::numbers.

filter sort ::numbers ascending into ::sorted.

say ::sorted.  ~ Output: ['1', '1', '2', '3', '4', '5', '6', '9']
```

### 7. Limiting and Skipping Results

```spell
['a', 'b', 'c', 'd', 'e', 'f'] into ::letters.

filter limit ::letters count '3' into ::first_three.
say ::first_three.  ~ Output: ['a', 'b', 'c']

filter skip ::letters count '2' into ::after_two.
say ::after_two.  ~ Output: ['c', 'd', 'e', 'f']
```

### 8. Chaining Filter Operations

```spell
[
  {name: 'Alice', age: '30'},
  {name: 'Bob', age: '25'},
  {name: 'Charlie', age: '35'},
  {name: 'David', age: '40'}
] into ::people.

math compare ::people:age > '28' into @age_condition.
filter where ::people with @age_condition into ::filtered.
filter sort ::filtered by 'age' descending into ::sorted.
filter limit ::sorted count '2' into ::result.

say ::result.  ~ Output: [{name: 'David', age: '40'}, {name: 'Charlie', age: '35'}]
```

## Edge Cases

- Filtering an empty collection returns an empty collection.
- `reduce` requires an initial value for empty collections.
- `sort` on non-comparable types may produce undefined behavior.
- `distinct` uses value equality for comparison.
- `skip` with count greater than collection size returns empty collection.
- `limit` with count of 0 returns empty collection.

## Best Practices

- Use `where` for conditional filtering, not manual loops.
- Chain filter operations for complex queries instead of nested loops.
- Provide meaningful initial values for `reduce` operations.
- Use `safe` when filtering user-provided or external data.
- Consider performance for large collections—filter early, limit late.
- Use `distinct` before other operations to reduce processing overhead.

## Common Errors

- **FILTER-001**: Invalid filter condition
- **FILTER-002**: Missing initial value for reduce
- **FILTER-003**: Invalid field reference in select

## Related Pages

- [Container](../core/container.md) — Container data structures
- [List](./list.md) — List operations
- [Map](./map.md) — Key-value collections
- [Math](./math.md) — Mathematical comparisons
- [Bool](./bool.md) — Boolean operations
