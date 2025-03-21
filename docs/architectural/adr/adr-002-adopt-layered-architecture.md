# ADR: Adopt Layered Architecture

## Status
Accepted

## Context

Our application presently operates on a monolithic architecture, in which all elements—including the user interface, business logic, and data access—are closely combined into one cohesive unit. 

Although this method was effective during the initial phases of development, it has become progressively harder to sustain, expand, and evaluate as the application evolves. The lack of distinct separation among concerns has led to code that is more difficult to comprehend, adjust, and expand. 

Additionally, the monolithic architecture complicates the adoption of contemporary development techniques such as continuous deployment, microservices, and domain-driven design (DDD).

As our product evolves, it is crucial to guarantee that the architecture can expand, stay manageable, and fulfill increasing requirements. To tackle these issues, we have chosen to shift to a layered architecture. 

This change marks an essential move towards building a more modular and adaptable system, which will establish the foundation for future improvements, including the implementation of microservices or hexagonal architecture.

Transitioning to a layered architecture allows us to establish a more scalable and maintainable framework that can meet our changing requirements and facilitate the adoption of more sophisticated architectural approaches down the line

### Comparisons to Other Architectures
- Monolithic Architecture: In the monolithic approach, all components of the application are bundled together in one codebase. While simpler to develop initially and more straightforward for small projects, monolithic systems tend to become rigid as they scale. Changes in one part of the system can have wide-reaching impacts on others, making it more difficult to introduce new features, maintain the codebase, and scale the system.

- Layered Architecture (N-Layer): Layered architecture divides the application into distinct, well-defined layers, each with a specific responsibility. This separation of concerns makes the system easier to maintain, test, and scale. Layered architectures also allow for better separation of logic, making it easier to replace or upgrade individual components over time. This approach acts as a stepping stone toward even more advanced architectures, such as microservices, where individual services can later be decoupled for even more flexibility and scalability.

- Microservices: Microservices break down the application into loosely coupled, independently deployable services, which can be scaled and managed individually. However, transitioning directly to a microservices architecture from a monolith can be complex and resource-intensive. The layered approach allows us to modularize the system in preparation for a potential migration to microservices in the future.

## Conclusion
We will adopt a layered architecture as a first step toward improving the scalability, maintainability, and flexibility of our system. This transition will allow us to better handle future growth and evolve the system into more advanced architectural styles like microservices if needed.

* **Tight coupling:** Business logic is intertwined with UI and data access, making changes difficult and risky.
* **Reduced testability:** Lack of clear separation makes unit testing challenging.
* **Scalability issues:** Scaling specific components is not possible, requiring scaling the entire application.
* **Maintenance overhead:** Code becomes increasingly complex and difficult to understand, slowing down development.
* **Limited reusability:** Components are not easily reusable in other contexts.

We need to refactor our application to improve maintainability, testability, scalability, and reusability. We are considering a layered architecture as a first step towards a more modular and flexible system, by breaking down the system into these layers, we aim to achieve better separation of concerns, improved maintainability, and the ability to scale the system in a more modular fashion.

#### Conclusion
Layered architecture provides a structured approach to separating concerns and improving the overall architecture. It serves as a good stepping stone to evolve to more advanced architectures like microservices or hexagonal architecture.

## Decision

We will use layered architecture as the guiding principle for our architecture. Specifically, we will implement the following layers:

* **Web Layer (Web App):** This layer handles user interactions, request routing, and presentation logic. It interacts with the Application Layer. It is the outermost layer.
* **Application Layer:** This layer orchestrates use cases and interacts with the Domain Layer. It does not contain business logic itself but delegates to the Domain. It is technology agnostic.
* **Domain Layer:** This layer contains the core business logic, domain models (entities, value objects), and domain services. It is the heart of the application. It is technology agnostic.
* **Infrastructure Layer:** This layer provides concrete implementations for interfaces defined in the Domain and Application Layers. It handles persistence, messaging, external API integrations, and other technical concerns. It is technology specific.

### Consideration 1: Monolithic Architecture vs. Layered Architecture

* **Monolithic Single Layer:**
    * Everything is tightly coupled.
    * Difficult to test and maintain.
    * Scaling is challenging.
    * Fast initial development.
* **Layered Architecture:**
    * Separation of concerns.
    * Improved testability and maintainability.
    * Easier to scale specific layers.
    * Slightly longer initial development due to layer separation.

#### Conclusion

While monolithic architecture is simpler in the short term, layered architecture provides long-term benefits in terms of maintainability, scalability, and flexibility. It also aligns better with modern software development practices and prepares the application for future architectural evolution.

### Consideration 2: Layered Architecture as a Stepping Stone to Other Architectures

* **Layered Architecture:**
    * Provides a good foundation for modularity.
    * Separates business logic from infrastructure.
    * Can be further decomposed into smaller services or modules.
* **Microservices/Hexagonal Architecture:**
    * Highly decoupled and scalable.
    * Improved fault isolation.
    * Increased complexity in deployment and management.
    * Requires careful design and implementation.

#### Conclusion

Layered architecture is a practical and incremental step toward more advanced architectures like microservices or hexagonal architecture. It allows us to gradually decouple components and improve the application's design without introducing the full complexity of microservices or hexagonal architecture upfront. This approach reduces risk and provides a clear path for future evolution.

## Consequences

**Positive:**

*   **Improved Maintainability:** The separation of concerns between layers makes the application easier to maintain and understand.
*   **Enhanced testability:** Each layer can be tested independently, facilitating unit testing and integration testing.
*   **Increased scalability:** The modularity of the architecture allows for easier scaling in the future, as each layers can be scaled independently.
*   **Improved separation of concerns:** The application is easier to understand, maintain, and extend.
*   **Better alignment with domain-driven design (DDD) principles:** Enables us to focus on the core business logic.
*   **Increased flexibility:** in changing the presentation layer.
*   **Foundation for Future Evolution:** Prepares the application for future architectural evolution, such as microservices or hexagonal architecture.
*   **Better code organization:** Code is more organized and easier to understand.
*   **Facilitates future refactoring:** Layering makes it easier to transition to other architectures and reduce the risk of unintended side effects when making changes.

**Negative:**

*   **Initial Development Overhead:** Refactoring the current monolithic application into layers will require time and effort upfront.
*   **Potential for over-engineering:** for very simple applications.
*   **Requires discipline:** to enforce layer boundaries and avoid tight coupling between layers.
*   **Potential for Performance Overhead:** If layers are not designed carefully, there might be performance overhead, particularly in inter-layer communication
*   **Increased complexity:** Introducing layers adds some initial complexity.
*   **Increased development time:** Setting up the layered architecture may require more initial development time.

## Decision Date
2024-10-27
