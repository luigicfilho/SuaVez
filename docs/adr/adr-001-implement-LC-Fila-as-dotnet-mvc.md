# ADR: Implementing LC Fila Management as a .NET MVC Application

## Status
Accepted

## Context
The LC Fila Management shall be responsible for creating companies, client of LC Fila, managing users of this company, that can be CompanyAdmin, CompanyEmployer, and creating and managing people in this queue. There were several ways through which the said service could have been implemented. These are as follows:
1. Implement it as a .NET MVC application.
2. Implement it as a microservice specific to us.
3. Using third-party identity provider.
4. Using other frameworks such as Razor Pages or Blazor,  
5. Implement on a separate front-end – backend architecture (SPA/MPA).
#### Conclusion
Based on the choices given above, we decided to implement the LC Fila Management as a .NET MVC application wherein the backend and the frontend would be on the same project.

## Decision
We chose to implement the LC Fila Management as a .NET MVC application for the following reasons:

1. **Separation of Concerns**: MVC offers a clear separation between the UI, business logic, and data management.

    **Well-Established Pattern**: MVC is a well-established architectural pattern that enforces a rigid separation of concerns between the user interface, or View; the business logic, or Controller; and the data management, or Model. This is ideal for an LC Fila Management where secure handling of user data and authentication logic is paramount.

2. **Team Expertise**: Our team is experienced with .NET MVC, enabling quicker implementation and easier maintenance.
    
    **Team Familiarity**: The development team has extensive experience with the .NET MVC framework; therefore, this reduces the learning curve for its adoption, thus allowing rapid implementation and maintenance. Since the team is familiar with MVC, it assures adherence to best practices to avoid common pitfalls.

3. **Framework Support**: ASP.NET Core MVC provides built-in support for authentication, authorization, and integration with ASP.NET Identity.
    
    **ASP.NET Core MVC**: The ASP.NET Core MVC framework provides out-of-the-box support for most features needed by an LC Fila Management, ranging from secure authentication and authorization to validation and model binding. It accelerates the development while enforcing security standards on the LC Fila Management.
    
    **Integration with Identity Framework**: ASP.NET Core Identity is pretty well integrated with MVC and provides basic identity functionality for features like user registration, password management, two-factor authentication, and integration with external login providers such as Google and Facebook. This makes it easier to provide a more secure and full-featured LC Fila Management.
4. **Security**: A centralized approach in MVC ensures consistent application of security policies and easier compliance with standards.

    **Centralized Security**: Being implemented as a .NET MVC application, the LC Fila Management itself will provide one-stop inclusion and management of authentication and authorization logic. The centralization assists in maintaining consistency in the application of security policies across the system.

    **Compliance and Auditing**: MVC's structured approach allows for comprehensive loggings, auditing, and mechanisms for access control that help in the compliance of industry standards such as GDPR and HIPAA. Since the service can easily be audited to meet the same regulatory requirements without much effort, that is a plus. 

5. **Maintainability**: MVC's structured codebase simplifies future enhancements and testing.
6. **Scalability**: The MVC architecture, combined with ASP.NET Core, supports both vertical and horizontal scaling.
7. **Flexibility**: MVC allows for easy customization of both back-end logic and front-end UI, providing flexibility for future enhancements.

    **Customizable User Interface**: While the main purpose of LC Fila Management is the back-end logic, the MVC pattern will also allow for a customizable user interface when needed-be it a login page or account management dashboards.

    **Future-Proofing**: The modular nature of the MVC architecture allows for future enhancements. Suppose there is a requirement to add new features, such as scheduling. In that case, the extensibility of the MVC framework is supportive of such enhancements with limited changes to the architecture.


### SPA/MPA Consideration
A separate SPA/MPA with a back-end API was considered for the following reasons:

- **Decoupling**: A SPA/MPA architecture would decouple the front-end and back-end, allowing for independent development and deployment of each layer.
- **UI Flexibility**: SPAs are often preferred for dynamic, rich user interfaces, and can provide a more responsive user experience compared to server-rendered views in MVC.
- **Technology Stack**: Using modern JavaScript frameworks (e.g., React, Angular, or Vue) could allow for more advanced front-end capabilities and better developer experience.

However, this approach was ultimately dismissed due to the following concerns:

1. **Increased Complexity**: Managing a separate front-end and back-end increases architectural complexity, requiring additional effort in handling cross-origin resource sharing (CORS), session management, and secure communication between the front-end and back-end.
2. **Security Risks**: Exposing the back-end API to the front-end introduces a larger attack surface, necessitating stricter security measures and monitoring to protect against potential vulnerabilities.
3. **Maintenance Overhead**: Maintaining two separate codebases for the front-end and back-end can lead to higher development and operational costs. This includes the need for separate testing, deployment pipelines, and versioning strategies to ensure compatibility between the front-end and back-end.
4. **Performance Considerations**: While SPAs can offer a more responsive UI, they might introduce additional latency due to the need for frequent API calls. In contrast, an integrated MVC application can handle some of these interactions server-side, potentially improving overall performance for the LC Fila Management.
5. **Redundancy for Identity Use Case**: Given that the identity service's primary function is secure user authentication and authorization, the dynamic features of a SPA/MPA are often unnecessary. MVC's built-in capabilities already address the needs of a typical identity service efficiently.

### Conclusion
While a SPA/MPA with a separate back-end could offer certain advantages, the added complexity, security challenges, and maintenance overhead outweigh the potential benefits for this specific use case. The .NET MVC approach, with its integrated structure, remains the preferred choice.


## Consequences
- **Positive**: Enhanced security, maintainability, and scalability.
- **Negative**: Potential monolithic structure and less UI flexibility compared to a SPA/MPA.

## Decision Date
February 9, 2025
