---
title: Variable Module
slug: variable-module
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Variable management and introspection.
tags: [variable, introspection, metadata]
---


# Variable

## Description

The Variable module provides introspection and metadata operations for variables, including type inspection and variable management utilities.

variable delete @rec. # remove variable entirely (unset/clear)
variable exists @rec into @ok. # check if variable is bound and non-empty
variable empty @rec into @ok. # check if value == ''
variable length @rec into @len. # number of characters
variable matches @rec 'regex' into @ok.
variable is-numeric @rec into @ok.
variable is-integer @rec into @ok.
variable is-decimal @rec into @ok.
variable is-date @rec 'YYYY-MM-DD' into @ok.
variable lens @rec as integer into @out. # throws if invalid
variable try-lens @rec as integer into @out ok @ok. # sets @ok true/false
variable lens @rec as decimal into @out.
variable lens @rec as json into ::container.
variable ensure @a default 'my_default_value' overwrite @a

• Destination / slot: the place after into.
• Scalar slot: @name (always text). (single value)
• Object slot: ::name (typed, with projections).
• scalar Projection: ::name:field[:subfield…] (read‑only views).

---

## Notes on `into`

### For scalars (`@`)

- `@name` stores a single text value (scalar).
- Overwrites the value if already set.
- Always string-based at base level.
- No projection: you always reference the whole scalar.

### For containers (`::`)

- `::container` can store structured or typed data.
- `::container:field` mutates only that field in the container.
- Root assignment overwrites the whole container.
- Type enforcement is applied (e.g., `::boolean` only accepts true/false).

### Mental model

- Scalars (`@`) are “single sticky notes” for text.
- Containers (`::`) are “labeled boxes” that can have types and compartments.
- `into` is the action of putting a value into the right sticky note or box at the end of a sentence.
