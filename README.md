# LCFila - Queue Managment

This project provides a robust Queue Management System (QMS) designed to handle multiple companies, provide UI customization, and incorporate advanced queue features like priority logic, real-time position tracking, and more.

> [!CAUTION]
> This project was originally developed from an abandoned client project. It was over-engineered with a lot of unnecessary complexity and features, and is not recommended for use in a production or product environment. While it has been adapted for inclusion in a portfolio, it may not follow best practices for scalability, performance, or maintainability. If you're considering using it, please review the code thoroughly and make the necessary adjustments before deploying to production.


## Features
- **Multiple Companies Support**

The system allows the management of queues for multiple companies simultaneously. Each company can have its own queue, and users can choose which queue to join.

- **Customizable UI**

UI elements are designed with flexibility in mind. Users can customize their interface to meet their preferences for a better experience. (Details on how to customize will be included in the documentation.)

- **Position Tracking and Exit Option**

Every user can see their current position in the queue. If they decide to leave the queue, they can do so directly from the same page.

- **Priority Logic**

The system includes a priority queue feature that allows certain individuals or types of requests to be prioritized over others. The priority logic can be customized based on user roles or other criteria.

- **Handling Multiple Queues Simultaneously**

The system supports managing many queues in parallel, ensuring efficient handling for large groups or multiple service points.

- **Scheduler Integration (Planned)**

The system is planned to integrate with a scheduler for time-based queue management, allowing automatic scheduling of services or sessions based on available resources and timings.

## Requirements
- .NET 8
- A modern web browser (for the frontend)
- Optional: Scheduler integration (planned feature, see below)

## Installation

### Clone the repository:
```bash
git clone https://github.com/luigicfilho/LCFila.git
```
### Navigate to the project folder:

```bash
cd LCFila
```

### Run the migrations to create database

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add Initial --startup-project .\LCFila.Web\LCFila.Web.csproj --project .\LCFila.Infra\LCFila.Infra.csproj
dotnet ef database update --project .\LCFila.Infra\LCFila.Infra.csproj -s .\LCFila.Web\LCFila.Web.csproj
dotnet ef migrations script -o ../fileName.sql --startup-project .\LCFila.Web\LCFila.Web.csproj --project .\LCFila.Infra\LCFila.Infra.csproj
```

### Run the development server:

```bash
dotnet run
```

## How It Works

### Architecture Overview
Here is a diagram illustrating the architecture of the Queue Management System:

```mermaid
---
title: LCFila Architecture Diagram
---
stateDiagram
    accTitle: Layers Diagram of LC Fila Application
    accDescr: Layers Diagram of LC Fila Application

    classDef notImplemented fill:white, font-style:italic

    LCFila.Web --> LCFila.Application 
    LCFila.Blazor:::notImplemented --> LCFila.Application
    LCFila.Application --> LCFila.Infra
    LCFila.Application --> LCFila.Domain
    LCFila.Infra --> LCFila.Domain

    note left of LCFila.Blazor
        This is not implemented.
        It's to ilustrate only (1).
    end note
```

The diagram above shows the major components and how they interact within the system, including:

- **LCFila.Web** - The frontend UI written in Razor MVC for user interaction.
- **LCFila.Blazor** - The optional, not implemented frontend UI written in Blazor for user interaction.
- **LCFila.Application** - The backend services.
- **LCFila.Infra** - The Infrastructure logic to connect to database and external services
- **LCFila.Domain** - The domain logic (e.g., queue management, priority logic).

You can check the ADRs:

- ADR-1: [Using MVC Web App](https://github.com/luigicfilho/LCFila/blob/main/docs/adr/adr-001-implement-LC-Fila-as-dotnet-mvc.md)
- ADR-2: [Adopt DDD With Layered Architecture](https://github.com/luigicfilho/LCFila/blob/main/docs/adr/adr-002-adopt-ddd-with-layered-architecture.md)

**1. Queue Creation for Multiple Companies**
- Each company can have its own queue.
- Users select which company’s queue they wish to join based on availability.

**2. UI Customization**
- Users can personalize their dashboard (color schemes, layout preferences).
- Admins or company owners can also customize their interface to match branding requirements.

**3. Position Tracking**
- Users can view their current position in the queue.
- Users are notified when their turn is approaching.
- An option is provided for users to leave the queue if needed.

**4. Priority Logic**
- Different users can have different priorities based on role (e.g., VIP customers or urgent service requests).
- The system processes priority users first, ensuring efficient queue management.

**5. Handling Multiple Lists**
- The system can handle and display several queues at the same time, enabling companies to manage different service points or types of requests simultaneously.

**6. Scheduler Integration (Planned)**
- Future versions will include a scheduler to manage time-based services automatically (e.g., appointments, reserved times, etc.).

## Usage

- For Customers:
    - Join a queue for a company.
    - Track your position in real time.
    - See estimated wait time.
    - Leave the queue at any time if desired.
- For Admins/Companies:
    - Set up and manage multiple queues.
    - Customize the UI for your company.
    - Manage customer positions and set priority rules.
    - Monitor active queues and their statuses.

## Planned Features
- Scheduler Integration
Automatically assign slots to users based on availability and timing.

- Mobile Optimization
Make the platform mobile-responsive for better usability across devices.

- Notifications
Implement push notifications to alert users when their turn is approaching or if there is an issue with the queue.

- Contributing
We welcome contributions! If you’d like to contribute to the Queue Management System, feel free to fork the repository and submit a pull request. Please make sure your contributions adhere to the project's coding standards.

## License
This project is licensed under the CC BY-NC-ND 4.0 - see the LICENSE file for details.

## Contact
If you have any questions or need further support, feel free to open an issue or reach out via discussion tab.
