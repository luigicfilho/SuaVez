# .NET C# Application Testing Guide

This document outlines the testing strategy for our .NET C# application. It covers how to run tests, understand test coverage, and add new tests.

## Testing Strategy

We employ a layered testing approach, including:

* **Unit Tests:** Focus on testing individual components (methods, classes) in isolation.
* **Integration Tests:** Verify the interaction between different components or services.
* **End-to-End (E2E) Tests:** Simulate real-user scenarios to test the entire application flow.

## Running Tests

We use xUnit for unit and integration tests.

### Running Unit Tests

1.  **Using Visual Studio:**
    * Open the solution in Visual Studio.
    * Navigate to `Test` > `Windows` > `Test Explorer`.
    * Click `Run All Tests` or select specific tests to run.

2.  **Using the .NET CLI:**
    * Open a terminal or command prompt.
    * Navigate to the test project directory (e.g., `YourProject.Tests`).
    * Run the following command:

    ```bash
    dotnet test
    ```

### Running Integration Tests

Integration tests are typically run similarly to unit tests, but they often require setting up external dependencies (databases, APIs). Ensure these dependencies are running before executing integration tests.

1.  **Using Visual Studio:**
    * Follow the same steps as for unit tests. Ensure integration test projects are included in the solution.

2.  **Using the .NET CLI:**
    * Navigate to the integration test project directory.
    * Run `dotnet test`.

### Running End-to-End (E2E) Tests

E2E tests often involve using tools like Playwright, Selenium, or similar frameworks. They are usually run from a dedicated test project.

1.  **Using the .NET CLI:**
    * Navigate to the E2E test project directory.
    * Run `dotnet test`. (Ensure the E2E test project is configured correctly).
2.  **Specific Frameworks:** Please consult the documentation of the specific E2E test framework you are using for more detailed instructions.

## Test Coverage Details

We use Code Coverage tools (e.g., built-in Visual Studio Code Coverage, or third-party tools like Coverlet) to measure the percentage of code covered by our tests.

### Generating Code Coverage Reports

1.  **Using Visual Studio:**
    * In the Test Explorer, click the dropdown next to `Run All Tests` and select `Analyze Code Coverage`.
    * Visual Studio will generate a code coverage report that shows the percentage of code covered by tests.

2.  **Using the .NET CLI with Coverlet:**
    * Add the `coverlet.collector` NuGet package to your test project:

    ```bash
    dotnet add package coverlet.collector
    ```

    * Run the tests with code coverage enabled:

    ```bash
    dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
    ```

    * This will produce a coverage.opencover.xml file, which can be viewed in tools like ReportGenerator.
    * To get a html report you can install ReportGenerator globally, and run the following command.

    ```bash
    dotnet tool install -g dotnet-reportgenerator-globaltool
    reportgenerator -reports:./coverage.opencover.xml -targetdir:./coverageReport
    ```

### Interpreting Code Coverage

* Aim for high code coverage (e.g., 80% or more) to ensure critical parts of the code are tested.
* Focus on covering complex logic, edge cases, and critical business functions.
* Review code coverage reports to identify areas that need more tests.

## Adding New Tests

1.  **Create a Test Project (if needed):**
    * For unit tests, create a new xUnit test project in your solution.
    * For integration or E2E tests, create appropriate test projects.

2.  **Add Necessary Dependencies:**
    * Add NuGet packages for xUnit, mocking libraries (e.g., Moq), and any other dependencies needed for your tests.

3.  **Create Test Classes and Methods:**
    * Create test classes for the classes you want to test.
    * Use the `[Fact]` attribute to mark test methods.
    * Use the `[Theory]` attribute to create data-driven tests.

4.  **Write Test Assertions:**
    * Use xUnit's `Assert` methods to verify the expected behavior of your code.
    * For mocking, use the mocking library to configure and verify interactions with dependencies.

5.  **Follow Test Naming Conventions:**
    * Use descriptive test names that clearly indicate what is being tested (e.g., `CalculateTotal_WithValidInput_ReturnsCorrectSum`).

### Example Unit Test (xUnit with Moq)

```csharp
using Xunit;
using Moq;

public class MyServiceTests
{
    [Fact]
    public void CalculateTotal_WithValidInput_ReturnsCorrectSum()
    {
        // Arrange
        var mockDependency = new Mock<IDependency>();
        mockDependency.Setup(d => d.GetValue()).Returns(10);
        var service = new MyService(mockDependency.Object);
        int input = 5;

        // Act
        int result = service.CalculateTotal(input);

        // Assert
        Assert.Equal(15, result);
        mockDependency.Verify(d => d.GetValue(), Times.Once); //Verify that the mock was called once.
    }
}

public interface IDependency
{
    int GetValue();
}

public class MyService
{
    private readonly IDependency _dependency;

    public MyService(IDependency dependency)
    {
        _dependency = dependency;
    }

    public int CalculateTotal(int input)
    {
        return input + _dependency.GetValue();
    }
}
