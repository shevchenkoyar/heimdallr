<h1 align="center" id="title">Heimdallr</h1>

<p align="center">
    <img src="https://socialify.git.ci/shevchenkoyar/heimdallr/image?description=1&amp;font=Inter&amp;name=1&amp;owner=1&amp;pattern=Floating+Cogs&amp;theme=Auto" alt="project-image">
</p>

<p align="center">
    <img src="https://img.shields.io/github/checks-status/shevchenkoyar/heimdallr/main" alt="shields">
    <img src="https://img.shields.io/github/commit-activity/t/shevchenkoyar/heimdallr" alt="shields">
    <img src="https://img.shields.io/badge/.NET-10-blue" alt="shields">
    <img src="https://img.shields.io/github/license/shevchenkoyar/heimdallr" alt="shields">
</p>

Heimdallr is a secure DLMS TCP proxy designed for controlled access to physical meter stands.  
It provides authentication, session control, dynamic TCP port allocation and traffic isolation without requiring any client modifications.

---

<!-- TOC -->
  * [Features](#features)
  * [How it works](#how-it-works)
  * [Architecture](#architecture)
    * [Clean Architecture](#clean-architecture)
    * [Infrastructure Orchestration](#infrastructure-orchestration)
  * [Core Concepts](#core-concepts)
    * [Users](#users)
    * [Meters](#meters)
    * [ProxyPorts](#proxyports)
    * [ProxySessions](#proxysessions)
  * [Security Model](#security-model)
  * [Built With](#built-with)
  * [Contributors](#contributors)
  * [License](#license)
<!-- TOC -->

---

## Features
- User authentication and role-based access control
- Configurable IP allow rules per user
- Dynamic TCP port allocation from managed pool
- Single active session per meter
- Lease-based session lifecycle (TTL support)
- Optional IP pinning (bind session to first connecting IP)
- Raw TCP proxy (byte-to-byte forwarding)
- Meter availability tracking
- Session metrics (traffic counters, timestamps)
- Audit-ready architecture

## How it works
1. User authenticates via Web UI.
2. User selects and reserves a meter.
3. System allocates a free TCP port from the pool.
4. A proxy session is created and the meter is marked as **Busy**.
5. User connects using any standard DLMS TCP client: `some-domain:<allocated_port>`
6. Heimdallr establishes a connection to the real meter endpoint.
7. TCP traffic is proxied transparently (no protocol modification).
8. When session ends or TTL expires:
    - Port is released
    - Meter becomes available
    - Session is finalized


## Architecture

### Clean Architecture

The solution follows strict separation of concerns:

- **Domain**
    - Core entities (User, Meter, ProxySession, ProxyPort)
    - Business rules (meter locking, session lifecycle, IP policy)
    - State transitions

- **Application**
    - Use cases (ReserveMeter, StartSession, StopSession)
    - Port allocation logic
    - Validation & orchestration
    - Session state management

- **Infrastructure**
    - EF Core persistence
    - TCP socket proxy implementation
    - Port pool management
    - IP validation
    - Logging & metrics

- **WebUI (Blazor)**
    - Authentication
    - Meter dashboard
    - Session monitoring
    - Administrative controls

### Infrastructure Orchestration

Heimdallr uses **.NET Aspire** for:

- Service orchestration
- Local development environment
- Dependency wiring (PostgreSQL, future services)
- Structured configuration
- Observability integration (ready for metrics/exporters)

## Core Concepts

### Users
- Authentication
- Roles (Admin / User)
- IP allow rules
- Session ownership

### Meters
- Physical device representation
- Real network endpoint mapping
- Availability state derived from active sessions

### ProxyPorts
- Managed pool of TCP ports
- Atomic allocation
- Reserved/InUse/Free state tracking

### ProxySessions
- Port binding
- Meter locking
- IP pinning
- Traffic metrics
- Lease expiration


## Security Model

- One active session per meter
- Port is allocated only during active session
- Optional IP restriction per user
- Optional IP pin-to-first-client
- Server never exposes physical meter address
- No client-side modification required
- TLS termination can be placed at reverse proxy level

## Built With

- .NET 10
- ASP.NET Core
- Blazor
- EF Core
- PostgreSQL
- .NET Aspire

## Contributors


| [<img alt="Yaroslav Shevchenko" src="https://avatars1.githubusercontent.com/u/46241859?s=460&v=4" width="100">](https://github.com/shevchenkoyar) | [<img alt="Alexey Tyapkin" src="https://avatars1.githubusercontent.com/u/34785129?s=460&v=4" width="100">](https://github.com/Lentilles) |
|:-------------------------------------------------------------------------------------------------------------------------------------------------:|:----------------------------------------------------------------------------------------------------------------------------------------:|
|                                              [Yaroslav Shevchenko](https://github.com/shevchenkoyar)                                              |                                              [Alexey Tyapkin](https://github.com/Lentilles)                                              |


## License

This project is licensed under the [MIT License](LICENSE)