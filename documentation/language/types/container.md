---
title: Container
slug: container
category: core
status: stable
version: 0.2.0
since: 0.0.1
summary: Structured data containers for complex data types including arrays, maps, and nested structures.
tags: [container, data-structure, array, map, nested, projection]
---

# Container

## Description

Containers are nekonomicon's structured data type, identified by the `::` symbol. They allow you to organize complex data into hierarchies with named fields, arrays, and nested structures—like JSON objects or dictionaries in other languages, but with nekonomicon's explicit, readable syntax.

Use containers when you need to:

- **Group related data** (e.g., user profile with name, email, age)
- **Store collections** (e.g., list of colors, array of scores)
- **Build nested structures** (e.g., configuration with multiple sections)
- **Pass complex data** to functions or modules

Containers complement records (`@variable`): use records for single values, containers for structured data.

---

## Summary Table

| Pattern              | Syntax                        | Effect                            | Notes                            |
| -------------------- | ----------------------------- | --------------------------------- | -------------------------------- |
| Container Definition | `container ... into ::name.`  | Create structured data            | Contextual parsing for structure |
| Field Assignment     | `:field value`                | Assign single value to field      | Direct field-value assignment    |
| Array Assignment     | `:field value1 value2 value3` | Assign multiple values as array   | Multiple values = array          |
| Nested Container     | `:field :subfield value`      | Create nested container           | Projection after projection      |
| Field Access         | `::container:field`           | Access named field                | Navigate with `:` separator      |
| Index Access         | `::container#0`               | Access array element by position  | Zero-indexed                     |
| Safe Field Access    | `::container:?field`          | Safe access to optional field     | Returns null if missing          |
| Safe Index Access    | `::container#?0`              | Safe access to array element      | Returns null if out of bounds    |
| Scalar as Array      | `::scalar_field#0`            | Treat scalar as single-item array | Future-proof field evolution     |
| Chained Access       | `::users:profile:email`       | Navigate nested structure         | Chain projections                |
| Scalar as Array      | `::scalar_field#0`            | Treat scalar as single-item array | Future-proof field evolution     |
| Chained Access       | `::users:profile:email`       | Navigate nested structure         | Chain projections                |
| Mixed Access         | `::data:colors#1`             | Access array within field         | Combine field and index          |
| Sealed Container     | `::!const`                    | Immutable container               | All fields inherit seal          |
| Nullable Container   | `::?maybe`                    | Container that can be null        | Per-node nullability             |
| Nullable Field       | `::user:?optional_field`      | Field that might not exist        | Does not chain to parent         |

---

## Container Identifiers

### Basic Identifier

A container identifier starts with `::` followed by an alphanumeric name with underscores allowed:

```spell
::user
::config_data
::api_response
```

### Nullable Containers (`?`)

Prefix with `?` to allow null values. Nullable containers can be checked before use:

```spell
::?optional_config   ~ Can be null
::?maybe_user        ~ Can be null
```

**Important:** Nullability does **not** chain transitively. Each field declares its own nullability:

```spell
::?container:field           ~ Container nullable, field NOT nullable
::?container:?field          ~ Both nullable independently
::container:?field           ~ Container required, field nullable
```

### Sealed Containers (`!`)

Prefix with `!` to make the container constant (immutable). Once defined, neither the container nor any of its fields can be reassigned:

```spell
::!constants    ~ Entire structure is immutable
```

**Important:** Sealing **does** chain transitively. All child fields inherit the seal:

```spell
::!database:host             ~ host is sealed (inherited)
::!database:port             ~ port is sealed (inherited)
::!config:api:key            ~ All levels sealed
```

**Error:** Cannot re-seal already sealed fields:

```spell
container
  :!field 'value'      ~ ❌ E-CTNR-SEAL-INHERITED
into ::!parent.              ~ Already sealed at root
```

---

## Anatomy

### Creating Containers

Use the `container` block to define structured data. The structure is determined contextually:

```spell
container
  :field1 'value1'                    ~ Single value = scalar field
  :colors 'red' 'blue' 'green'        ~ Multiple values = array field
  :contact                            ~ Nested container
    :name 'John Doe'
    :email 'john@example.com'
into ::my_container.
```

### Container Node Types

Containers support three types of child nodes determined by context:

1. **Value nodes** — Single value after projection creates scalar field
2. **Array nodes** — Multiple values after projection creates array
3. **Container nodes** — Projection followed by more projections creates nested structure

```spell
container
  :name 'Alex'                             ~ Value node (single value)
  :colors 'red' 'blue' 'green'             ~ Array node (multiple values)
  :profile                                 ~ Nested container node (projections follow)
    :email 'admin@example.com'
    :verified true
into ::user.
```

