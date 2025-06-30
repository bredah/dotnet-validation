# WebShared

**WebShared** is a reusable **browser automation library** for **Selenium WebDriver** projects using **.NET 9**.  
It provides a clean, shared setup for **cross-browser WebDriver initialization**, environment configuration, and helper utilities that can be used in your test suites (like `WebTests`).

## Libraries Used

| Purpose            | Package(s)                          |
|--------------------|-------------------------------------|
| Browser Automation | `Selenium.WebDriver`, `Selenium.Support` |
| Driver Management  | `WebDriverManager`                  |
| Env Configuration  | `DotNetEnv`                         |

## Project Structure

```
WebShared/
├── WebShared.csproj
├── Browser.cs               # Enum for supported browsers (Chrome, Edge, Firefox, Safari)
├── BrowserHelper.cs         # Optional helper logic for browser config
├── Driver.cs                # Core WebDriver setup and local/remote driver logic
```

## Key Features

- **Cross-Browser Support:** Easily switch between Chrome, Edge, Firefox, or Safari using the `Browser` enum.
- **Automatic Driver Management:** Uses `WebDriverManager` to automatically download the correct driver binaries.
- **.env Integration:** Loads browser configuration and other variables from a `.env` file using `DotNetEnv`.
- **Flexible Driver:** Supports both local and remote WebDriver sessions.
- **Reusable Helper:** Designed to be imported by other projects like `WebTests` for DRY test setup.

## Environment Variables

You can configure your default browser or remote WebDriver address via a `.env` file:

```env
BROWSER=Chrome
REMOTE_URL=http://localhost:4444/wd/hub
```

## Example Usage

In a test project like `WebTests`:

```csharp
DotNetEnv.Env.Load(); // load .env

var driver = new Driver().Start(); // uses browser from .env or fallback

// or specify explicitly:
var driver = new Driver().Start(Browser.Chrome);
```

## Key Classes

| File            | Purpose                                                      |
|-----------------|--------------------------------------------------------------|
| `Driver.cs`     | Main entry point for starting local or remote WebDriver.     |
| `Browser.cs`    | Enum for supported browsers.                                 |
| `BrowserHelper` | Optional helper utilities for extra browser configuration.   |


## How to Use in Other Projects

Reference `WebShared` in your test project:

```bash
dotnet add reference ../WebShared/WebShared.csproj
```

## Best Practices

- Always load your `.env` **before** starting the driver.
- Use the `Driver` class to manage browser startup consistently.
- Pair with `WebDriverWait` and explicit waits for robust test automation.
