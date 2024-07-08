# Blog Management Application (Backend)

This is the backend API for the Blog Management Application developed using .NET 6. The API provides endpoints for creating, retrieving, and deleting blog posts. It also includes custom error handling and logging using middleware.

## Setup Instructions

### Prerequisites

- .NET 6 SDK: [Download and install](https://dotnet.microsoft.com/download/dotnet/6.0)
- A code editor or IDE, such as Visual Studio or Visual Studio Code

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/SomilRavrani79/BlogManagementApp_BE.git
    cd your-repository-name
    ```

2. Restore the .NET dependencies:

    ```bash
    dotnet restore
    ```

## How to Run the Application

1. Build the project:

    ```bash
    dotnet build
    ```

2. Run the backend application:

    ```bash
    dotnet run
    ```

3. The API will be available at `[https://localhost:7182/]`.

## Design Decisions

- **Clean Architecture**: The application is built using a clean architecture approach, separating concerns into different layers such as Controllers, Services, and Middleware.
- **Error Handling Middleware**: A custom middleware is implemented to handle exceptions and log errors, ensuring a consistent error response format.
- **Logging**: Logging is implemented at both the controller and service levels to track the application flow and diagnose issues effectively.
- **Dependency Injection**: Dependency injection is used throughout the application to manage dependencies, making the code more testable and maintainable.
- **Unit Testing**: xUnit and Moq are used for unit testing the application, ensuring that both the service and controller layers are tested independently.

### Application Structure

```plaintext
BlogManagementApp/
├── Controllers/
│   ├── BlogController.cs
├── Middleware/
│   ├── ErrorHandlingMiddleware.cs
├── Models/
│   ├── BlogPost.cs
│   ├── GenericResponse.cs
├── Services/
│   ├── BlogService.cs
│   ├── IBlogService.cs
├── Program.cs
├── appsettings.json
├── BlogManagementApp.csproj

BlogManagementTest/
├── Controllers/
│   ├── BlogControllerTests.cs
├── Services/
│   ├── BlogServiceTests.cs
├── BlogManagementTest.csproj
```
