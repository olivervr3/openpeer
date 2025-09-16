# OpenPeer — Mini peer-review platform (MVP)

OpenPeer is a learning/portfolio project that mirrors a simplified scientific peer-review workflow.  
It’s designed to demonstrate end-to-end skills: **.NET 8 (Clean Architecture) + EF Core (SQL Server) + Docker + CQRS (MediatR) + FluentValidation**.

---

## Progress

### 09/12/2025 — Project Bootstrap
- Solution created with **Clean Architecture**:
  - `OpenPeer.Domain` — pure domain entities.
  - `OpenPeer.Application` — CQRS (commands/queries), validation, DTOs.
  - `OpenPeer.Infrastructure` — EF Core (DbContext, migrations).
  - `OpenPeer.Api` — ASP.NET Core Minimal API (+ Swagger).
- SQL Server 2022 running in **Docker Compose** with healthcheck + volume.
- EF Core `InitialCreate` migration applied.
- `/health` endpoint implemented.
- Versioning with Git + README baseline.

### 16/09/2025 — Articles Endpoints
- Added **MediatR v13** for CQRS pattern.
- Added **FluentValidation** for declarative input validation.
- Application layer:
  - `Commands` → `CreateArticle`.
  - `Queries` → `GetArticleById`, `GetArticlesPaged`.
  - `Validators` → rules for `CreateArticle`.
  - `Dtos` → `ArticleDto`.
- API layer:
  - `POST /articles` — create article.
  - `GET /articles/{id}` — get by id.
  - `GET /articles?page=1&pageSize=10` — list with pagination.
- Swagger UI to test endpoints.

---

## Architecture

**Domain entities implemented so far:**
- `Article` (Title, Abstract, CreatedAt, Status, RowVersion)
- `Review` (to be added in Session 3)
- `EditorialDecision` (to be added in Session 3)

**Relationships:**
- Article 1—* Reviews (cascade delete planned)
- Article 1—1 EditorialDecision (optional)

---

## Prerequisites

- .NET 8 SDK → `dotnet --info`
- Docker Desktop → `docker --version`
- Git → `git --version`
- Visual Studio Code (recommended) + **MSSQL** extension

---

## Quickstart

> Commands shown for **Windows (PowerShell/CMD)**. On macOS/Linux replace `\` with `/`.

### 1) Clone
```bash
git clone https://github.com/ovincent/openpeer.git
cd openpeer
```

### 2) Start SQL Server (Docker)
```bash
docker compose up -d
docker compose ps   # wait for 'healthy'
```

### 3) Apply EF Core migration
```bash
dotnet tool install --global dotnet-ef  # if not installed
dotnet ef database update -p src\OpenPeer.Infrastructure -s src\OpenPeer.Api
```

### 4) Run the API
```bash
dotnet run --project src\OpenPeer.Api
```

---

## Endpoints

### Health
- `GET /health`

### Articles
- `POST /articles`
  ```json
  { "title": "My first article", "abstract": "Short test abstract." }
  ```
  → `201 Created` with `{ "id": 1 }`.

- `GET /articles/1`
  → Returns article by ID or `404 Not Found`.

- `GET /articles?page=1&pageSize=5`
  → Returns list of articles (paginated).

---

## Next Steps
- **Session 3:** Add `Reviews` and `EditorialDecisions`, including optimistic concurrency (RowVersion) and `409 Conflict` handling.
- **Session 4:** Observability (Serilog + OpenTelemetry) and background services.

---
