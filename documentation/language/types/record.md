---
title: Records
slug: record
category: core
status: wip
version: 0.0.1
since: 0.0.1
summary: Record data types for structured data with named fields.
tags: [records, data types, structured data, objects]
---

# Records

Records in nekonomicon provide a way to create structured data with named fields, similar to objects or structs in other languages. They allow you to group related data together in a meaningful way.

---

## Creating Records

### Record Syntax

Records are created using the `record` keyword followed by field definitions:

```nekonomicon
# Define a record type
record Person with
  name as text
  age as number
  email as text
end record

# Create an instance
set person to Person(
  name: "Alice Johnson",
  age: 30,
  email: "alice@example.com"
)

say "Name: {person.name}"
say "Age: {person.age}"
```

### Inline Records

You can also create records inline without defining a type:

```nekonomicon
# Create anonymous record
set user to {
  name: "Bob Smith",
  role: "developer",
  active: true
}

say "User: {user.name} ({user.role})"
```

---

## Record Types

### Basic Record Declaration

```nekonomicon
record User with
  id as number
  username as text
  email as text
  created_at as datetime
end record

record Product with
  sku as text
  name as text
  price as decimal
  in_stock as boolean
  categories as list
end record
```

### Records with Default Values

```nekonomicon
record Config with
  host as text default "localhost"
  port as number default 8080
  ssl_enabled as boolean default false
  timeout as number default 30
end record

# Using defaults
set default_config to Config()
say "Default host: {default_config.host}"  # localhost
say "Default port: {default_config.port}"    # 8080

# Overriding defaults
set custom_config to Config(
  host: "example.com",
  port: 9090
)
say "Custom host: {custom_config.host}"      # example.com
say "Custom port: {custom_config.port}"       # 9090
```

### Optional Fields

```nekonomicon
record UserProfile with
  username as text
  email as text optional
  phone as text optional
  bio as text optional
end record

set user1 to UserProfile(
  username: "alice",
  email: "alice@example.com"
)

set user2 to UserProfile(
  username: "bob",
  phone: "555-1234"
)

# Check for optional fields
if user1 has phone then
  say "User1 phone: {user1.phone}"
else
  say "User1 has no phone"
end if
```

---

## Working with Records

### Accessing Fields

Use dot notation to access record fields:

```nekonomicon
set employee to {
  name: "Carol Davis",
  department: "Engineering",
  salary: 75000,
  start_date: "2020-03-15"
}

say "Employee: {employee.name}"
say "Department: {employee.department}"
say "Salary: ${employee.salary}"
```

### Updating Records

Records are immutable by default. Create new records to modify values:

```nekonomicon
set original to { name: "John", age: 25 }

# Create updated copy
set updated to original with name: "Johnny"
say "Original: {original.name}"  # John
say "Updated: {updated.name}"   # Johnny
```

### Pattern Matching

Use records in pattern matching:

```nekonomicon
record Response with
  status as number
  data as any optional
  error as text optional
end record

set result to Response(
  status: 200,
  data: {"message": "Success"},
  error: none
)

match result
  case {status: 200} then
    say "Request successful"
  case {status: 404} then
    say "Not found"
  case {status: s, error: err} then
    say "Error {s}: {err}"
end match
```

---

## Built-in Record Functions

### Field Access

```nekonomicon
set person to {name: "Alice", age: 30, city: "New York"}

# Get all field names
set fields to fields of person
# fields = ["name", "age", "city"]

# Check if field exists
if person has "email" then
  say "Has email field"
else
  say "No email field"
end if

# Get field count
set count to field count of person
say "Person has {count} fields"
```

### Record Operations

```nekonomicon
# Merge records
set base to {name: "App", version: "1.0"}
set override to {version: "2.0", author: "DevTeam"}
set merged to merge(base, override)
# merged = {name: "App", version: "2.0", author: "DevTeam"}

# Convert to map/dictionary
set map_data to as_map(person)
# map_data = {"name": "Alice", "age": 30, "city": "New York"}

# Convert from map
set recreated from map_data
# recreated = {name: "Alice", "age": 30, "city": "New York"}
```

---

## Nested Records

### Hierarchical Data

```nekonomicon
record Address with
  street as text
  city as text
  state as text
  zip as text
end record

record Person with
  name as text
  age as number
  address as Address
  contacts as list
end record

set person to Person(
  name: "Alice Johnson",
  age: 32,
  address: Address(
    street: "123 Main St",
    city: "Boston",
    state: "MA",
    zip: "02108"
  ),
  contacts: ["alice@work.com", "alice@personal.com"]
)

say "Name: {person.name}"
say "City: {person.address.city}"
say "State: {person.address.state}"
```

### Accessing Nested Fields

```nekonomicon
# Using dot notation for nested access
say "Full address: {person.address.street}, {person.address.city}, {person.address.state} {person.address.zip}"

# Alternative with path syntax
say "City: {person["address.city"]}"
say "Zip: {person["address.zip"]}"
```

