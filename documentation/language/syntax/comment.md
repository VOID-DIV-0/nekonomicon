---
version: 0.1.0
title: comment
status: stable
---

# Comment

## Single-line Comments

Comments are an important part of writing clear code. They help explain the purpose and functionality of code sections to anyone reading it later, including your future self. Nekonomicon uses a simple syntax for comments that is easy to read and write.

Use the `~` symbol to write a comment. nekonomicon encourages self-documenting code, so comments should be used sparingly, not excessively.

Examples:

```spell
set '3' into @value. ~ this set the value to 3.
```

## Multiline Comments

For longer explanations or documentation, nekonomicon supports multiline comments. Enclose the comment text within `~~~` and `~~~` to create a block comment that spans multiple lines.

```spell
~~~
    This is a multiline comment.
    It can span multiple lines.
    Use it to explain complex logic or provide detailed information.
~~~

say 'Hello, World!'.
```
