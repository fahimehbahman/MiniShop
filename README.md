# MiniShop

MiniShop is a full-stack e-commerce application built with **ASP.NET Core** and **React/TypeScript**, designed as a practical implementation of modern backend architecture, containerized development, authentication, testing, and observability.

The backend follows a layered architecture with clear separation between the API, application, domain, and infrastructure layers. The frontend is organized by features and communicates with the backend through a centralized API client.

The project also includes Docker-based deployment, CI workflows, reverse-proxy configuration, database migrations, monitoring, and logging infrastructure.

---

## Features

### Product Management

- Retrieve and manage products
- Product filtering and search
- Product API endpoints
- GraphQL queries and mutations
- Product persistence through repository abstractions

### Order Management

- Create and manage orders
- Support multiple order items
- Domain-level order entities
- Transactional persistence using Unit of Work

### Authentication & Users

- User authentication
- JWT-based token generation
- Role-based user model
- Authentication service abstraction
- Frontend authentication context and token management

### Pricing & Discounts

Pricing logic is implemented using the **Strategy Pattern**, allowing different pricing policies to be selected without coupling them to the core product logic.

Implemented strategies include:

- No discount
- Fixed-amount discount
- Percentage discount

### Search

The application contains dedicated search and embedding services, keeping search-related functionality separated from the core product services.

---

## Architecture

MiniShop follows a layered architecture inspired by **Clean Architecture** principles.

```text
                         ┌──────────────────────┐
                         │    React Frontend    │
                         │    TypeScript/Vite   │
                         └──────────┬───────────┘
                                    │
                                    │ HTTP
                                    ▼
                         ┌──────────────────────┐
                         │    MiniShop.Api      │
                         │ REST / GraphQL / JWT │
                         └──────────┬───────────┘
                                    │
                                    ▼
                    ┌──────────────────────────────┐
                    │     MiniShop.Application     │
                    │ Services / DTOs / Interfaces │
                    │ Mapping / Application Logic  │
                    └──────────────┬───────────────┘
                                   │
                     ┌─────────────┴─────────────┐
                     ▼                           ▼
          ┌────────────────────┐      ┌──────────────────────┐
          │  MiniShop.Domain   │      │ MiniShop.Infrastructure│
          │ Entities / Enums   │      │ EF Core / Repositories │
          │ Pricing Strategies │      │ Migrations / Security   │
          └────────────────────┘      └───────────┬───────────┘
                                                  │
                                                  ▼
                                             Database
```

### MiniShop.Domain

Contains the core domain model and business concepts.

```text
MiniShop.Domain/
├── Entities/
│   ├── Product.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   └── User.cs
│
├── Enums/
│   ├── DiscountType.cs
│   └── UserRole.cs
│
└── Pricing/
    ├── IPriceStrategy.cs
    ├── NoDiscountStrategy.cs
    ├── FixedAmountDiscountStrategy.cs
    └── PercentageDiscountStrategy.cs
```

The domain layer does not depend on infrastructure concerns such as database access or HTTP.

### MiniShop.Application

Contains application services, interfaces, DTOs, mapping, and use-case logic.

```text
MiniShop.Application/
├── Interfaces/
│   ├── IAuthService.cs
│   ├── IJwtTokenGenerator.cs
│   ├── IOrderRepository.cs
│   ├── IOrderService.cs
│   ├── IProductRepository.cs
│   ├── IProductService.cs
│   ├── IUnitOfWork.cs
│   ├── IUserRepository.cs
│   └── IUserService.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── EmbeddingService.cs
│   ├── OrderService.cs
│   ├── ProductService.cs
│   ├── SearchService.cs
│   └── UserService.cs
│
├── Mapping/
└── Product/
```

Repository and service interfaces keep application logic decoupled from concrete infrastructure implementations.

### MiniShop.Infrastructure

Implements persistence and security concerns.

