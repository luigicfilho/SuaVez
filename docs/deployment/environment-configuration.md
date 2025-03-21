# Environment Configuration Guide for C# .NET Application
This guide explains how to configure different environments (e.g., Development, Staging, Production) for a C# .NET application. It covers environment variables, configuration files, and best practices.

## Overview
In .NET, environments are typically configured using:

- **Environment Variables:** For sensitive or environment-specific settings.
- **Configuration Files:** For non-sensitive settings (e.g., appsettings.json).
- **Launch Settings:** For local development.

## Step 1: Set Up Environment-Specific Configuration Files
Default Configuration File:

appsettings.json: Contains default settings for all environments.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "AppSettings": {
    "SomeKey": "DefaultValue"
  }
}
```
Environment-Specific Configuration Files:

appsettings.Development.json:

```json
{
  "AppSettings": {
    "SomeKey": "DevelopmentValue",
    "DatabaseConnection": "Server=localhost;Database=DevDB;User Id=devuser;Password=devpassword;"
  }
}
```
appsettings.Staging.json:

```json
{
  "AppSettings": {
    "SomeKey": "StagingValue",
    "DatabaseConnection": "Server=staging-db;Database=StagingDB;User Id=staginguser;Password=stagingpassword;"
  }
}
```
appsettings.Production.json:

```json
{
  "AppSettings": {
    "SomeKey": "ProductionValue",
    "DatabaseConnection": "Server=prod-db;Database=ProdDB;User Id=produser;Password=prodpassword;"
  }
}
```

## Step 2: Configure Environment Variables
Set Environment Variables:

Use environment variables for sensitive data (e.g., connection strings, API keys).

Example:

```bash
export ASPNETCORE_ENVIRONMENT=Development
export ConnectionStrings__DefaultConnection="Server=localhost;Database=DevDB;User Id=devuser;Password=devpassword;"
```

Environment-Specific Variables:

Use .env files for local development (via tools like dotenv).

Example .env file:

```env
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=Server=localhost;Database=DevDB;User Id=devuser;Password=devpassword;
AppSettings__SomeKey=DevelopmentValue
```

## Step 3: Configure Launch Settings for Local Development
launchSettings.json:

Located in the Properties folder of your project.

Defines environment-specific settings for local debugging.

Example:

```json
{
  "profiles": {
    "Development": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Staging": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging"
      }
    },
    "Production": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Production"
      }
    }
  }
}
```

## Step 4: Configure Environments in Cloud Platforms

### Azure
App Service Configuration:

Go to your App Service in the Azure Portal.

Navigate to Configuration > Application Settings.

Add environment variables:

```
ASPNETCORE_ENVIRONMENT = Production
ConnectionStrings__DefaultConnection = Server=prod-db;Database=ProdDB;User Id=produser;Password=prodpassword;
AWS
```
Elastic Beanstalk or ECS Configuration:

Go to the AWS Management Console.

Navigate to Elastic Beanstalk > Environment > Configuration > Software or ECS > Task Definition > Environment Variables.

Add environment variables:

```
ASPNETCORE_ENVIRONMENT = Production
ConnectionStrings__DefaultConnection = Server=prod-db;Database=ProdDB;User Id=produser;Password=prodpassword;
```

## Step 5: Best Practices

- Use Secret Manager for Local Development:
  - Store sensitive data securely using the .NET Secret Manager:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=DevDB;User Id=devuser;Password=devpassword;"
```

- Avoid Hardcoding Sensitive Data:
  - Never hardcode secrets in configuration files or code.
- Use Configuration Overrides:
  - Override configuration values using environment variables for flexibility.
- Validate Configuration:
  - Use the Options pattern to validate configuration settings