---

## Projections: Accessing Container Data

Projections are the syntax for navigating into container structures. There are two types:

### Field Projection (`:field`)

Access named fields using `:` followed by the field name:

```spell
::user:name                  ~ Access 'name' field
::config:database:host       ~ Navigate nested fields
```

### Index Projection (`#n`)

Access array elements by zero-based index using `#` followed by the position:

```spell
::colors#0                   ~ First element
::scores#5                   ~ Sixth element
```

### Chaining Projections

Combine field and index projections to navigate complex structures:

```spell
::user:tags#0                     ~ First tag of user
::data:users#2:email              ~ Email of third user
::config:servers#0:hostname       ~ Hostname of first server
::matrix:rows#1:columns#3         ~ Element at row 1, column 3
```

### Projection Anatomy

```
::user:profile:email
^^ ^^^^ ^^^^^^^ ^^^^^
|   |      |      └─── Field projection
|   |      └────────── Field projection
|   └───────────────── Identifier
└───────────────────── Sigil

::colors#0#1
^^ ^^^^^^ ^ ^
|    |    | └─ Index projection
|    |    └─── Index projection
|    └──────── Identifier
└───────────── Sigil

::!config:api:?key
^^^ ^^^^^^ ^^^ ^^^^
 |    |     |   └─── Field projection (nullable)
 |    |     └─────── Field projection
 |    └───────────── Identifier
 └────────────────── Sigil (sealed)
```

---

## Safe Access Patterns

nekonomicon provides safe access operators for defensive programming and future-proof code.

### Safe Field Access (`?field`)

Use `?` prefix to safely access fields that might not exist:

```spell
container
  :name 'Alice'
  :email 'alice@example.com'
into ::user.

say ::user:name.        ~ ✅ 'Alice' (field exists)
say ::user:phone.       ~ ❌ ERROR: Field not found
say ::user:?phone.      ~ ✅ null (safe access)
```

### Safe Index Access (`#?index`)

Use `#?` to safely access array elements that might be out of bounds:

```spell
container
  :tags 'red' 'blue'    ~ Array with 2 elements
into ::data.

say ::data:tags#0.      ~ ✅ 'red' (valid index)
say ::data:tags#5.      ~ ❌ ERROR: Index out of bounds
say ::data:tags#?5.     ~ ✅ null (safe access)
```

### Chained Safe Access

Combine safe operators for maximum defensive programming:

```spell
~ Triple-safe: field might not exist, index might be out of bounds, nested field might not exist
say ::data:?optional_array#?3:?name.
```

### Scalar-to-Array Compatibility

Access scalar fields with array syntax for future-proof code:

```spell
~ Today: scalar field
container
  :tag 'red'
into ::data.

say ::data:tag#0.       ~ ✅ 'red' (treats scalar as 1-element array)

~ Tomorrow: expand to array
'blue' 'green' into ::data:tag.

say ::data:tag#0.       ~ ✅ 'red' (same code still works!)
```

---

## Examples

### 1. Simple Container

```spell
container
  :name 'Alice'
  :email 'alice@example.com'
  :age 30
into ::user.

say ::user:name.             ~ Outputs: Alice
say ::user:email.            ~ Outputs: alice@example.com
say ::user:age.              ~ Outputs: 30
```

### 2. Container with Arrays

```spell
container
  :fruits 'apple' 'banana' 'cherry'        ~ Multiple values = array
  :scores 10 20 30 40 50                   ~ Multiple values = array
into ::data.

say ::data:fruits#0.         ~ Outputs: apple
say ::data:fruits#2.         ~ Outputs: cherry
say ::data:scores#4.         ~ Outputs: 50
```

### 3. Nested Containers

```spell
container
  :name 'MyApp'
  :database                               ~ Projection followed by projections = nested container
    :host 'localhost'
    :port '5432'
  :logging                               ~ Another nested container
    :level 'info'
    :enabled true
into ::config.

say ::config:name.                    ~ Outputs: MyApp
say ::config:database:host.           ~ Outputs: localhost
say ::config:logging:enabled.         ~ Outputs: true
```

### 4. Sealed (Immutable) Containers

```spell
container
  :host 'localhost'
  :port '5432'
  :database 'postgres'
into ::!db_config.

~ ✅ Reading is allowed
say ::!db_config:host.

~ ❌ Reassignment is forbidden
'remote-host' into ::!db_config:host.  ~ ERROR: E-CTNR-SEALED
```

### 5. Nullable Containers

```spell
null into ::?optional_data.

decide ::?optional_data is null into @is_null.

if @is_null
  say 'No data available'.
else
  say ::?optional_data:value.
end
```

### 6. Nullable Fields

