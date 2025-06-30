using System.Drawing;
using DotNetEnv;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WebShared;

public class Driver
{
    private Browser? _browser;

    public IWebDriver Start(Browser? browser = null, string address = "")
    {
        Env.Load();
        _browser = browser ?? GetBrowser();
        return !string.IsNullOrEmpty(address) ? Remote(address) : Local();
    }

    private static Browser? GetBrowser()
    {
        var browserName = Environment.GetEnvironmentVariable("BROWSER");
        return Enum.TryParse<Browser>(browserName, ignoreCase:true, out var browser) ? browser : Browser.Chrome;
    }

    /// <summary>
    /// Local this instance, use during the development
    /// </summary>
    /// <returns>Local instance driver</returns>
    private IWebDriver Local()
    {
        IWebDriver driver;
        
        switch (_browser)
        {
            case Browser.Chrome:
                new DriverManager().SetUpDriver(new ChromeConfig());
                var options = new ChromeOptions();
                options.AddExcludedArguments(new List<string>() { "enable-automation" });
                driver = new ChromeDriver(options);
                break;
            case Browser.Edge:
                new DriverManager().SetUpDriver(new EdgeConfig());
                driver = new EdgeDriver();
                break;
            case Browser.Firefox:
                new DriverManager().SetUpDriver(new FirefoxConfig());
                driver = new FirefoxDriver();
                break;
            case Browser.Safari:
                driver = new SafariDriver();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // Set window size
        driver.Manage().Window.Size = new Size(1280, 730);
        // Default timeout
        DefaultTimeout(driver);
        return driver;
    }

    /// <summary>
    /// Start the remote driver
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <returns>Remote instance driver</returns>
    private IWebDriver Remote(string address)
    {
        DriverOptions options;
        //TODO: Add support to SeleniumGrid
        switch (_browser)
        {
            case Browser.Chrome:
                options = new ChromeOptions();
                break;
            case Browser.Edge:
                options = new EdgeOptions();
                break;
            case Browser.Firefox:
                options = new FirefoxOptions();
                break;
            case Browser.Safari:
                options = new SafariOptions();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var driver = new RemoteWebDriver(new Uri(address), options);
        // Set window size
        driver.Manage().Window.Size = new Size(1280, 730);
        // Default timeout
        DefaultTimeout(driver);
        return driver;
    }

    /// <summary>
    /// Set the default timeout
    /// </summary>
    /// <param name="driver">Current driver</param>
    private static void DefaultTimeout(IWebDriver driver)
    {
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }
}