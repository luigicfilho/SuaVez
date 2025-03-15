# System Architecture Diagram

## Overview
The system is divided into the following components:

1. **Frontend (LCFila.Web)**:
   - Built using Razor MVC.
   - Handles user interaction and displays queue information.

2. **Backend (LCFila.Application)**:
   - Contains business logic and services.
   - Processes queue management and priority rules.

3. **Database (LCFila.Infra)**:
   - Manages data storage and retrieval.
   - Uses Entity Framework Core for database interactions.

## Diagram
```mermaid
stateDiagram
    accTitle: System Architecture Diagram
    accDescr: Diagram showing the interaction between Frontend, Backend, and Database.
    Frontend --> Backend: Sends requests
    Backend --> Database: Queries data
    Database --> Backend: Returns data
    Backend --> Frontend: Sends responses
Copy

---

#### **`design-patterns.md`**
```markdown
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
