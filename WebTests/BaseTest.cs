using DotNetEnv;
using OpenQA.Selenium;
using WebShared;

namespace WebTests;

public class BaseTest : IDisposable
{
    public IWebDriver? Driver { get; }

    public BaseTest()
    {
        Env.Load();
        Driver = new Driver().Start();
    }

    public void Dispose()
    {
        Driver?.Quit();
    }
}