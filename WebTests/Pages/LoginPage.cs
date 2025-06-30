using OpenQA.Selenium;

namespace WebTests.Pages;

public class LoginPage(IWebDriver? driver) : PageBase(driver)
{
    private IWebElement InputEmail => Driver!.FindElement(By.CssSelector("input[id=email]"));
    private IWebElement ButtonSubmit => Driver!.FindElement(By.CssSelector("button[type=submit]"));
    private IWebElement InputPassword => Driver!.FindElement(By.CssSelector("input[type=password]"));

    public void Open()
    {
        Open($"{BaseUrl}/login");
    }

    public void Login(string email = "", string password = "")
    {
        InputEmail.SendKeys(email);
        InputEmail.SendKeys(Keys.Enter);
        InputPassword.SendKeys(password);
        ButtonSubmit.Click();
    }
}