# ApiShared

**ApiShared** is a reusable helper library for **API test automation** with **.NET 9**, providing **JSON Schema validation**, **HTTP API client helpers**, and **assertion extensions** for testing RESTful APIs such as [ReqRes](https://reqres.in).

## Libraries Used

| Purpose            | Package(s)                                          |
|--------------------|-----------------------------------------------------|
| JSON Parsing       | `Newtonsoft.Json`                                   |
| Schema Validation  | `JsonSchema.Net`                                    |
| HTTP Client        | `RestSharp`                                         |
| Test Assertions    | `Shouldly`, `xunit.assert`                          |

## Project Structure

```
ApiShared/
├── ApiShared.csproj
├── /Schemas/                      # JSON Schema files for response validation
│   ├── users-list-response.schema.json
│   └── users-list-response-error.schema.json
├── /Extensions/                   # Custom assertion helpers
│   └── JsonAssertionsExtensions.cs
├── /Api/
│   ├── JsonSchemaAssertions.cs    # Helpers for validating JSON payloads
│   ├── JsonSchemaValidator.cs     # Schema validator logic
│   └── /ReqRes/
│       ├── BaseApi.cs             # Shared API base class (e.g., base URL, client)
│       └── UsersApi.cs            # Example API wrapper for ReqRes endpoints
```

## Key Features

- **Reusable JSON Schemas:** Located under `/Schemas` for validating REST API responses against expected structure.
- **Fluent JSON Assertions:** Custom extensions for verifying payloads using `Shouldly` and `xunit`.
- **Base API Client:** `BaseApi` abstracts common HTTP client logic (built on `RestSharp`).
- **Example Endpoint Client:** `UsersApi` provides typed methods for interacting with ReqRes users endpoints.
- **Schema Assertion Helpers:** `JsonSchemaValidator` and `JsonSchemaAssertions` provide reusable validation logic for strict contract testing.

## Libraries Used

- `Newtonsoft.Json` → For parsing and manipulating JSON payloads.
- `JsonSchema.Net` → For validating JSON payloads against schemas.
- `RestSharp` → For sending HTTP requests.
- `Shouldly` → For clean, readable test assertions.
- `xunit.assert` → For xUnit integration.

## Usage

**Example: Validate response payload against a JSON Schema**

```csharp
var response = new UsersApi().GetUsers();
response.StatusCode.ShouldBe(HttpStatusCode.OK);

response.Content.ShouldMatchJsonSchema("users-list-response.schema.json");
```

`ShouldMatchJsonSchema` is provided via `JsonAssertionsExtensions`.

## Best Practices

- Store all reusable JSON Schemas in `/Schemas` for easy tracking.
- Keep API clients (`BaseApi`, `UsersApi`) isolated by domain.
- Use assertion extensions for consistent, readable validation.
- Integrate `ApiShared` in your test projects (e.g., `ApiTests`) to avoid duplication.

## How to Add to Other Projects

Reference `ApiShared` in your test project:

```bash
dotnet add reference ../ApiShared/ApiShared.csproj
```

Use it in your test classes for shared API helpers and JSON Schema validation.
