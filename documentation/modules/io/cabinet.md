---
title: Cabinet Module
slug: cabinet
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: File and folder management operations across platforms.
tags: [cabinet, file, folder, filesystem, io]
---

# Cabinet

## Description

Cabinet is nekonomicon’s built-in file and folder management module.  
It provides a simple, composable interface for common filesystem operations—such as listing, reading, writing, moving, copying, and deleting files and directories—across platforms.

## Features

- List files and folders with filtering and sorting
- Read and write file contents
- Create, move, copy, and delete files or directories
- Supports safe, elevated, and sensitive operations
- Integrates with nekonomicon’s result pattern and modifiers

## Summary Table

- `list` `file`
  - `with`:
    - `hidden` → Includes hidden files in the list.
    - `recursion [depth @count]` →
    - `sort` [`size`, `name`,] — Sort files/folders by name, date, size, etc.
  - `limit` @count — Limit the number of results returned.
  - `extension` (@extension, **+**) — Filter by file extension (e.g., .txt, .jpg).
  - `size` @min @max — Filter by file size range.
  - date @from `to` @last — Filter by creation/modification date.
  - owner '<user>' — Filter by file owner.
  - type '<file|folder|symlink>' — Filter by item type.
  - with pattern '<glob>' — Use glob patterns for matching.
  - with attributes '<hidden|readonly|system>' — Filter by file attributes.
  - `without`
    - empty — Exclude empty files or folders.
- `create` @file
- `create` ::file
- 'create' `files` [@file, ::file, +]
- `delete`
- `read`
- `write`

## Examples

### List Files

```spell
cabinet list in '/Users/alex/Documents' with hidden with depth '2' filter '[.txt]'
```

### Read a File

```spell
cabinet read file '/Users/alex/Documents/notes.txt' into @content
say @content
```

### Write to a File

```spell
cabinet write file '/Users/alex/Documents/notes.txt' with content 'Hello, nekonomicon!'
```

### Delete a File

```spell
safe cabinet delete file '/Users/alex/Documents/old.txt'
```

### Create a Directory

```spell
cabinet create folder '/Users/alex/Documents/new_folder'
```

### Move or Copy Files

```spell
cabinet move file '/Users/alex/Documents/a.txt' to '/Users/alex/Documents/b.txt'
cabinet copy file '/Users/alex/Documents/a.txt' to '/Users/alex/Documents/copy.txt'
```

## Modifiers and Clauses

- Use `safe` to prevent script failure on errors.
- Use `elevated` for admin rights when required.
- Use `sensitive` for operations involving vault variables.

## Best Practices

- Always use `safe` for destructive operations unless failure should halt the script.
- Use filters and depth options to control file listing output.
- Combine Cabinet with result patterns for robust automation.

---

Let me know if you want more advanced examples, error handling, or details on platform-specific behavior!
