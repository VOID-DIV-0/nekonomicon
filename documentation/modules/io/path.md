---
title: Path Module
slug: path
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: File path manipulation and utilities.
tags: [path, filesystem, utilities]
---

# Module `path`

## Description

The `path` module provides instructions for manipulating and analyzing filesystem paths. It helps you join, split, normalize, and extract information from paths in a cross-platform way.

## Features

- Join multiple path segments
- Split a path into components
- Normalize paths (resolve `..`, `.`)
- Extract filename, extension, parent directory
- Convert between absolute and relative paths
- Inspect and modify path containers

## Instructions and Modifiers List

- `join @segment1 @segment2 [@segmentN] into @output` — Join segments into a single path
- `split @input into ::output` — Split a path into its components (container)
- `normalize @input into @output` — Normalize a path
- `basename @input into @output` — Get the filename
- `dirname @input into @output` — Get the parent directory
- `extension @input into @output` — Get the file extension
- `absolute @input into @output` — Convert to absolute path
- `relative @input base @base into @output` — Convert to relative path

## Examples

### Join segments

```
path join 'folder' 'subfolder' 'file.txt' into @full_path.
say @full_path. ~ 'folder/subfolder/file.txt'.
```

### Split a path

```
path split 'folder/subfolder/file.txt' into ::parts.
say ::parts:0. ~ 'folder'.
say ::parts:1. ~ 'subfolder'.
say ::parts:2. ~ 'file.txt'.
```

### Normalize a path

```
path normalize 'folder/../file.txt' into @normalized.
say @normalized. ~ 'file.txt'.
```

### Get filename and extension

```
path basename '/home/user/file.txt' into @filename.
say @filename. ~ 'file.txt'.

path extension '/home/user/file.txt' into @ext.
say @ext. ~ 'txt'.
```

### Get parent directory

```
path dirname '/home/user/file.txt' into @parent.
say @parent. ~ '/home/user'.
```

## Path Container Content

A path container holds the components of a path and may include metadata such as whether it is absolute or relative.

```
Container ::Path
    'segments': [@segment1, @segment2, ...]
    'is_absolute': @bool
    'filename': @filename
    'extension': @extension
    'parent': @parent
End
```
