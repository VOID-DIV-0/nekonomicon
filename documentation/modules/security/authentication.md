---
title: Authentication Module
slug: authentication
category: module
status: wip
version: 0.0.1
since: 0.0.1
summary: Authentication and authorization operations.
tags: [authentication, auth, security, credentials]
---


# Authentication

## Description

The Authentication module provides utilities for authentication and authorization operations, including credential management and token handling.

authentication basic user @u pass @p into @header.
authentication bearer @token into @header.
authentication jwt parse @token into ::claims. // no verification yet
authentication jwt verify @token alg 'HS256' key @secret into @ok.
authentication jwt sign ::claims alg 'HS256' key @secret into @token.
