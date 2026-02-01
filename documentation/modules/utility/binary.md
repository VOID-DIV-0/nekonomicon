---
title: Binary Module
slug: binary
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Binary data operations.
tags: [binary, bytes, data]
---

# Binary

## Description

Instructions for manipulating binary data: encoding, decoding, bitwise operations, and format conversion. Useful for files, network packets, and low‑level data.

> **Sink rule**  
> `binary` instructions are **producers**. Bind results with a **sink**: `into`, `update`, or `save` (see `core/sinks.md`).  
> Destinations must be **`@name` (scalar)** or **`::name` (container)**; projections `::name:…` are **not** valid sink targets.
>
> - `into @/::` → create new binding (error if it exists)
> - `update @/::` → replace existing binding (error if missing)
> - `save   @/::` → materialize (if needed) and bind **immutable**

---

## Summary Table

| Instruction | Syntax Example                                  | Effect                                 | Notes                           |
| ----------- | ----------------------------------------------- | -------------------------------------- | ------------------------------- |
| encode      | `binary encode 'hi' format 'base64' into @out.` | Encode input; emit scalar or container | Formats: `base64`, `hex`, `raw` |
| decode      | `binary decode @b format 'hex' into @txt.`      | Decode from format to string           |                                 |
| bitwise     | `binary bitwise and '1010' '1100' into @out.`   | Bitwise operation on inputs            | Ops: `and`, `or`, `xor`         |
| not         | `binary not '10101010' into @out.`              | Bitwise NOT                            |                                 |
| shift       | `binary shift '10101010' left 2 into @out.`     | Shift bits left/right                  |                                 |

---

## Usage

### Encode → string

```spell
binary encode 'Hello World!' format 'base64' into @encoded.
say @encoded.  ~ 'SGVsbG8gV29ybGQh'.
```

### Encode → binary container (multiple projections)

```spell
binary encode 'Hello World!' into ::bin.
say ::bin:base64. ~ 'SGVsbG8gV29ybGQh'.
say ::bin:hex.    ~ '48656c6c6f20576f726c6421'.
say ::bin:raw.    ~ 'Hello World!'.
```

### Decode base64 to text

```spell
binary decode 'SGVsbG8gV29ybGQh' format 'base64' into @decoded.
say @decoded. ~ 'Hello World!'.
```

### Bitwise AND

```spell
binary bitwise and '10101010' '11001100' into @result.
say @result. ~ '10001000'.
```

### Bitwise NOT

```spell
binary not '10101010' into @result.
say @result. ~ '01010101'.
```

### Shift left

```spell
binary shift '10101010' left 2 into @result.
say @result. ~ '1010101000'.
```

---

## Binary Schema

A binary container holds raw data plus common projections.

```spell
schema &Binary
  'hex'     @hex_value
  'raw'     @raw_value
  'base64'  @base64_value
End
```

> **Note:** If you `save ::Binary`, it is **immutable**. Use module‑level ops to derive a new container and then `update` a mutable binding if needed.