---

## Records and Modules

### Serialization

```nekonomicon
summon json

record Product with
  id as text
  name as text
  price as decimal
  tags as list
end record

set product to Product(
  id: "prod-123",
  name: "Wireless Mouse",
  price: 29.99,
  tags: ["electronics", "computer", "wireless"]
)

# Convert to JSON
set json_data to json stringify product
write json_data to file "product.json"

# Convert from JSON
set loaded_json to read file "product.json"
set loaded_product to json parse loaded_json
say "Loaded product: {loaded_product.name}"
```

### Database Operations

```nekonomicon
summon database

record User with
  id as number
  username as text
  email as text
  created_at as datetime
end record

# Create user record
set new_user to User(
  username: "bob_smith",
  email: "bob@example.com",
  created_at: now
)

# Insert into database
database insert "users" with new_user

# Query and deserialize
set results to database query "SELECT * FROM users WHERE age > 25"
for each row in results
  set user to User from row
  say "User: {user.username} ({user.email})"
end for
```

---

## Advanced Features

### Generic Records

```nekonomicon
# Generic container for any data
record Container with
  data as T
  metadata as map
end record

# Usage with different types
set text_container to Container(
  data: "Hello, World!",
  metadata: {"type": "string", "length": 13}
)

set number_container to Container(
  data: 42,
  metadata: {"type": "integer", "range": "positive"}
)
```

### Record Inheritance

```nekonomicon
record Entity with
  id as text
  created_at as datetime
  updated_at as datetime
end record

record User extends Entity with
  username as text
  email as text
end record

record Product extends Entity with
  sku as text
  name as text
  price as decimal
end record

set user to User(
  id: "user-123",
  username: "alice",
  email: "alice@example.com",
  created_at: now,
  updated_at: now
)

# Has access to both base and derived fields
say "User ID: {user.id}"
say "Username: {user.username}"
```

### Validation Rules

```nekonomicon
record EmailContact with
  address as text validate regex "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
  type as text validate in ["work", "personal", "other"]
  is_primary as boolean default false
end record

# This will fail validation
try
  set contact to EmailContact(
    address: "invalid-email",
    type: "work"
  )
catch validation_error as error
  say "Validation failed: {error.message}"
end try
```

---

## Best Practices

### 1. Meaningful Field Names

```nekonomicon
# Good: Descriptive field names
record CustomerOrder with
  order_id as text
  customer_email as text
  order_date as datetime
  total_amount as decimal
  status as text
end record

# Poor: Unclear abbreviations
record Order with
  oid as text
  cust_email as text
  o_date as datetime
  amt as decimal
  stat as text
end record
```

### 2. Consistent Naming

```nekonomicon
# Good: Consistent naming convention
record APIResponse with
  status_code as number
  response_data as any optional
  error_message as text optional
  timestamp as datetime
end record

# Poor: Inconsistent naming
record APIResponse with
  statusCode as number
  data as any optional
  error_msg as text optional
  created as datetime
end record
```

### 3. Appropriate Types

```nekonomicon
# Good: Specific types
record UserProfile with
  user_id as number
  birth_date as date
  is_active as boolean
  last_login as datetime
  preferences as map
end record

# Poor: Using generic text for everything
record UserProfile with
  user_id as text
  birth_date as text
  is_active as text
  last_login as text
  preferences as text
end record
```

---

## Common Patterns

### Configuration Records

```nekonomicon
record DatabaseConfig with
  host as text default "localhost"
  port as number default 5432
  database as text required
  username as text required
  password as text required
  ssl_mode as boolean default true
  connection_timeout as number default 30
end record

set config to DatabaseConfig(
  database: "myapp",
  username: "admin",
  password: "secret"
)
```

### Error Handling Records

```nekonomicon
record ErrorResult with
  error_code as number
  error_message as text
  error_details as map optional
  timestamp as datetime
end record

record SuccessResult with
  data as any
  metadata as map optional
  processing_time as number
end record

# Using in functions
function process_data(data)
  try
    set result to complex_operation(data)
    return SuccessResult(
      data: result,
      processing_time: elapsed_time
    )
  catch error
    return ErrorResult(
      error_code: error.code,
      error_message: error.message,
      error_details: error.context
    )
  end try
end function
```

---

## Performance Considerations

### Memory Usage

Records are lightweight but consider:
- Many small records vs. fewer large records
- Nested record depth affects access performance
- Optional fields add minimal overhead

### Serialization

- JSON serialization works best with flat records
- Deeply nested records may cause performance issues
- Consider custom serialization for complex structures

---

## Summary

Records in nekonomicon provide:
- **Structured Data** with named fields
- **Type Safety** with field type definitions
- **Immutability** by default
- **Composability** with nesting and inheritance
- **Serialization** support for data exchange

Use records to create clear, maintainable data structures in your nekonomicon scripts!