# Zeiss Products — Project Documentation

## Table of Contents

1. [Overview](#overview)
2. [Architecture](#architecture)
3. [Project Structure](#project-structure)
4. [Domain Model](#domain-model)
5. [CQRS & Features](#cqrs--features)
6. [Infrastructure](#infrastructure)
7. [API Endpoints](#api-endpoints)
8. [Running the Application](#running-the-application)
9. [Tests](#tests)

---

## Overview

**Zeiss Products** is a RESTful API for products and inventories. 
It was implemented **.NET 10** and follows **Clean Architecture** combined 
with **CQRS** (Command/Query Responsibility Segregation) pattern. It also 
uses asynchronous communication to simmulate the publication of domain events
using RabbitMQ. Inventory operations uses some idempotency mechanism to try to
prevent double requests by the same user.

The API endpoints are protected using JWT and there's and enpoint to generate
access tokens. This feature is just to illustrate how it would be implemented. It
does not use any registered database to validate the submitted credentials.

Logs are formatted with Serilog and pushed to Elasticsearch.

The API exposes health checks endpoints to check the availability  
and the readyness of the API.

### Key cross-cutting concerns:

| Concern | Technology                                     |
|---|------------------------------------------------|
| Database (writes) | PostgreSQL with **Entity Framework Core**      |
| Database (reads) | PostgreSQL with **Dapper**                     |
| Caching / Idempotency | **Redis**                                      |
| Messaging | **RabbitMQ** with **MassTransit** using Outbox |
| Logging | **Serilog** pushes to **Elasticsearch**        |
| Authentication | **JWT Bearer** tokens                          |
| API documentation | **OpenAPI / Swagger** (non-production only)    |

---

## Architecture

The solution uses **Clean Architecture** with **CQRS** and **Vertical Slices** design patterns to
structure the application features. It the strict inward dependency rule of the layers: 
outer layers depend on inner layers, not the other way around.

Project structure
- **Presentation layer** 
  - WebAPi - HTTP, Minimal API endpoints, Swagger page, middlewares
- **Infrastructure** - EF Core, Dapper, Redis, RabbitMQ, Serilog, Elasticsearch
- **Application** and **Domain** - Features (CQRS), application interfaces, business logic, domain events, domain entities


### Why CQRS?

In an e-commerce applications, like on this coding challenge, read operation numbers greatly outnumbers write operations and must be extremely fast.
Write operations can have acceptable latency. CQRS makes this distinction explicit:

- **Commands** (writes) use **EF Core** (required) and domain events are dispatched via **MassTransit / RabbitMQ** using **Outbox** pattern. This means failed events can be retried asynchronously.
- **Queries** (reads) use **Dapper** for raw-SQL performance.

### Patterns in use

| Pattern | Purpose                                                                                                                                                                       |
|---|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Outbox** | Guarantees message delivery after a command succeeds (MassTransit with EF Core Outbox supported by PostgreSQL).                                                               |
| **Idempotency (Inbox-like)** | Write endpoints generate a user `fingerprint` based on the request content, invoked endpoint and user access token. The key is stored in Redis to prevent duplicate requests. |
| **Result / Error types** | Handlers return a `Result<T>` type instead of throwing exceptions for expected failures.                                                                                      |
| **Domain Events** | Entities raise strongly-typed domain events (`ProductCreatedEvent`, `InventoryOutOfStockEvent`, …) that are published via `IEventPublisher` (RabbitMQ) after committing data. |
| **Global Exception Middleware** | Unhandled exceptions are caught by `GlobalExceptionMiddleware` for logging and returned as `ProblemDetails`.                                                                  |

---

## Project Structure

```
ZeissCodingChallenge.sln
├── src/
│   ├── Core/
│   │   ├── Zeiss.Products.Domain/          # Entities, domain events, exceptions, value objects
│   │   └── Zeiss.Products.Application/     # Features (commands/queries), interfaces, results
│   ├── Infrastructure/
│   │   └── Zeiss.Products.Infrastructure/  # EF Core, Dapper, Redis, RabbitMQ, Serilog
│   └── Presentation/
│       └── Zeiss.Products.WebApi/          # Minimal API endpoints, Swagger, security, middlewares
└── tests/
    ├── Zeiss.Products.UnitTests/           # Unit tests for application and infrastructure
    └── Zeiss.Products.IntegrationTests/    # Integration tests
```

### `Zeiss.Products.Domain`

The innermost layer with **no external dependencies**.

| Folder | Contents                                                                                                |
|---|---------------------------------------------------------------------------------------------------------|
| `Entities/` | `Product`, `Inventory` — entities with the domain logic to change state and register domain events to raise. |
| `Events/` | 14 strongly-typed domain events (e.g. `ProductCreatedEvent`, `InventoryOutOfStockEvent`).               |
| `Exceptions/` | `DomainException` — thrown when an invariant is violated inside an entity.                              |
| `Common/` | Shared base types (`Entity<T>`, `DomainEvent`). |
| `Constants/` | Domain-level constants.   |
| `Extensions/` | Domain extension helpers.   |

### `Zeiss.Products.Application`

Orchestrates use-cases / features. Depends only on `Domain`.

| Folder | Contents                                                                                                                               |
|---|----------------------------------------------------------------------------------------------------------------------------------------|
| `Features/Products/Commands/` | `CreateProduct`, `UpdateProduct`, `DeleteProduct` — each folder contains the command, handler, validator and result type.              |
| `Features/Products/Queries/` | `GetProducts`, `GetProductById`, `SearchProducts`, `GetByStockLevel` — each folder contains the query, handler, result, and validator. |
| `Features/Inventories/Commands/` | `AddToStock`, `DecrementStock` - each folder contains the query, handler, result, and validator.                                       |
| `Interfaces/Repositories/` | `IProductRepository`, `IInventoryRepository`, `IUnitOfWork`.                                                                           |
| `Interfaces/Messaging/` | `IEventPublisher` - Abstraction for event publisher.                                                                                   |
| `Interfaces/Handlers/` | `IRequestHandler<TReq, TRes>`, `IRequestDispatcher` - Abstraction of a custom request dispatcher.                                      |
| `Interfaces/` | `IIdempotencyGuard` - Abstraction of the logic that uses distributed locking mechanism with Redis.                                     |
| `Results/` | `Result<T>`, `Error` strongly-type result structure that includes the operation result in case of success or an error                  |

### `Zeiss.Products.Infrastructure`

Implements all interfaces declared in `Application`. Depends on `Application` and `Domain`.

| Folder | Contents                                                                                                                        |
|---|---------------------------------------------------------------------------------------------------------------------------------|
| `Database/` | `PersistenceDbContext` (EF Core), `DbConnectionFactory` (Dapper), entity type configurations, `DbErrorInterceptor`, migrations. |
| `Repositories/` | `ProductRepository` (EF Core writes), `InventoryRepository` (EF Core writes), `ProductInventoryReadRepository` (Dapper reads).  |
| `Caching/` | `IdempotencyGuard` (Redis-backed), `CachingExtensions`.                                                                         |
| `Messaging/` | `RabbitMqEventPublisher`, MassTransit bus setup with PostgreSQL Outbox.                                                         |
| `Logging/` | Serilog configuration with Elasticsearch sink.                                                                                  |
| `HealthChecks/` | Health-check registrations (PostgreSQL, Redis, RabbitMQ, Elasticsearch).                                                        |
| `Builders/` | Infrastructure builder/extension helpers.                                                                                       |
| `Mappers/` | Infrastructure-level mapping utilities.                                                                                         |
| `Migrations/` | EF Core migration files + SQL script.                                                                                |

### `Zeiss.Products.WebApi`

The outermost layer. Includes Hosts for the HTTP server. Depends on `Application` and `Infrastructure`.

| Folder | Contents                                                                                          |
|---|---------------------------------------------------------------------------------------------------|
| `Endpoints/Products/` | Minimal API handlers for product CRUD operations.                                                 |
| `Endpoints/Inventories/` | Minimal API handlers for inventory operations.                                                    |
| `Endpoints/Tokens/` | JWT issuer endpoint.                                                                              |
| `Endpoints/HealthChecks/` | Health, liveness, and readiness check endpoints.                                                  |
| `Contracts/` | Request / response DTOs (`CreateProductRequest`, `UpdateProductRequest`, `PageRequest`, etc.).    |
| `Filters/` | `IdempotencyCheckFilter` — endpoint filter applied to write endpoints to prevent double requests. |
| `Middlewares/` | `GlobalExceptionMiddleware` - middleware that logs all unhandled exceptions                       |
| `Security/` | JWT bearer setup, `JwtSettings` - configurations for the security of the API                      |
| `Swagger/` | OpenAPI / Swagger configuration (disabled in production).                                         |
| `Mappers/` | `Result<T>` to `ApiResponse` mapping. ApiResponse structures resulting API messages.              |

---

## Domain Model

### `Product`

| Property | Type | Notes |
|---|---|---|
| `Id` | `long` | Auto-generated primary key. |
| `Name` | `string` | Required, non-empty. |
| `Sku` | `string` | Required, non-empty. Stock-Keeping Unit. |
| `Description` | `string?` | Optional. |
| `Price` | `decimal` | Must be > 0. |
| `IsActive` | `bool` | Defaults to `true` on creation. |
| `IsDeleted` | `bool` | Soft-delete flag. |
| `CreatedAt` | `DateTime` | UTC, set on creation. |
| `UpdatedAt` | `DateTime?` | UTC, set on each mutation. |
| `DeletedAt` | `DateTime?` | UTC, set on soft-delete. |

State transitions that raise domain events: `SetName`, `SetSku`, `SetDescription`, `SetPrice`, `Delete`.

#### Product ID generation
The API was designed to be scaled horizontally. In this situation to ensure Product ID is always unique
the generation logic was put at the database level. As per requirement Product ID are always 6-digits length. 
To achieve this, on the database, the values of the Product ID is designed as **Identity** with ranging from 100000 to 999999. 

### `Inventory`

| Property | Type | Notes |
|---|---|---|
| `Id` | `long` | Auto-generated primary key. |
| `ProductId` | `long` | Foreign key to `Product`. |
| `Quantity` | `int` | Available stock. Must be ≥ 0. |
| `CreatedAt` | `DateTime` | UTC. |
| `UpdatedAt` | `DateTime?` | UTC. |

State transitions: `Increment` (raises `InventoryChangedEvent` or `InventoryRestockedEvent`) and 
`Decrement` (raises `InventoryChangedEvent` or `InventoryOutOfStockEvent`).

### Domain Events

| Event | Trigger |
|---|---|
| `ProductCreatedEvent` | New `Product` constructed |
| `ProductUpdatedEvent` | Any product field changed |
| `ProductDeletedEvent` | `Product.Delete()` |
| `ProductRenamedEvent` | `Product.SetName()` |
| `ProductSkuChangedEvent` | `Product.SetSku()` |
| `ProductDescriptionChangedEvent` | `Product.SetDescription()` |
| `ProductPriceChangedEvent` | `Product.SetPrice()` |
| `ProductActivatedEvent` | Product re-activated |
| `ProductDeactivatedEvent` | Product deactivated |
| `InventoryTrackingStartedEvent` | First inventory record created |
| `InventoryChangedEvent` | Stock incremented / decremented (non-zero result) |
| `InventoryOutOfStockEvent` | Stock reaches 0 after decrement |
| `InventoryRestockedEvent` | Stock restocked from 0 |

---

## CQRS and Features

Each feature lives in its own self-contained folder under `Application/Features`. 
#### Note: even though MediatR could have been used as command/query dispatcher, as per personal preference,
I opted to create a lightweight custom `IRequestDispatcher` to trigger the execution of `IRequestHandler<TCommand, TResult>`.

### Commands (write path) request pipeline

```
HTTP Request
   → Minimal API Endpoint
   → IdempotencyCheckFilter (Redis lock via fingerprint calculation)
   → IRequestDispatcher.DispatchAsync<TCommand, TResult>()
   → IRequestHandler<TCommand, TResult>.HandleAsync()
   → Domain entity mutation + domain events raised
   → IUnitOfWork.CommitAsync() — EF Core saves and MassTransit Outbox enqueues events
   → MassTransit background worker publishes events to RabbitMQ
```

### Queries (read path) request pipeline

```
HTTP Request
   → Minimal API Endpoint
   → IRequestDispatcher.DispatchAsync<TQuery, TResult>()
   → IRequestHandler<TQuery, TResult>.HandleAsync()
   → ProductInventoryReadRepository (Dapper)
   → Returns paginated ProductInventoryReadModel
```

---

## Infrastructure

### Messaging — RabbitMQ Exchanges, Queues, and Topics
All RabbitMQ exchanges, queues and topics are created automatically at the application startup.
This is achieved by invoking the `rabbit.ConfigureEndpoints` when configuration RabbitMQ with MassTransit.

### Caching — Redis

- **Idempotency keys** are stored with a configurable TTL (`Redis:RecordRetentionInSeconds`, default 30 s).
- Distributed optimistic locking (`LockTakeAsync` / `LockReleaseAsync`) prevents concurrent duplicate processing of the same write request.

### Logging — Serilog and Elasticsearch

- All HTTP requests are logged with Serilog via `UseSerilogRequestLogging()`.
- Logs are pushed from Serilog to Elasticsearch and indexed as `ze-product-logs-{yyyy.MM.dd}`.
- Log level defaults: `Information` for the application, `Warning` for `Microsoft.*` and `System.*` namespaces.

---

## API Endpoints

The API endpoints for product and inventory are protected with a valid **JWT Bearer** token (see `/api/auth` below).  
The base path for products and inventory is `/api/products`.

### Authentication

| Method | Path | Auth | Description |
|---|---|---|---|
| `POST` | `/api/auth` | None | Issues a JWT Bearer token. |

**Request body:**

```json
{
  "secretId": "string",
  "secretKey": "string"
}
```

**Response `200 OK`:**

```json
{
  "token": "<jwt>",
  "expiration": "2026-08-19T05:00:00Z"
}
```

---

### Products

#### `GET /api/products`

Returns a paginated list of all products.

**Query parameters:**

| Parameter | Type | Default | Description |
|---|---|---------|---|
| `page` | `int` | `1`     | Page number (1-based). |
| `pageSize` | `int` | 100     | Number of items per page. |

**Response `200 OK`** — paginated array of `ProductInventoryReadModel`.

---

#### `GET /api/products/{id}`

Returns a single product by its 6-digit Product ID.

**Route parameter:** `id` — `int`, the product ID.

**Response `200 OK`** — single `ProductInventoryReadModel`.  
**Response `400 Bad Request`** — product not found or validation errors.

---

#### `GET /api/products/search`

Full-text search for products by name.

**Query parameters:**

| Parameter | Type | Required | Description |
|---|---|---|---|
| `name` | `string` | ✅ | Partial or full product name to search for. |
| `page` | `int` | ❌ | Page number (1-based). |
| `pageSize` | `int` | ❌ | Page size. |

**Response `200 OK`** — paginated array of matching products.

---

#### `GET /api/products/stock-level`

Returns products filtered by a stock quantity range.

**Query parameters:**

| Parameter | Type | Required | Description |
|---|---|---|---|
| `min` | `int` | ✅ | Minimum stock quantity (inclusive). |
| `max` | `int` | ✅ | Maximum stock quantity (inclusive). |
| `page` | `int` | ❌ | Page number (1-based). |
| `pageSize` | `int` | ❌ | Page size. |

**Response `200 OK`** — paginated array of products within the specified stock range.

---

#### `POST /api/products`

Creates a new product. A `fingerprint` cache key is created based on the access-token, request body, the requested endpoint, and HTTP method.

**Headers:**

| Header | Required | Description |
|---|---|---|
| `Authorization` | ✅ | `Bearer <token>` |

**Request body:**

```json
{
  "name": "string",
  "sku": "string",
  "description": "string or null",
  "price": 0.00
}
```

**Response `201 Created`** — `Location` header pointing to the new product URI.  
**Response `400 Bad Request`** — validation or business rule errors.

---

#### `PUT /api/products/{id}`

Updates an existing product.

**Route parameter:** `id` — `int`, the product ID.

**Request body:**

```json
{
  "name": "string",
  "sku": "string",
  "description": "string or null",
  "price": 0.00
}
```

**Response `202 Accepted`** — updated product data with `Location` header.  
**Response `400 Bad Request`** — product not found or validation errors.

---

#### `DELETE /api/products/{id}`

Soft-deletes a product.

**Route parameter:** `id` — `int`, the product ID.

**Response `204 No Content`** — product deleted successfully.  
**Response `400 Bad Request`** — product not found or already deleted.

---

### Inventory

Both inventory endpoints are under `/api/products/{id}` and require a `fingerprint` to be calculated for idempotency check.

#### `POST /api/products/{id}/add-to-stock/{quantity}`

Increments the stock for a product.

**Route parameters:**

| Parameter | Type | Description |
|---|---|---|
| `id` | `int` | Product ID. |
| `quantity` | `int` | Number of units to add. Must be > 0. |

**Headers:**

| Header | Required | Description |
|---|---|---|
| `Authorization` | ✅ | `Bearer <token>` |

**Response `200 OK`** — updated inventory data.  
**Response `400 Bad Request`** — product not found or validation/domain errors.

---

#### `POST /api/products/{id}/decrement-stock/{quantity}`

Decrements the stock for a product. Calculates `fingerprint` for idempotency check.

**Route parameters:**

| Parameter | Type | Description |
|---|---|---|
| `id` | `int` | Product ID. |
| `quantity` | `int` | Number of units to remove. Must be > 0 and ≤ current stock. |

**Headers:**

| Header | Required | Description |
|---|---|---|
| `Authorization` | ✅ | `Bearer <token>` |

**Response `200 OK`** - updated inventory data.  
**Response `400 Bad Request`** - product not found or validation / domain errors.

---

### Health Checks

| Method | Path | Auth | Description                                                                                           |
|---|---|---|-------------------------------------------------------------------------------------------------------|
| `GET` | `/health` | None | Basic liveness probe (always returns `Healthy`).                                                      |
| `GET` | `/health/live` | None | Liveness probe (always `Healthy`).                                                                    |
| `GET` | `/health/ready` | None | Readiness probe - checks dependencies tagged as `ready` (PostgreSQL, Redis, RabbitMQ, Elasticsearch). |

---

## Running the Application

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- EF Core tools: `dotnet tool install --global dotnet-ef`

### 1. Start the infrastructure services

From the repository root:

```bash
docker compose up -d
```

This starts:

| Service | Port |
|---|---|
| PostgreSQL 15 | `5432` |
| RabbitMQ 4 | `5672` |
| Elasticsearch 8.2 | `9200` |
| Redis 6 | `6379` |

### 2. Apply database migrations

Run from the `src/` directory:

```bash
dotnet ef database update \
  --project ./Infrastructure/Zeiss.Products.Infrastructure \
  --startup-project ./Presentation/Zeiss.Products.WebApi
```

> **Tip — CI/CD idempotent SQL script:**
> ```bash
> dotnet ef migrations script --idempotent \
>   --project ./Infrastructure/Zeiss.Products.Infrastructure \
>   --startup-project ./Presentation/Zeiss.Products.WebApi \
>   --output ./Infrastructure/Zeiss.Products.Infrastructure/Migrations/Migrations.sql
> ```

### 3. Review `appsettings.json`

The default configuration in `src/Presentation/Zeiss.Products.WebApi/appsettings.json` targets `localhost` for all services and matches the credentials already defined in `compose.yaml`. No changes are needed for a standard local run.

Key configuration sections:

```jsonc
{
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5432;Database=zeiss-products;Username=ze1ss;Password=ze1s<s>-pr0ds"
  },
  "Jwt": {
    "SecretKey": "---> your 40+ chars long secret key here <---",
    "Issuer": "Zeiss Products",
    "Audience": "zeiss-clients",
    "TokenExpirationMinutes": 15
  },
  "RabbitMq": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "ze1s",
    "Password": "ze-rabbit"
  },
  "Redis": {
    "ConnectionString": "localhost:6379,user=appuser,password=ze1ss<caching>-sync,Ssl=False,abortConnect=False",
    "RecordRetentionInSeconds": 30
  },
  "Elasticsearch": {
    "Uri": "http://localhost:9200"
  }
}
```

### 4. Run the API

```bash
cd src/Presentation/Zeiss.Products.WebApi
dotnet run
```

The application starts on `http://localhost:5056` by default. Swagger UI is available at `http://localhost:5056/swagger` in non-production environments.

### 5. (Optional) Run the full dependencies stack via Docker

```bash
docker compose up --build
```

### 6. Database seed
On the root of the solution, the `./data/` folder contains SQL scripts to seed the database:
Once the database has been lifted with `docker compose`, use the scripts below to seed the database.

| File                    | Description                               |
|-------------------------|-------------------------------------------|
| `01-products-seed.sql`  | Inserts 500 products for testing.         |
| `02-inventory-seed.sql` | Assigns random quantity for each product. |


### 7. Obtain a JWT token

```bash
curl -X POST http://localhost:5056/api/auth \
  -H "Content-Type: application/json" \
  -d '{ "secretId": "demo", "secretKey": "demo" }'
```

Copy the returned `token` and include it as `Authorization: Bearer <token>` on all subsequent protected requests.

---

## Tests

### Unit Tests — `Zeiss.Products.UnitTests`

```bash
dotnet test tests/Zeiss.Products.UnitTests
```

| Folder | What is tested |
|---|---|
| `Infrastructure/Caching/` | `IdempotencyGuard` — Redis lock acquisition, duplicate detection, value get/set. |
| `Infrastructure/Messaging/` | `RabbitMqEventPublisher` — event dispatch and error scenarios. |
| `Infrastructure/Repositories/` | `ProductInventoryReadRepository` (Dapper queries), `InventoryRepository` (EF Core). |
| `Application/` | Feature handlers and domain logic. |

### Integration Tests — `Zeiss.Products.IntegrationTests`

```bash
dotnet test tests/Zeiss.Products.IntegrationTests
```

Uses `WebApplicationFactory` to spin up the full HTTP pipeline in-process and test endpoint contracts end-to-end.

### Code Quality

```bash
# Verify formatting
dotnet format --verify-no-changes

# Build with analyzers (warnings as errors)
dotnet build --no-restore --warnaserror /p:RunAnalyzers=true /p:Configuration=Debug

# Run all tests
dotnet test
```