```text
MiniShop.Infrastructure/
├── Data/
│   ├── ShopDbContext.cs
│   └── Configurations/
│
├── Repositories/
│   ├── ProductRepository.cs
│   ├── OrderRepository.cs
│   ├── UserRepository.cs
│   └── UnitOfWork.cs
│
├── Security/
│   └── JwtTokenGenerator.cs
│
└── Migrations/
```

Entity Framework Core migrations are included for database schema evolution and initial data setup.

### MiniShop.Api

Exposes the application functionality to clients.

```text
MiniShop.Api/
├── ProductsController.cs
├── OrdersController.cs
├── UsersController.cs
├── AuthController.cs
├── DocumentController.cs
│
├── GraphQL/
│   ├── query.cs
│   └── Mutation.cs
│
├── Contracts/
├── Mapping/
└── Program.cs
```

The API supports both traditional REST endpoints and GraphQL operations.

---

## Frontend

The frontend is implemented using **React and TypeScript** and organized around application features.

```text
minis-shop-ui/src/
│
├── app/
│   ├── apiClient.ts
│   ├── AppProviders.tsx
│   ├── ErrorBoundary.tsx
│   ├── PageLayout.tsx
│   ├── queryClient.ts
│   └── router.tsx
│
├── features/
│   ├── auth/
│   │   ├── api/
│   │   ├── context/
│   │   ├── pages/
│   │   └── services/
│   │
│   └── products/
│       ├── api/
│       ├── hooks/
│       └── pages/
│
├── shared/
│   ├── component/
│   └── hooks/
│
└── styles/
```

Authentication state is managed through a dedicated authentication context and token service.

Product functionality is isolated within its own feature module, including API access, hooks, and UI components.

---

## Testing

The frontend contains both unit and integration tests.

Examples include:

```text
useProducts.test.tsx
ProductListPage.integration.test.tsx
```

The project also includes Jest configuration and shared test setup:

```text
jest.config.ts
jest.setup.ts
```

This structure allows individual hooks as well as higher-level page behavior to be tested independently.

---

## API Design

MiniShop exposes functionality through ASP.NET Core controllers including:

```text
/api/products
/api/orders
/api/users
/api/auth
```

Request and response contracts are separated from domain entities through dedicated DTOs and API contracts.

Examples include:

```text
LoginRequest
CreateOrderRequest
CreateOrderItemDto
CreateOrderResponse
CreateProductRequest
ProductsRequest
```

This prevents transport-layer models from being directly coupled to domain entities.

---

## GraphQL

In addition to REST APIs, the backend contains GraphQL support through dedicated query and mutation definitions:

```text
MiniShop.Api/GraphQL/
├── query.cs
└── Mutation.cs
```

This provides an alternative interface for querying and modifying application data.

---

## Authentication

Authentication is implemented using JWT.

The authentication flow is separated across the application layers:

```text
Client
   │
   ▼
AuthController
   │
   ▼
AuthService
   │
   ▼
UserRepository
   │
   ▼
JwtTokenGenerator
   │
   ▼
JWT Token
```

The frontend stores and manages authentication state through its authentication context and token service.

---

## Persistence

Database access is implemented using **Entity Framework Core**.

The infrastructure layer contains:

- `ShopDbContext`
- Entity configurations
- Repository implementations
- Unit of Work
- EF Core migrations

Database schema changes are versioned through migrations.

The repository includes migrations for:

- initial schema creation
- additional product fields
- initial seed data

---

## Design Patterns

Several software design patterns and architectural practices are demonstrated in the project.

### Repository Pattern

Database operations are abstracted behind repository interfaces such as:

```text
IProductRepository
IOrderRepository
IUserRepository
```

### Unit of Work

`IUnitOfWork` coordinates persistence operations and provides a consistent transaction boundary.

### Strategy Pattern

Pricing and discount behavior is encapsulated using:

```text
IPriceStrategy
```

with separate implementations for different discount policies.

### Dependency Injection

Services and infrastructure implementations are registered through ASP.NET Core's dependency injection system, keeping components loosely coupled and testable.

---

## Docker

The project supports containerized development and production environments.

