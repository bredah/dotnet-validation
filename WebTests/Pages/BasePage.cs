using OpenQA.Selenium;

namespace WebTests.Pages;

public abstract class PageBase
{
    protected IWebDriver? Driver { get; }
    protected static string? BaseUrl => Environment.GetEnvironmentVariable("BASE_URL");

    protected PageBase(IWebDriver? driver)
    {
        Driver = driver;
    }

    /// <summary>
    /// Open the URL
    /// </summary>
    protected void Open(string path = "")
    {
        Driver.Navigate().GoToUrl(path);
    }

    public void SimulateType(IWebElement element, string value)
    {
        foreach (var c in value)
        {
            element.SendKeys(c.ToString());
            Thread.Sleep(100);
        }
    }
}