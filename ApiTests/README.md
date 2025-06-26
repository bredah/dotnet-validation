# API Test Suite

This repository contains automated tests for the public [ReqRes](https://reqres.in) API using **.NET 9**. It supports both traditional unit/integration testing using **xUnit** and behavior-driven development (BDD) with **SpecFlow (Gherkin syntax)**.

## Libraries Used

| Purpose            | Package(s)                                                                     |
|--------------------|---------------------------------------------------------------------------------|
| Test Framework     | `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`                 |
| BDD Support        | `SpecFlow.xUnit`, `SpecFlow.Tools.MsBuild.Generation`                          |
| Assertions         | `Shouldly` (used instead of FluentAssertions due to license restrictions)       |
| HTTP & JSON        | `RestSharp`, `Newtonsoft.Json`, `JsonSchema.Net`                              |

## Project Structure

```
ApiTests/
├── Features/                     # BDD tests (.feature)
│   ├── GetUsers.feature
│   └── Steps/
│       └── GetUserSteps.cs       # Step definitions
├── ReqRes/
│   └── UsersApiTest.cs           # xUnit test cases
├── ApiTests.csproj
```


## Test Execution

- Run All Tests

```bash
dotnet test
```

- Run Only xUnit Tests

```bash
dotnet test --filter "FullyQualifiedName~ApiTests.ReqRes"
```

-  Run Only BDD Tests (SpecFlow / Gherkin)

```bash
dotnet test --filter "FullyQualifiedName~ApiTests.Features"
```

## Notes

- **Schema Validation**: The project includes JSON Schema validation using `JsonSchema.Net`.
- **Assertions**: Uses `Shouldly` for expressive, human-readable assertions.
- **Gherkin Support**: `.feature` files define user-facing behaviors and expectations using plain English syntax.
- **Test Isolation**: Unit/integration tests and BDD tests are logically and structurally separated for clarity.
