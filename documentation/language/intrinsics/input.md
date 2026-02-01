---
title: Input Module
slug: input
category: module
status: stable
version: 0.0.1
since: 0.0.1
summary: User input and interaction utilities.
tags: [input, interaction, prompt]
---

# Input

# Input

## Description

The Input module provides user input and interaction utilities for collecting data during script execution.

Input module is an extension of the reserve container `::input`. It provides additional feature to simplify the access of the script or function arguments.

## Summary Table

| Input Instructions | Syntax Example                                   | Effect                                       | Notes                                                    |
| ------------------ | ------------------------------------------------ | -------------------------------------------- | -------------------------------------------------------- |
| optional           | `input optional 'name' default 'doe' into @name` | Request specific module version to be used   | Can also uses no module for nekonomicon versioning check |
| required           | `global set silent 'off' `                       | Apply a runtime-wide setting                 |                                                          |
| reset              | `global reset silent`                            | Clear a runtime-wide setting back to default |                                                          |

# Examples

```spell
~ HelloWorld.purr
~~~~~~~~~~~~~~~~~
global require modules 'input' 'rule' 'say'.

input required 'is_enabled' into @is_enabled.
~ -OR- input required parameter 0 into @is_enabled.

input optional 'is_multiline' default false into @is_multiline.
~ -OR- input optional parameter 1 default false into @is_multiline.

rule 'is_enabled' is boolean. ~verify that they are valid boolean
rule 'is_multiline' is boolean. ~verify that they are valid boolean

function hello_world
  input required parameter 1 into @hello.
  input required parameter 2 into @world.
  ~ parameter 3 is ignored.
  say @hello @world.
end

function hello_world_multiline
  input required 'hello' into @hello.
  ::input:world into @world.
  input required parameter 3 into @exclamation.

  say @hello.
  say @world.
  say @exclamation.
end

if @is_enabled
  if not @is_multiline
    hello_world with 'Hello' 'World' '!'.
  else
    hello_world_multiline with map key 'hello' value 'Hello'
                          with map key 'world' value 'world'
                          with map item '!'.
  end

end

success.
```

`nekonomicon HelloWorld.purr --is_enabled 'true'`