```spell
container
  :name 'John Doe'
  :email 'john@example.com'
  :?phone null                      ~ Optional field (nullable)
into ::contact.

decide ::contact:?phone is null into @no_phone.

if @no_phone
  say 'No phone number provided'.
else
  say 'Phone: @{::contact:?phone}'.
end
```

### 7. Mixed Nullable and Sealed

```spell
container
  'ProductionDB' into :name
  'db.example.com' into :host
  null into :?backup_host           ~ Optional field
into ::!database.

~ ✅ Reading sealed fields
say ::!database:name.
say ::!database:host.

~ ✅ Checking nullable field (sealed but nullable)
decide ::!database:?backup_host is null into @no_backup.

~ ❌ Cannot reassign sealed fields
'new-host' into ::!database:host.  ~ ERROR: E-CTNR-SEALED
```

### 8. Arrays of Containers

```spell
container
  container
    'Alice' into :name
    25 into :age
  into :user1
  container
    'Bob' into :name
    30 into :age
  into :user2
into ::users.

say ::users:user1:name.      ~ Outputs: Alice
say ::users:user2:age.       ~ Outputs: 30
```

### 9. Complex Nested Structure

```spell
container
  :title 'API Config'
  :api
    :base_url 'https://api.example.com'
    :version 'v2'
    :auth
      :type 'Bearer'
      :?token null
  :logging
    :levels 'error' 'warning' 'info'    ~ Multiple values = array
    :file '/var/log/app.log'
into ::app_config.

~ Navigate deeply nested structures
say ::app_config:api:base_url.           ~ https://api.example.com
say ::app_config:api:auth:type.          ~ Bearer
say ::app_config:logging:levels#0.       ~ error
```

### 10. Building Containers from User Input

```spell
input 'Enter your name: ' into @name.
input 'Enter your email: ' into @email.
input 'Enter your age: ' into @age.

container
  :name @name
  :email @email
  :age @age
  :status 'active'
into ::new_user.

say 'Created user: @{::new_user:name}'.
say 'Status: @{::new_user:status}'.
```

### 11. Modifying Container Fields

```spell
container
  :status 'draft'
  :views 0
into ::document.

say ::document:status.       ~ Outputs: draft

'published' into ::document:status.
1000 into ::document:views.

say ::document:status.       ~ Outputs: published
say ::document:views.        ~ Outputs: 1000
```

### 12. Container with Multiple Array Levels

```spell
container
  :row_0 1 2 3                        ~ Multiple values = array
  :row_1 4 5 6                        ~ Multiple values = array
  :row_2 7 8 9                        ~ Multiple values = array
into ::matrix.

say ::matrix:row_0#0.    ~ 1
say ::matrix:row_1#2.    ~ 6
say ::matrix:row_2#1.    ~ 8
```

### 13. Safe Access Patterns

```spell
container
  :name 'Alice'
  :tags 'developer' 'admin'
  :profile
    :verified true
into ::user.

~ Safe field access
say ::user:?phone.                    ~ null (field doesn't exist)
say ::user:?name.                     ~ 'Alice' (field exists)

~ Safe index access
say ::user:tags#?5.                   ~ null (out of bounds)
say ::user:tags#?1.                   ~ 'admin' (valid index)

~ Chained safe access
say ::user:?settings:?theme.          ~ null (settings field doesn't exist)
say ::user:?profile:?verified.        ~ true (both fields exist)
```

### 14. Field-to-Array Evolution

```spell
~ Start with scalar
container
  :status 'active'
into ::user.

~ Code written for future arrays
say ::user:status#0.                  ~ 'active' (scalar treated as array)

~ Later: expand to array
'active' 'verified' 'premium' into ::user:status.

~ Same code still works!
say ::user:status#0.                  ~ 'active' (first element of array)
say ::user:status#2.                  ~ 'premium' (third element)
```

### 15. Defensive Container Access

```spell
~ Accessing dynamic/external data safely
container
  :api_response
    :status 'success'
    :data 'item1' 'item2'
into ::response.

~ Defensive programming
decide ::response:?api_response:?status equals 'success' into @success.
decide ::response:?api_response:?data#?0 is not null into @has_data.

if @success and @has_data
  say 'First item: ' ::response:api_response:data#0.
else
  say 'No data available or API error.'.
end
```

---

## Best Practices

1. **Use descriptive field names:** Choose clear, meaningful names like `:user_email` over `:e`.

2. **Use safe access for uncertain data:** When accessing external data or optional fields, use `?` and `#?`:

   ```spell
   ~ Unsafe
   say ::api_data:result:items#0.

   ~ Safe
   say ::api_data:?result:?items#?0.
   ```

