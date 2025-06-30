using OpenQA.Selenium;

namespace WebTests.Components;

public class HomeComponent(IWebDriver? driver)
{
    private IWebElement _elementUserPortrait => driver.FindElement(By.CssSelector("div[id*=USER_PORTRAIT]"));
    private IWebElement _elementModal => driver.FindElement(By.CssSelector("div[id*=MODAL]"));
    private IWebElement _elemenLogount => _elementModal.FindElement(By.CssSelector("div[id*=LOGOUT]"));
    private IWebElement _elemenAllNotes => driver.FindElement(By.CssSelector("a[id*= NAV_ALL_NOTES]"));
    private IWebElement _buttonCreateNote => driver.FindElement(By.CssSelector("button[id*=SIDEBAR_CREATE_NOTE]"));

    public bool IsLoggedIn()
    {
        return _elementUserPortrait.Displayed;
    }

    public void OpenAllNotes()
    {
        _elemenAllNotes.Click();
    }

    public void CreateNote(string title, string content)
    {
        _buttonCreateNote.Click();
    }
    
    public void Logout()
    {
        _elementUserPortrait.Click();
        _elemenLogount.Click();
    }
}