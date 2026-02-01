---
title: Table Module
slug: table
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Table data structures and operations.
tags: [table, data-structure, rows, columns]
---


# Table

## Description

The Table module provides operations for working with tabular data structures, including row and column manipulation.

table read 'data.csv' delimiter ',' header true into ::rows.
table write ::rows to 'out.tsv' delimiter '\t'.
