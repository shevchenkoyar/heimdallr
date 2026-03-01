# Heimdallr

Secure DLMS TCP proxy for controlled meter stand access.

---

<!-- TOC -->
* [Heimdallr](#heimdallr)
  * [What It Does?](#what-it-does)
  * [How It Works?](#how-it-works)
  * [Architecture](#architecture)
    * [Clean Architecture](#clean-architecture)
    * [Infrastructure Orchestration](#infrastructure-orchestration)
  * [Stack](#stack)
<!-- TOC -->

---

## What It Does?
* Authenticated access to meters
* Dynamic TCP port allocation
* One active session per meter
* Optional IP restrictions
* Raw TCP proxy (no client modifications required)

---

## How It Works?
1. User logs in.
2. Reserves a meter.
3. Receives a dedicated TCP port.
4. Connects using any standard DLMS client.

```text
some-domain:PORT
```

Traffic is proxied directly to the physical meter.

---

## Architecture
### Clean Architecture
* Domain
* Application
* Infrastructure
* WebUI (Blazor)

### Infrastructure Orchestration
Powered by .NET Aspire for local development and service orchestration.

---

## Stack
* .NET
* ASP.NET Core
* Blazor
* EF Core
* PostgreSQL