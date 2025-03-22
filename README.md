# NUnitPlaywrightFramework

## Overview
NUnitPlaywrightFramework is a test automation framework that leverages NUnit for test management and Playwright for browser automation. This framework is designed to facilitate the creation and execution of end-to-end tests for web applications.

## Features
- Parallel test execution using NUnit
- Browser automation with Playwright
- Configuration management using User Secrets
- Utility methods for common actions and string manipulations

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or later

## Getting Started

### Clone the Repository

```console
git clone https://github.com/chinmaymudholkar/NUnitPlaywrightFramework.git
```

### Install Playwright

```console
cd NUnitPlaywrightFramework
pwsh .\NUnitPlaywrightFramework\bin\Debug\net8.0\playwright.ps1 --install-deps
```

### Build the Project
Open the solution in Visual Studio, build the project to restore the NuGet packages, and compile the code.

## Running Tests
You can run the tests using the Test Explorer in Visual Studio or by using the .NET CLI:

```console
dotnet test
```

## Project Structure
- **NUnitPlaywrightFramework/Tests/Tests.cs**: Contains the test cases for the application.
- **NUnitPlaywrightFramework/Libs/Wrappers.cs**: Contains utility methods for common actions and string manipulations.
- **NUnitPlaywrightFramework/Libs/FrameworkActions.cs**: Contains methods to perform actions on web elements.
- **NUnitPlaywrightFramework/Libs/GetAttributes.cs**: Contains methods to retrieve attributes and text content from web elements.

## Example Test Cases
### HomepageHasSwagLabsInTitle
Verifies that the homepage title contains "Swag Labs".

### LoginWithInvalidCredentials
Tests login functionality with various invalid credentials and verifies the error message.

### LoginWithValidCredentials
Tests login functionality with valid credentials and verifies the successful login by checking the page heading.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact
For any questions or issues, please open an issue on GitHub or contact the repository owner.

