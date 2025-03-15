# Design Patterns

## Repository Pattern
- **Purpose**: Abstracts the data access logic and provides a cleaner way to interact with the database.
- **Implementation**: Used in `LCFila.Infra` to separate database operations from business logic.

## Dependency Injection
- **Purpose**: Promotes loose coupling and makes the system more testable and maintainable.
- **Implementation**: Configured in `Startup.cs` to inject services into controllers and other components.

## Layered Architecture
- **Purpose**: Separates concerns into distinct layers (e.g., Presentation, Application, Domain, Infrastructure).
- **Implementation**: Follows Domain-Driven Design (DDD) principles to organize the codebase.
