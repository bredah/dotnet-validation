# .Net Validation

**dotnet-validation** is a consolidated automation solution using **.NET 9**, focused on **end-to-end testing**, **API contract testing**, and **shared automation libraries**.  
It combines UI testing with Selenium WebDriver, API testing with JSON Schema validation, and reusable helpers for cross-project consistency.

## Solution Structure

```
dotnet-validation/
├── ApiShared/       # Shared library for API helpers, JSON Schema validation, custom assertions
├── ApiTests/        # Automated API tests using xUnit, SpecFlow and RestSharp
├── WebShared/       # Shared Selenium configuration: cross-browser driver setup, .env loading
├── WebTests/        # Automated UI tests for Evernote, built on Selenium,Shouldly, xUnit
```

## Projects

### **ApiShared**

Reusable library for API tests:
- JSON Schema validation with `JsonSchema.Net`
- Fluent JSON assertions (`Shouldly` + custom extensions)
- Shared `BaseApi` clients for REST calls with `RestSharp`
- Example: `Schemas/` for reusable contract validation

### **ApiTests**

Automated tests for public APIs like [ReqRes](https://reqres.in):
- Unit/integration tests with **xUnit**
- BDD scenarios using **SpecFlow** (`.feature` + step definitions)
- JSON Schema contract checks

### **WebShared**

Reusable Selenium setup:
- Cross-browser support with `Selenium.WebDriver`
- Automatic driver downloads with `WebDriverManager`
- `.env` integration with `DotNetEnv`
- Shared `Driver` class to start local or remote sessions

### **WebTests**

Automated UI tests:
- Example: Evernote login, session, note operations
- Uses `Shouldly` for fluent assertions
- Structured with Page Objects & reusable components (`MenuComponent`, `NoteComponent`)


## Tech Stack

| Area               | Libraries                                                                |
| ------------------ | ------------------------------------------------------------------------ |
| Test Framework     | `xUnit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`           |
| BDD Support        | `SpecFlow.xUnit`, `SpecFlow.Tools.MsBuild.Generation` (in ApiTests)      |
| Assertions         | `Shouldly`                                                               |
| HTTP & JSON        | `RestSharp`, `Newtonsoft.Json`, `JsonSchema.Net` (in ApiShared)          |
| Browser Automation | `Selenium.WebDriver`, `Selenium.Support`, `WebDriverManager` (WebShared) |
| Env Config         | `DotNetEnv`                                                              |

## Running Tests

```bash
# Run API tests
dotnet test ApiTests

# Run Web tests
dotnet test WebTests
```

Ensure your `.env` files are configured where needed (e.g. credentials, browser).

## Continuous Integration (CI)

This repository includes a GitHub Actions pipeline defined in .github/workflows/ci.yml:

- Runs automatically on every push or pull_request to main.
- Can be triggered manually at any time using Run workflow in the Actions tab.
- Currently runs only ApiTests by default — WebTests are skipped for now due to captcha challenges.
- Ensures that API contract tests pass before merging.

Example manual run:

1. Go to Actions → .NET Validation CI
2. Click Run workflow
3. Select main (or any branch) and run on demand.

## Best Practices

- Use `ApiShared` and `WebShared` for reusable code — **keep DRY**.
- Store JSON Schemas in `ApiShared/Schemas` for API contract testing.
- Store browser settings and secrets in `.env` files, version `.env.example` only.
- Never store real credentials in the repo.
- For UI tests, run against non-production environments when possible.
