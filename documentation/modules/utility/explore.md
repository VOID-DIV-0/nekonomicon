---
title: Explore Module
slug: explore
category: module
status: stable
version: 0.0.1
since: 0.0.1
summary: Data exploration and introspection utilities.
tags: [explore, introspection, debug, inspect]
---

# explore 0.0.1

# Explore

## Description

The Explore module provides data exploration and introspection utilities for examining runtime values, structures, and metadata.

## Overview

The `explore` module provides a flexible and expressive query language for data exploration within nekonomicon. It enables users to retrieve, filter, transform, and aggregate data from various data sources using a rich set of clauses and reducers. The module is designed to support complex data querying scenarios while maintaining clarity and composability.

## Syntax and Signatures

The `explore` module supports a declarative query syntax composed of multiple clauses. Below is an EBNF-like representation of the query structure:

```
query         ::= [distinct] from_clause [join_clause] [where_clause] [group_by_clause] [select_clause] [order_by_clause] [flatten_clause] [take_clause] [skip_clause] [reducers_clause] [destination_clause]

distinct      ::= "distinct"

from_clause   ::= "from" source

join_clause   ::= { ("join" | "left join" | "right join" | "inner join" | "outer join") source ["on" condition] }

where_clause  ::= "where" condition

group_by_clause ::= "group by" field_list

select_clause ::= "select" field_list | "*"

order_by_clause ::= "order by" order_list

flatten_clause ::= "flatten" field

take_clause   ::= "take" integer

skip_clause   ::= "skip" integer

reducers_clause ::= reducer { "," reducer }

reducer      ::= reducer_name "(" [field] ")"

destination_clause ::= "into" destination_action destination_target

source       ::= identifier | subquery

condition    ::= expression

field_list   ::= field { "," field }

order_list   ::= order_item { "," order_item }

order_item   ::= field [ "asc" | "desc" ]

field        ::= identifier | expression

reducer_name ::= "count" | "sum" | "avg" | "min" | "max" | "collect" | "group_concat" | "first" | "last"

destination_action ::= "new" | "update" | "merge" | "ensure"

destination_target ::= identifier
```

## Supported Clauses

- **from**: Defines the data source for the query.
- **join**: Supports various join types (`join`, `left join`, `right join`, `inner join`, `outer join`) with optional join conditions.
- **where**: Filters records based on conditions.
- **select**: Specifies which fields to retrieve; supports `*` for all fields.
- **order by**: Orders the result set by specified fields, ascending or descending.
- **group by**: Groups records by specified fields.
- **distinct**: Removes duplicate records from the result set.
- **flatten**: Flattens nested collections or arrays within fields.
- **take**: Limits the number of records returned.
- **skip**: Skips a specified number of records.
- **reducers**: Aggregation functions such as `count()`, `sum(field)`, `avg(field)`, `min(field)`, `max(field)`, `collect(field)`, `group_concat(field)`, `first(field)`, and `last(field)`.

## Destination Binding Rules

The `into` clause specifies how query results are saved or applied:

- `into new <target>`: Creates a new entity or dataset named `<target>`. Fails if `<target>` already exists.
- `into update <target>`: Updates an existing entity or dataset named `<target>`. Fails if `<target>` does not exist.
- `into merge <target>`: Merges results into `<target>`, creating it if it does not exist.
- `into ensure <target>`: Ensures that `<target>` exists by creating it if necessary, otherwise updates it.

## Examples

### Basic Query

```nekonomicon
explore from users
        where age > 18
        select id, name, email
        order by name asc
        take 10.
```

### Using Joins and Group By

```nekonomicon
explore from orders
        join users on orders.user_id = users.id
        where orders.status = "completed"
        group by users.country
        select users.country, count(orders.id) as order_count
        order by order_count desc into ::content.
```

### Using Reducers and Destination

```nekonomicon
explore  from sales
        where date >= "2023-01-01"
        group by product_id
        select product_id, sum(amount) as total_sales
        into @monthly_sales_summary.
```

## Error Model

The `explore` module defines the following error codes and conditions:

- `E1001`: **SourceNotFound** — The specified data source in the `from` or `join` clause does not exist.
- `E1002`: **InvalidSyntax** — The query contains syntax errors or unsupported clauses.
- `E1003`: **FieldNotFound** — A referenced field in `select`, `where`, `group by`, or reducers does not exist in the source.
- `E1004`: **DestinationExists** — The `into new` target already exists.
- `E1005`: **DestinationNotFound** — The `into update` target does not exist.
- `E1006`: **JoinConditionMissing** — A join clause requires an `on` condition but it is missing or invalid.
- `E1007`: **InvalidReducerUsage** — Reducer function used incorrectly or with invalid arguments.
- `E1008`: **TypeMismatch** — Type incompatibility in expressions or join conditions.
- `E1009`: **PermissionDenied** — Insufficient permissions to read source or write to destination.

Users should handle these errors appropriately when constructing queries or processing results.
