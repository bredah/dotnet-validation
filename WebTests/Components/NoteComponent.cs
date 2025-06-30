using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace WebTests.Components;

public class NoteComponent(IWebDriver? driver)
{
    private IWebElement _elementNodeTitle => driver.FindElement(By.CssSelector("en-noteheader textarea"));
    private IWebElement _elementNodeContent => driver.FindElement(By.CssSelector("[id=en-note]"));
    private IWebElement _elementNoteSidebar => driver.FindElement(By.CssSelector("div[id*=NOTES_SIDEBAR]"));
    private ReadOnlyCollection<IWebElement> _elementNotes => _elementNoteSidebar.FindElements(By.CssSelector("div[id*=NOTES_SIDEBAR]"));
    private IWebElement _elementNodeActions => driver.FindElement(By.CssSelector("button[id*=NOTE_ACTIONS]"));
    
    public void CreateNote(string title, string content)
    {
        _elementNodeTitle.Click();
        _elementNodeTitle.SendKeys(title);
        _elementNodeContent.Click();
        _elementNodeContent.SendKeys(content);
    }

    public int CountNotes()
    {
        return _elementNotes.Count;
    }

    public void DeleteNote(int index = 0)
    {
        _elementNotes[index].Click();
        _elementNodeActions.Click();
        driver.FindElement(By.CssSelector("a[id*=ACTION_DELETE]")).Click();
    }

}