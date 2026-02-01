---
title: Time Module
slug: time
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Time and date operations.
tags: [time, date, datetime, timestamp]
---


# Time

## Description

The Time module provides operations for working with dates, times, timestamps, and time-based calculations.

time
• time now into ::t
• time parse '2025-08-08T12:00:00Z' into ::t
• time add ::t days '7' into ::t2
• time diff ::a ::b in 'days' into @days
• time format ::t 'YYYY-MM-DD' into @s
• time compare ::a after|before|equal ::b into @ok
