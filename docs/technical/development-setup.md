# .NET C# Application Development Setup Guide

This document outlines the steps to set up your development environment for working on the .NET C# application.

## Prerequisites

Before you begin, ensure you have the following software installed:

1.  **Operating System:**
    * Windows 10/11 (Recommended)
    * macOS (with .NET SDK)
    * Linux (with .NET SDK)

2.  **.NET SDK:**
    * Download and install the latest .NET SDK from the official Microsoft website: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
    * Verify the installation by opening a terminal or command prompt and running: `dotnet --version`

3.  **Integrated Development Environment (IDE):**
    * **Visual Studio (Windows):**
        * Download and install Visual Studio Community, Professional, or Enterprise from: [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/)
        * Ensure you install the ".NET desktop development" and ".NET web development" workloads.
    * **Visual Studio Code (Cross-platform):**
        * Download and install Visual Studio Code from: [https://code.visualstudio.com/](https://code.visualstudio.com/)
        * Install the C# extension from the VS Code Marketplace.
    * **JetBrains Rider (Cross-platform):**
        * Download and install JetBrains Rider from: [https://www.jetbrains.com/rider/](https://www.jetbrains.com/rider/)

4.  **Git (Version Control):**
    * Download and install Git from: [https://git-scm.com/downloads](https://git-scm.com/downloads)
    * Configure your Git username and email:

    ```bash
    git config --global user.name "Your Name"
    git config --global user.email "[email address removed]"
    ```

5.  **Database (if applicable):**
    * If your application uses a database (e.g., SQL Server, PostgreSQL, MySQL), install the appropriate database server and client tools.
    * For local development, consider using Docker containers for database setup.

## Setting Up the Project

1.  **Clone the Repository:**
    * Open a terminal or command prompt.
    * Navigate to the directory where you want to clone the repository.
    * Run the following command, replacing `<repository_url>` with the actual repository URL:

    ```bash
    git clone <repository_url>
    ```

2.  **Open the Solution:**
    * **Visual Studio:** Open the `.sln` file in Visual Studio.
    * **Visual Studio Code:** Open the project folder in VS Code.
    * **JetBrains Rider:** Open the `.sln` file in Rider.

3.  **Restore NuGet Packages:**
    * **Visual Studio:** Visual Studio should automatically restore NuGet packages. If not, right-click on the solution in the Solution Explorer and select "Restore NuGet Packages."
    * **Visual Studio Code:** Open the terminal in VS Code and run: `dotnet restore`
    * **JetBrains Rider:** Rider should automatically restore NuGet packages. If not, right-click on the solution in the Solution Explorer and select "Restore NuGet Packages."

4.  **Configure Environment Variables:**
    * Create a `.env` file or use environment variables to store sensitive information (e.g., database connection strings, API keys).
    * You may also use `appsettings.Development.json` for development specific configurations.
    * Ensure to exclude `.env` from version control (add it to `.gitignore`).

5.  **Database Setup (if applicable):**
    * If your application uses a database, configure the connection string in your environment variables or `appsettings.Development.json`.
    * Run any database migrations or scripts to create the necessary database schema.

6.  **Build and Run the Application:**
    * **Visual Studio:** Press F5 to build and run the application.
    * **Visual Studio Code:** Open the terminal and run: `dotnet run` (from the project directory).
    * **JetBrains Rider:** Click the run button in Rider.

7.  **Run Tests:**
    * Follow the testing guide to run unit, integration, and end-to-end tests.

## Recommended Tools and Extensions

* **Visual Studio Code Extensions:**
    * C# (for C# development)
    * .NET Extension Pack
    * GitLens (for Git integration)
    * Debugger for Chrome or Edge (for debugging web applications)
    * Docker (for Docker integration)
* **Git GUI Clients:**
    * GitKraken
    * SourceTree
    * GitHub Desktop

## Best Practices

* **Use Git for Version Control:** Commit your code regularly and use meaningful commit messages.
* **Follow Coding Conventions:** Adhere to the project's coding conventions and style guidelines.
* **Write Unit Tests:** Write unit tests to ensure code quality and prevent regressions.
* **Use Dependency Injection:** Use dependency injection to make your code more testable and maintainable.
* **Handle Exceptions Properly:** Handle exceptions gracefully and provide meaningful error messages.
* **Keep Your Development Environment Clean:** Regularly update your tools and dependencies.
* **Use a Linter/Formatter:** Use dotnet format, or other linters to ensure code consistency.
* **Use Docker for Development:** If your application has many dependencies, consider using docker to containerize those dependencies.