```text
docker-compose.dev.yml
docker-compose.prod.yml
```

Dockerfiles are provided for both the backend and frontend:

```text
MiniShop.Api/Dockerfile
minis-shop-ui/Dockerfile
```

A dedicated migration Dockerfile is also included:

```text
MiniShop.Api/Dockerfile.migration
```

This allows database migration execution to remain separate from normal API startup when required.

---

## Reverse Proxy

Nginx configuration is available under:

```text
nginx/nginx.conf
```

Nginx can be used as the entry point for containerized deployments and to route traffic between frontend and backend services.

---

## Observability

The repository includes monitoring and logging configuration.

```text
prometheus.yml
promtail-config.yaml
```

These configurations provide the foundation for collecting application metrics and forwarding logs in a containerized environment.

---

## CI/CD

GitHub Actions workflows are included separately for the backend and frontend:

```text
.github/workflows/
├── backend.yml
└── frontend.yml
```

This separation allows backend and frontend changes to be validated independently as part of the continuous integration process.

---

## Project Structure

```text
MiniShop/
│
├── MiniShop.Api/
│   ├── Contracts/
│   ├── GraphQL/
│   ├── Mapping/
│   ├── AuthController.cs
│   ├── OrdersController.cs
│   ├── ProductsController.cs
│   ├── UsersController.cs
│   └── Program.cs
│
├── MiniShop.Application/
│   ├── Interfaces/
│   ├── Mapping/
│   ├── Product/
│   └── Services/
│
├── MiniShop.Domain/
│   ├── Entities/
│   ├── Enums/
│   └── Pricing/
│
├── MiniShop.Infrastructure/
│   ├── Data/
│   ├── Migrations/
│   ├── Repositories/
│   └── Security/
│
├── minis-shop-ui/
│   ├── src/
│   │   ├── app/
│   │   ├── features/
│   │   ├── shared/
│   │   └── styles/
│   └── Dockerfile
│
├── .github/
│   └── workflows/
│
├── nginx/
│   └── nginx.conf
│
├── sql/
│   └── init.sql
│
├── docker-compose.dev.yml
├── docker-compose.prod.yml
├── prometheus.yml
├── promtail-config.yaml
└── MiniShop.sln
```

---

## Running the Project

### Prerequisites

Make sure the following tools are installed:

- .NET SDK
- Node.js and npm
- Docker and Docker Compose
- Git

### Clone the Repository

```bash
git clone <repository-url>
cd MiniShop
```

### Run with Docker

For the development environment:

```bash
docker compose -f docker-compose.dev.yml up --build
```

For the production configuration:

```bash
docker compose -f docker-compose.prod.yml up --build
```

### Run the Backend Locally

```bash
dotnet restore
dotnet build
dotnet run --project MiniShop.Api
```

### Run the Frontend Locally

```bash
cd minis-shop-ui
npm install
npm run dev
```

---

## Technology Stack

### Backend

- C#
- ASP.NET Core
- Entity Framework Core
- REST APIs
- GraphQL
- JWT Authentication

### Frontend

- React
- TypeScript
- Vite
- SCSS
- Jest

### Architecture & Engineering

- Clean Architecture principles
- Repository Pattern
- Unit of Work
- Strategy Pattern
- Dependency Injection
- DTO-based API contracts

### DevOps & Infrastructure

- Docker
- Docker Compose
- Nginx
- GitHub Actions
- Prometheus
- Promtail

---

## Purpose

MiniShop was built as a practical full-stack engineering project demonstrating how a modern web application can be structured beyond basic CRUD functionality.

The project focuses on:

- separation of concerns
- maintainable backend architecture
- domain-driven business logic
- API design
- authentication
- persistence abstractions
- frontend feature organization
- automated testing
- containerization
- CI/CD
- observability

It serves both as a functional e-commerce application and as a demonstration of production-oriented software engineering practices.

---

## Author

**Fahimeh Bahman**

Software Engineer  
C# / .NET | Distributed Systems | React / TypeScript | Cloud & DevOps
