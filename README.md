# Product Management Application
English | [Français](README.fr.md)

## Online Test

[productmanagement-grhabed5f9hvhhej.canadacentral-01.azurewebsites.net](https://productmanagement-grhabed5f9hvhhej.canadacentral-01.azurewebsites.net)

## Overview
The **Product Management Application** is a web-based application built with ASP.NET Core 8.0, Blazor Server, and SQLite for managing products. It provides functionality to add, edit, delete, and list products with filtering and pagination. The application supports localization (English and French), integrates Azure AD for authentication, and includes unit tests for the product service.

## Features
- **Product Management**:
  - Add new products with name, description, and price.
  - Edit existing products.
  - Delete products.
  - List products with filtering by name and pagination.
- **Localization**:
  - Supports English (`en`) and French (`fr`) languages.
  - Localized UI elements using resource files.
- **Authentication**:
  - Azure Active Directory (Azure AD) integration for secure user authentication.
  - Role-based access control to restrict product management to authorized users.
- **Database**:
  - Uses SQLite as the database with Entity Framework Core.
  - Database seeding with sample data on startup.
- **Unit Tests**:
  - Comprehensive unit tests for the `ProductService` using xUnit, FluentAssertions, and an in-memory database.
- **Blazor Server**:
  - Interactive UI components for a responsive user experience.

## Prerequisites
To run the application, ensure you have the following installed:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (or later) or another IDE like Visual Studio Code
- [SQLite](https://www.sqlite.org/download.html) (optional, for viewing the database directly)
- An Azure AD tenant for authentication (configure in `appsettings.json`)

## Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd ProductManagementApp
   ```

2. **Configure Azure AD**:
   - Update the `appsettings.json` file in the `ProductManagementApp` project with your Azure AD configuration:
     ```json
     "AzureAd": {
       "Instance": "https://login.microsoftonline.com/",
       "Domain": "<your-domain>",
       "TenantId": "<your-tenant-id>",
       "ClientId": "<your-client-id>",
       "CallbackPath": "/signin-oidc"
     }
     ```
   - Ensure you have registered the application in Azure AD and granted necessary permissions.

3. **Set Up the Database**:
   - The application uses SQLite, and the database file (`ProductManagement.db`) is automatically created in the project directory.
   - Sample data is seeded on application startup via the `DbSeeder` class.

4. **Install Dependencies**:
   - Restore NuGet packages:
     ```bash
     dotnet restore
     ```

5. **Run the Application**:
   - Using Visual Studio:
     - Open the solution (`ProductManagementApp.sln`) in Visual Studio.
     - Set `ProductManagementApp` as the startup project.
     - Press `F5` or click "Start" to run the application.
   - Using the command line:
     ```bash
     cd ProductManagementApp
     dotnet run
     ```
   - The application will be available at `https://localhost:5001` or `http://localhost:5000` (depending on your configuration).

6. **Access the Application**:
   - Navigate to `/products` to view the product list.
   - Use `/add-product` to create a new product (requires authentication).
   - Use `/edit-product/{id}` to edit an existing product (requires authentication).
   - Sign in using Azure AD credentials to access protected features.

## Running Tests
The solution includes a test project (`ProductManagementTests`) with unit tests for the `ProductService`.

1. **Run Tests in Visual Studio**:
   - Open the solution in Visual Studio.
   - Open the **Test Explorer** (Test > Test Explorer).
   - Click **Run All Tests** to execute all unit tests.

2. **Run Tests via Command Line**:
   - Navigate to the test project directory:
     ```bash
     cd ProductManagementTests
     ```
   - Run the tests using the `dotnet test` command:
     ```bash
     dotnet test
     ```

3. **Test Coverage**:
   - The test project uses `coverlet.collector` to collect code coverage data.
   - To generate a coverage report:
     ```bash
     dotnet test --collect:"XPlat Code Coverage"
     ```
   - The coverage report will be generated in the `TestResults` directory.

## Project Structure
- **ProductManagementApp**:
  - Main web application project.
  - Contains Blazor components (`Products.razor`, `AddProduct.razor`, `EditProduct.razor`).
  - Configures services, middleware, and database context in `Program.cs`.
- **ProductManagementTests**:
  - Unit test project for testing the `ProductService`.
  - Uses xUnit, FluentAssertions, and an in-memory database for testing.
- **Key Files**:
  - `ProductService.cs`: Handles business logic for product operations.
  - `AppDbContext.cs`: Entity Framework Core context for SQLite database.
  - `Resources.resx`: Localization resource files for English and French.
  - `DbSeeder.cs`: Seeds the database with sample data.

## Localization
- The application supports English and French languages.
- Switch languages by setting the culture in the browser or via query parameters (e.g., `?culture=fr`).
- Localized strings are defined in `Resources.resx` (English) and `Resources.fr.resx` (French).

## Notes
- The application uses Blazor Server for interactivity. Ensure a stable server connection for optimal performance.
- The `@rendermode InteractiveServer` is commented out in Razor components to allow flexibility in render mode configuration.
- Ensure Azure AD is properly configured to avoid authentication issues.
- The SQLite database file (`app.db`) is created in the project root. Back up this file before making significant changes.

## Troubleshooting
- **Authentication Issues**: Verify Azure AD settings in `appsettings.json` and ensure your user has the correct permissions.
- **Database Issues**: Check that the SQLite connection string in `appsettings.json` is correct and the database file is accessible.
- **Test Failures**: Ensure the in-memory database is properly set up and no external dependencies are interfering.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any bugs or feature requests.

## License
This project is licensed under the MIT License.