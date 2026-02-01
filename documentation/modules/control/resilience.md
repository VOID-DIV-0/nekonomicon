---
title: Resilience Module
slug: resilience
category: module
status: stable
version: 0.0.1
since: 0.0.1
summary: Retry, timeout, and circuit breaker patterns.
tags: [resilience, retry, timeout, circuit-breaker]
---

# Resilience

# Resilience

## Description

The Resilience module provides retry, timeout, and circuit breaker patterns for building robust and fault-tolerant scripts.

The `resilience` module provides advanced error handling and reliability features for nekonomicon scripts. It enables your automation to gracefully handle failures, retries, timeouts, fallbacks, and circuit breaking, making scripts robust and production-ready.

## Features

- **Retry:** Automatically re-attempt failed operations, with support for exponential backoff, delay, and jitter to avoid resource contention.
- **Timeout:** Limit the maximum execution time for commands, ensuring scripts do not hang indefinitely.
- **Fallback:** Specify alternative actions or endpoints if the primary operation fails.
- **Circuit Breaker:** Temporarily disable failing operations after repeated errors, with configurable thresholds and cool-down periods.
- **Error Evaluation:** Retry or handle failures based on specific error codes or conditions.

## Usage Examples

### Retry with Backoff and Error Evaluation

```spell

function fn_status
  http get 'https://api.example.com/status' into ::res.
  success ::res.
end

resilience retry
  do fn_status
  with times '3'
  with backoff 'exponential'
  with delay '500' ms
  with jitter '20' percent
  with evaluate ['E_TIMEOUT','E_5XX']
into ::attempt.

if ::attempt:ok
  say 'ok'.
else
  failure ::attempt:message.
end
```

### Timeout

```spell

function fn_post
  http post 'https://slow.example.com' with @payload into ::res.
  success ::res.
end

resilience do fn_status with timeout '2s' into ::timed.

if ::timed:ok
  say 'posted'.
else
  failure ::timed:code.         ~ likely 'E_TIMEOUT'
end
```

### Fallback

```spell

function fn_primary
  http get 'https://primary/api' into ::result.
  success ::result.
end

function fn_secondary
  http get 'https://backup/api' into ::result.
  success ::result.
end

resilience fallback
  primary do fn_primary
  secondary do fn_secondary
into ::final.

if ::final:ok
  say 'used primary or backup'.
else
  failure 'both failed'.
end
```

### Circuit Breaker

```spell

function fn_pay
  http get 'https://backup/api' into ::result.
  success ::result.
end

resilience circuit do fn_pay
  with threshold 5 in '30s'
  with cooldown '60s'
into ::cb.

if ::cb:code is 'E_OPEN_CIRCUIT'
  failure 'temporarily disabled'.
end
```

## Best Practices

- Use retries with backoff and jitter for transient errors and network operations.
- Set timeouts for all external or long-running commands to prevent stuck scripts.
- Provide fallback logic for critical operations to improve reliability.
- Use circuit breakers to protect downstream services and avoid cascading failures.
- Evaluate error codes to handle only the failures you care about.
