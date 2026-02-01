---
title: Literals
slug: literals
category: core
status: wip
version: 0.0.1
since: 0.0.1
summary: Fixed value representations including strings, numbers, and booleans.
tags: [literals, strings, numbers, booleans, constants]
---

# Literals

In nekonomicon, literals are used to represent fixed values directly in the code. They can be of various types, including strings, numbers, and booleans. Below are the different types of literals supported in nekonomicon. At the end, they are all formatted as text internally but modules and commands interpret them based on context.

## Singleline string Literals

Data can be in a form of literals. All string literals must be wrapped around single quotes `' '` and interpreted as a string.

> [!Note]
>
> Use escape character `\` when using single quote.

```spell
say 'this is a single line \'literals\''.

success.
```

```spell
'Hello, World!' into @greeting. ~ Storing literal into a variable by using a sink.
say @greeting. ~ Hello, World!

success.
```

### Multiline Literals

nekonomicon also supports **multiline literals** using the same single-quote syntax:

## Example

```spell
'This is
a multiline
string.' into @text_block.

success.
```

````spell

### Numerical/Decimal Literals

nekonomicon supports numerical literals. They do not require the string quote syntax but internally are processed as string.

**_Example_**

```spell
say 0.1.
say '4'.

success.
````

### boolean Literals

Nekonomicon supports boolean literals. They can be represented using the keywords `true` and `false`, and are internally treated as strings.

**_Example_**

```spell
say true. ~ using boolean literal
say 'true'. ~ using boolean literal in string form.

success.
```
