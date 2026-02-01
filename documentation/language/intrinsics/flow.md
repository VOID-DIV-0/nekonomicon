---
title: Flow Control
slug: flow
category: core
status: stable
version: 0.1.0
since: 0.0.1
summary: Conditional logic and loops for script control flow.
tags:
  [flow, conditionals, loops, if, any, all, exclusive, decide, while, repeat]
---

# Flow

nekonomicon provides clear, context-based flow control that prioritizes readability. Conditions are expressed using explicit keywords that make the intent obvious.

---

## Summary Table

### Conditional

| Instruction  | Syntax Example                                     | Effect                                | Notes                        |
| ------------ | -------------------------------------------------- | ------------------------------------- | ---------------------------- |
| if           | `if @condition`                                    | True if single variable is true       | Simple single variable check |
| if any       | `if any @flag1 @flag2 @flag3`                      | True if at least one variable is true | Requires 2+ variables        |
| if all       | `if all @check1 @check2 @check3`                   | True if all variables are true        | Requires 2+ variables        |
| if none      | `if none @error1 @error2 @error3`                  | True if all variables are false       | Requires 2+ variables        |
| if exclusive | `if exclusive @dev @prod`                          | True if exactly one variable is true  | Requires 2+ variables        |
| if decide    | `if decide @score > 85 and @status equals 'ready'` | True if complex expression is true    | Full expression support      |

### Loops

| Instruction | Syntax Example                        | Effect                         | Notes                      |
| ----------- | ------------------------------------- | ------------------------------ | -------------------------- |
| while       | `while @condition`                    | Repeat while condition is true | Supports conditional logic |
| while       | `while all @c1 @c2 @c3`               | Repeat while all true          | Supports conditional logic |
| repeat      | `repeat @count`                       | Repeat fixed number of times   | Structural iteration only  |
| increase    | `increase @i by '2' to '10'`          | Increment and repeat           | Structural iteration only  |
| decrease    | `decrease @i by '3' to '0'`           | Decrement and repeat           | Structural iteration only  |
| foreach     | `foreach from ::container into @item` | Iterate over container         | Structural iteration only  |

---

## Conditionals

nekonomicon provides six types of conditional logic, each with clear intent:

### `if` - Simple Check

Check if a single variable is true. The most basic conditional.

```spell
decide @user_authenticated into @is_logged_in.

if @is_logged_in
  say 'Welcome back!'.
else
  say 'Please log in'.
end
```

### `if any` - OR Logic

Check if **at least one** variable is true. Requires at least 2 variables.

```spell
# Pre-compute your checks
decide @score > 85 into @high_score.
decide @attendance >= 0.9 into @good_attendance.

if any @high_score @good_attendance @extra_credit
  say 'Student qualifies for honors'.
end
```

### `if none` - None True (NOR Logic)

Check if **all** variables are false. Requires at least 2 variables.

```spell
# Pre-compute your error checks
decide @network_available into @network_ok.
decide @disk_space > 1000 into @disk_ok.
decide @memory_usage < 80 into @memory_ok.

if none @validation_error @network_error @auth_error
  say 'No errors detected, proceeding'.
else
  error 'System checks failed'.
end
```

### `if all` - AND Logic

Check if **all** variables are true. Requires at least 2 variables.

````spell
# Pre-compute your checks
vault check access 'database' into @db_access.
decide @user_level equals 'admin' into @is_admin.

if all @authenticated @db_access @is_admin
  say 'Full database access granted'.
end
```### `if exclusive` - XOR Logic

Check if **exactly one** variable is true. Requires at least 2 variables.

```spell
if exclusive @is_development @is_production
  say 'Valid environment configuration'.
else
  error 'Cannot be both dev and prod, or neither'.
end
````

### `if decide` - Complex Expressions

For complex conditions that need full expression evaluation.

```spell
if decide @score > 85 and @attendance >= 0.9 and @status equals 'active'
  say 'Complex condition met'.
end
```

## Loops

### `repeat`

Repeat a block a fixed number of times. The count must be a variable or literal.

```spell
'5' into @count.
repeat @count
  say 'Iteration in progress'.
end
```

### `while`

Repeat a block while a condition is true. Supports the same conditional logic as `if` statements.

#### Simple Condition

```spell
'10' into @counter.
decide @counter > 0 into @should_continue.

while @should_continue
  say 'Counter is @{counter}'.
  calculate @counter - 1 into @counter.
  decide @counter > 0 into @should_continue.
end
```

#### Multi-Variable Conditions

```spell
# Continue while all systems are operational
while all @network_ok @database_ok @cache_ok
  # Process requests
  say 'All systems operational'.
  # Re-check system status
  check_network_status into @network_ok.
  check_database_status into @database_ok.
  check_cache_status into @cache_ok.
end

# Continue while any work remains
while any @pending_uploads @pending_downloads @pending_processing
  say 'Work in progress'.
  process_next_item.
  check_work_status into @pending_uploads @pending_downloads @pending_processing.
end
```

### `increase` and `decrease`

Increment or decrement a value in steps, executing the block each iteration.

```spell
increase @i by '2' to '10'
  say 'Current value: @{i}'.
end

decrease @countdown by '1' to '0'
  say '@{countdown} seconds remaining'.
end
```

### `foreach`

Iterate over containers, binding each element to a variable.

```spell
foreach from ::names into @name
  say 'Hello, @{name}!'.
end
```

## Control Flow Rules

### Conditional Logic

1. **Simple conditions** use `any`, `all`, `none`, or `exclusive` with variables only
2. **Multi-variable conditions** (`any`, `all`, `none`, `exclusive`) require at least 2 variables
3. **Single variable checks** use simple `if @variable` or `while @variable`
4. **Complex conditions** require `decide` for full expression evaluation
5. **Conditional support** available in `if` statements and `while` loops

### Structural Iteration

6. **Structural loops** (`repeat`, `increase`, `decrease`, `foreach`) use fixed parameters only
7. **No conditional logic** in structural loops - they follow predetermined iteration patterns
8. **All blocks** must be closed with `end`
9. **Variables** must be pre-computed for conditional logic

## Examples

### Authentication Flow

```spell
vault check token @user_token into @valid_token.
decide @user_role equals 'admin' into @is_admin.
decide @session_time < 3600 into @session_valid.

if all @valid_token @is_admin @session_valid
  say 'Admin access granted'.
else
  error 'Access denied'.
end
```

### Environment Check

```spell
if exclusive @development @staging @production
  say 'Valid environment configuration'.
else
  error 'Invalid environment state'.
end
```

### Complex Business Logic

```spell
if decide @order_total > 100 and @customer_tier equals 'premium' and @stock_available >= @quantity
  say 'Order approved with premium processing'.
else
  say 'Order requires manual review'.
end
```
