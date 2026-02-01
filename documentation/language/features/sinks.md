---
title: Sinks
slug: sinks
category: core
status: wip
version: 0.0.1
since: 0.0.1
summary: Output targets for command results using into keyword.
tags: [sinks, into, output, assignment]
---

# Sinks

Sinks define how data is stored or validated in nekonomicon. They determine whether bindings are mutable or immutable, and whether data conforms to expected schemas.

---

## `into` — Modify or create binding

**Syntax:** `<instructions> into [{!}@variable | {!}::container | ::container:projection]`

Creates a new binding or updates an existing one. Bindings created with `into` are mutable and can be modified later.

When you seal a container using !::container, all projections (fields, elements) within that container become immutable. You cannot update any projection of a sealed container.

### Examples

**Binding into an unsealed variable**

```spell
'hello' into @greeting.
say @greeting.              ~ Output: hello

'hi' into @greeting.        ~ Overwrite existing value
say @greeting.              ~ Output: hi
```

**Binding into a sealed variable**

```spell
'hello' into @!hello. ~ Make the variable hello sealed.
string concat @!hello ' world' into @result. ~ OK: Reading from sealed variable
'new' into @!hello. ~ ERROR: Cannot modify sealed variable
```

**Binding into an unsealed container**

```spell
array create 'a', 'b', 'c' into ::my_array.
say ::my_array:(0). ~ Output: a
say ::my_array:(1). ~ Output: b
say ::my_array:(2). ~ Output: c
```

**Binding into a sealed container**

```spell
array create 'a', 'b', 'c' into !::my_array.
say !::my_array:(0). ~ Output: a (reading is OK)
say !::my_array:(1). ~ Output: b
'z' into !::my_array:(1). ~ ERROR: Cannot modify a projection of a sealed container
```

**Binding into a projection**

```spell
'localhost' into ::config:database:host.
say ::config:database:host. ~ Output: localhost

'5432' into ::config:database:port.
say ::config:database:port. ~ Output: 5432
```

---

## `is` — Validate Against Schema

**Syntax:** `<instructions> is &schema [into binding]`

Validates data against a schema. If validation fails, the script fails with an error. This ensures data integrity and type safety.

When combined with `into`, validation and assignment happen atomically - only validated data is stored in the target binding.

### Examples

**Basic validation:**

```spell
@config is &settings_schema.         ~ Fails script if @config doesn't match schema
```

**Container validation:**

```spell
::user_data is &user_schema.         ~ Validates entire container
::config:database is &db_schema.     ~ Validates specific field
```

**Validation with error handling:**

```spell
safe @input is &input_schema.        ~ Continue on validation failure
```

**Validation errors:**

```spell
@invalid_data is &user_schema.       ~ ERROR: Script fails if validation fails
safe @invalid_data is &user_schema.  ~ Script continues, validation result in return code
```

**Combining with other sinks:**

```spell
'john@example.com' into @email.
@email is &email_schema.             ~ Validate after assignment

'admin' into @!role.
@!role is &role_schema.               ~ Validate sealed value
```

**Validate-and-assign (atomic operation):**

```spell
user_input 'email' is &email_schema into @email.           ~ Validate then assign to mutable
config_read 'api_key' is &api_key_schema into @!api_key.   ~ Validate then seal
safe @raw_data is &data_schema into @clean_data.           ~ Continue on validation failure
```

---

## Best Practices

1. **Use `into @ | ::` for working bindings** that need to change during script execution.
2. **Use `into @! | !::` for configuration and constants** to prevent accidental modification.
3. **Use `is` after data input or computation** to ensure data integrity.
4. **Use `is ... into` for validate-and-assign** when you want only validated data stored.
5. **Seal security-sensitive values** like API keys or passwords to prevent tampering.
6. **Validate external data** (user input, file contents, API responses) with `is` before use.
7. **Use the `!` prefix consistently** when reading from sealed bindings for clarity.

---

## Edge Cases and Error Scenarios

### Double Sealing

```spell
'value' into @!constant.
'new' into @!constant.    ~ ERROR: Cannot modify sealed binding
```

### Validation with Assignment

```spell
'invalid' is &user_schema into @user.         ~ ERROR: Validation fails, @user not created
safe 'invalid' is &user_schema into @user.    ~ Script continues, @user remains unset
'valid_data' is &user_schema into @!user.     ~ Validates then seals the binding
```

### Reading vs Writing Sealed Bindings

```spell
'data' into @!sealed.
say @!sealed.             ~ OK: Reading with ! prefix
say @sealed.              ~ OK: Reading without ! prefix (both work)
'new' into @sealed.       ~ ERROR: Cannot modify sealed binding
```

### Projection Access Rules

```spell
'host' into !::config:database:host.     ~ ERROR: Cannot seal projection directly
'host' into ::config:database:host.      ~ OK: Mutable projection
array create into !::data.               ~ Sealed container
say !::data:(0).                         ~ OK: Reading from sealed container
'new' into !::data:(0).                  ~ ERROR: Cannot modify sealed container's projections
```

---

## Complete Example

```spell
~ Configuration (immutable)
'https://api.example.com' is &url_schema into @!api_base.    ~ Validate and seal
'production' is &environment_schema into @!environment.     ~ Validate and seal

~ Working variables (mutable)
user_input 'endpoint' is &path_schema into @endpoint.       ~ Validate user input

~ Build URL
text concat @!api_base, @endpoint into @full_url.

~ Validate before use
@full_url is &url_schema.

~ Make request
http get @full_url into ::response.

~ Validate response
::response is &api_response_schema.

success ::response.
```

---

## Related Pages

- [Data Types](./data-0.0.1.md) — Records, containers, and projections
- [Schemas](./schema.md) — Schema definition and validation
- [Vault](../modules/vault-0.0.1.md) — Secure handling of sensitive data
