---
title: Text Module
slug: text
category: module
status: stable
version: 0.0.1
since: 0.0.1
summary: String and pattern operations for text manipulation.
tags: [text, string, pattern, format]
---

# Text

## Description

Unified string and pattern operations: create, transform, inspect, match, and format textual values.

## Summary Table

| Category   | Instruction                                        | Output          |
| ---------- | -------------------------------------------------- | --------------- |
| Basic      | text concat 'a' 'b' into @out.                     | record          |
| Basic      | text length @s into @len.                          | record (number) |
| Basic      | text upper @s into @u.                             | record          |
| Structural | text split @s by ',' into ::parts.                 | container(list) |
| Structural | text join ::parts with ', ' into @csv.             | record          |
| Pattern    | text match @s 'regex' into @has.                   | boolean record  |
| Pattern    | text replace @s 'from' 'to' into @new.             | record          |
| Pattern    | text capture @s '([A-Z]+)-(\\d+)' into ::cap.      | container       |
| Format     | text format 'Hello {name}' with name @n into @msg. | record          |
| Slice      | text slice @s from '2' to '5' into @sub.           | record          |

| Operator          | Meaning                   | Example                          |
| ----------------- | ------------------------- | -------------------------------- |
| equals            | String equality           | `@str1 equals @str2`             |
| not equals        | String inequality         | `@str1 not equals @str2`         |
| contains          | Substring presence        | `@text contains 'foo'`           |
| not contains      | Substring absence         | `@text not contains 'bar'`       |
| starts with       | Prefix match              | `@filename starts with 'log_'`   |
| ends with         | Suffix match              | `@filename ends with '.txt'`     |
| matches regex     | Pattern match             | `@input matches '^user_[0-9]+$'` |
| not matches regex | Pattern non-match         | `@input not matches '^temp_.*'`  |
| is empty          | Empty string check        | `@str is empty`                  |
| is not empty      | Non-empty string check    | `@str is not empty`              |
| is whitespace     | Whitespace only check     | `@str is whitespace`             |
| is not whitespace | Not whitespace only check | `@str is not whitespace`         |
| is missing 'x'    | Character absence check   | `@str is missing 'a'`            |
| has 'x'           | Character presence check  | `@str has 'b'`                   |

## Categories

### 1. Basic

(…)

### 2. Structural

(…)

### 3. Pattern & Matching

(…)

### 4. Formatting

(…)

### 5. Slice & Range

(…)

### 6. Edge Cases & Performance

(…)
