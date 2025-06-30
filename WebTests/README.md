# Web Test Suite

This repository contains automated UI tests for the **Evernote web application**, built with **.NET 9**, **xUnit**, **Selenium**, and **Shouldly**.  
It is designed to provide clean, maintainable, and reusable end-to-end tests.

## Libraries Used

| Purpose            | Package(s)                                                            |
|--------------------|------------------------------------------------------------------------|
| Test Framework     | `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`        |
| Assertions         | `Shouldly`                                                            |
| Web Automation     | `Selenium.WebDriver`, `Selenium.Support`, `WebDriverManager` (via `WebShared`) |

The solution also includes a **`WebShared`** project that provides:
- Shared drivers (Selenium setup)
- Common page base classes (`BasePage`)
- Browser configuration and helper utilities

## Project Structure

```
WebTests/
├── BaseTest.cs                   # xUnit fixture to manage WebDriver lifecycle
├── EvernoteTest.cs               # Example test cases
├── .env                          # Environment configuration (local only)
├── WebTests.csproj
├── /Components/                  # Shared UI components
│   ├── MenuComponent.cs
│   ├── NoteComponent.cs
│   └── HomeComponent.cs
└── /Pages/                       # Page objects
    ├── BasePage.cs
    ├── LoginPage.cs
    ├── HomePage.cs
```

---

## Test Execution

Run all tests using the **.NET test runner**:

```bash
dotnet test
```

Tests are implemented using **xUnit** and `Shouldly` for expressive, human-readable assertions.

## Environment Variables

Tests require credentials and configuration to run correctly.  
The project uses a `.env` file in the project root to define:

```env
ACCOUNT_EMAIL=your-email@example.com
ACCOUNT_PASSWORD=your-password
BROWSER=Chrome
```

## Captcha Limitation

**Important:**  
The Evernote login flow is protected by **hCaptcha** (`https://api.hcaptcha.com`).  
This means that **automated login will fail**, because Selenium cannot solve or bypass `hCaptcha`.  
When the captcha is triggered, the backend always returns an **incorrect password error**, even if credentials are valid.

## Notes

- `BaseTest` manages WebDriver startup and cleanup for all tests.
- `BasePage` provides shared browser helpers and common selectors.
- `MenuComponent`, `NoteComponent`, `HomeComponent` contain reusable UI actions.
- `LoginPage` automates the login form (except for `hCaptcha`).
- `WebShared` provides the common driver setup, browser options, and utilities shared across projects.

---

## Best Practices

- Keep your `.env` out of version control.
- Extend page objects with robust `WebDriverWait` and clear assertions.
- Use screenshots or logging for debugging failures.
- Avoid testing the login flow on production with Selenium due to captcha.
