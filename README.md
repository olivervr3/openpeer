# OpenPeer — Mini peer-review platform (MVP)

OpenPeer is a learning/portfolio project that mirrors a simplified scientific peer-review workflow.  
It’s designed to demonstrate end-to-end skills: **.NET 8 (Clean Architecture) + EF Core (SQL Server) + Docker**.

> **9/12/2025** delivers: solution bootstrap, SQL Server in Docker, EF Core mappings, initial migration, health endpoint.

---

## 9/12/2025 Features MVP

- Clean Architecture skeleton:
  - `OpenPeer.Domain` — pure domain entities.
  - `OpenPeer.Application` — (reserved for CQRS/use-cases in Session 2).
  - `OpenPeer.Infrastructure` — EF Core (DbContext, mappings).
  - `OpenPeer.Api` — ASP.NET Core Minimal API (+ Swagger).
- SQL Server 2022 in Docker with **persistent volume** and **healthcheck**.
- EF Core **InitialCreate** migration applied.
- `/health` endpoint.

---

## Architecture (high-level)


**Domain entities (9/12/2025):**
- `Article` (Title, Abstract, CreatedAt, Status, RowVersion)
- `Review` (ArticleId, Reviewer, Score, Comments, CreatedAt)
- `EditorialDecision` (ArticleId, Decision, Notes, DecidedAt)

Relationships:
- Article 1—* Reviews (cascade delete)
- Article 1—1 EditorialDecision (optional)

---

## Prerequisites

- .NET 8 SDK — `dotnet --info`
- Docker Desktop — `docker --version`
- Git — `git --version`
- Visual Studio Code (recommended) + **MSSQL** extension

---

## Quickstart

> Commands shown for **Windows (PowerShell/CMD)**. On macOS/Linux, change `\` to `/`.

### 1) Clone
```bat
git clone https://github.com/ovincent/openpeer.git
cd openpeer
```

---

### Start the server
```docker compose up -d
docker compose ps  # wait for 'healthy'
```

---

### Check logs if needed:
```docker compose logs -f mssql
```

---

### 3) Apply EF Core migration
```dotnet tool install --global dotnet-ef  # if not installed
dotnet ef database update -p src\OpenPeer.Infrastructure -s src\OpenPeer.Api
```

---

### 4) Run the API
```dotnet run --project src\OpenPeer.Api
```

---