3. **Future-proof with array syntax:** Even for scalar fields, consider using `#0` if the field might become an array:

   ```spell
   say ::user:tag#0.     ~ Works for both scalar and array
   ```

4. **Keep structures flat when possible:** Deeply nested containers can be hard to read. Consider breaking into multiple containers.

5. **Seal configuration data:** Use `::!` for data that shouldn't change during script execution.

6. **Check nullable fields before access:** Always use `decide` to check if nullable fields exist:

   ```spell
   decide ::user:?phone is not null into @has_phone.
   if @has_phone
     say ::user:?phone.
   end
   ```

7. **Use arrays for ordered collections:** When the order matters or you need indexed access, use arrays. For named data, use fields.

8. **Document complex structures:** Add comments explaining nested structures:

   ```spell
   container
     :section          ~ Purpose of section
       :field1 'value' ~ Purpose of field1
       :field2 'value' ~ Purpose of field2
   into ::config.
   ```

9. **Avoid over-nesting:** More than 3-4 levels deep may indicate your structure needs refactoring.

10. **Use consistent naming:** Pick a style (snake_case recommended) and stick with it across your containers.

11. **Consider immutability early:** If data won't change, seal it at creation with `::!` rather than trying to protect it later.

12. **Validate complex structures:** Use schemas (see [Schema](./schema.md)) to validate container structure and types.

---

## Edge Cases

### Accessing Non-Existent Fields

Accessing a field that doesn't exist causes an error:

```spell
container
  'value' into :field1
into ::data.

say ::data:field2.           ~ ERROR: E-CTNR-FIELD-NOT-FOUND
```

### Index Out of Bounds

Accessing an array index beyond its length causes an error:

```spell
container
  array 'a' 'b' 'c' into :items
into ::data.

say ::data:items#0.          ~ ✅ Outputs: a
say ::data:items#5.          ~ ❌ ERROR: E-CTNR-INDEX-OUT-OF-BOUNDS
```

### Null Container Access

Accessing a null container without checking causes an error:

```spell
null into ::?maybe.

say ::?maybe:field.          ~ ERROR: E-CTNR-NULL-ACCESS

~ ✅ Correct approach
decide ::?maybe is not null into @exists.
if @exists
  say ::?maybe:field.
end
```

### Reassigning Sealed Containers

```spell
container 'v1' into :version into ::!config.

container 'v2' into :version into ::!config.  ~ ERROR: E-CTNR-SEALED
```

### Mixing Projections Incorrectly

```spell
container
  'value' into :field
into ::data.

say ::data#0.                ~ ERROR: E-CTNR-INVALID-PROJ
                             ~ Cannot use index on non-array
```

---

## Error Handling

| Error Code                 | Description                                  | Solution                                                |
| -------------------------- | -------------------------------------------- | ------------------------------------------------------- |
| E-CTNR-SEAL-INHERITED      | Attempting to seal field in sealed container | Remove `!` from field—it inherits seal from parent      |
| E-CTNR-INVALID-PROJ        | Invalid projection syntax or type mismatch   | Check field name spelling and use correct projection    |
| E-CTNR-NULL-ACCESS         | Accessing null container without checking    | Use `decide ::?container is not null` before access     |
| E-CTNR-SEALED              | Attempting to modify sealed container        | Remove seal or create new container                     |
| E-CTNR-FIELD-NOT-FOUND     | Accessing non-existent field                 | Check field name or define field before accessing       |
| E-CTNR-INDEX-OUT-OF-BOUNDS | Array index exceeds array length             | Verify index is within bounds (0 to length-1)           |
| E-CTNR-TYPE-MISMATCH       | Projection type doesn't match node type      | Use `:field` for named nodes, `#index` for arrays       |
| E-CTNR-UNDEFINED           | Container referenced before definition       | Define container with `container ... into ::name` first |
| E-CTNR-DUPLICATE-FIELD     | Field name used multiple times in container  | Use unique field names within container                 |
| E-CTNR-INVALID-IDENTIFIER  | Container identifier has invalid characters  | Use alphanumeric characters and underscores only        |

---

## Related Pages

- [Variables](./variables.md) — Records and scalar data types (`@variable`)
- [Schema](./schema.md) — Type definitions and container validation
- [Decide](../intrinsics/decide.md) — Null checking and schema validation
- [Arrays](../intrinsics/array.md) — Creating and manipulating arrays
- [Functions](../intrinsics/function.md) — Passing containers to functions

---

## Version History

- **0.2.0** — Documentation overhaul with comprehensive examples and best practices
- **0.1.0** — Added sealed and nullable modifiers with transitive/non-transitive semantics
- **0.0.1** — Initial container implementation with field and index projections
