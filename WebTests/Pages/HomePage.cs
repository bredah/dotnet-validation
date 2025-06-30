using OpenQA.Selenium;

namespace WebTests.Pages;

public class HomePage(IWebDriver? driver) : PageBase(driver)
{
    public void Open(bool acceptAllCookies = false)
    {
        Open($"{BaseUrl}/");
        if (acceptAllCookies)
            AcceptCookies();
        else
            RejectCookies();
        AddCookies();
    }


    private void RejectCookies()
    {
        var jsExecutor = (IJavaScriptExecutor)driver;
        jsExecutor.ExecuteScript(
            "localStorage.setItem('cookie-preferences', '{\"analytics-cookies\":false,\"profiling-cookies\":false}');");
        jsExecutor.ExecuteScript("localStorage.setItem('cookie-notice-accepted-version', '1');");
        jsExecutor.ExecuteScript(
            "localStorage.setItem('consentMode', '{\"ad_storage\":\"denied\",\"ad_user_data\":\"denied\",\"ad_personalization\":\"denied\",\"analytics_storage\":\"denied\",\"functionality_storage\":\"granted\",\"personalization_storage\":\"denied\",\"security_storage\":\"granted\"}');");
    }

    private void AcceptCookies()
    {
        var jsExecutor = (IJavaScriptExecutor)driver;
        jsExecutor.ExecuteScript(
            "localStorage.setItem('cookie-preferences', '{\"analytics-cookies\":true,\"profiling-cookies\":true}');");
        jsExecutor.ExecuteScript("localStorage.setItem('cookie-notice-accepted-version', '1');");
        jsExecutor.ExecuteScript(
            "localStorage.setItem('consentMode', '{\"ad_storage\":\"granted\",\"ad_user_data\":\"granted\",\"ad_personalization\":\"granted\",\"analytics_storage\":\"granted\",\"functionality_storage\":\"granted\",\"personalization_storage\":\"granted\",\"security_storage\":\"granted\"}');");
    }

    private void AddCookies()
    {
        
        driver.Manage().Cookies.AddCookie(new Cookie(
            name: "userdata_accountType",
            value: "FREE",
            domain: ".evernote.com",
            path: "/",
            expiry: DateTime.Now.AddDays(1)
        ));
        driver.Manage().Cookies.AddCookie(new Cookie(
            name: "userdata_acctCreatedTime",
            value: "1317617883000",
            domain: ".evernote.com",
            path: "/",
            expiry: DateTime.Now.AddDays(1)
        ));
        driver.Manage().Cookies.AddCookie(new Cookie(
            name: "userdata_lastLoginTime",
            value: "1751311628123",
            domain: ".evernote.com",
            path: "/",
            expiry: DateTime.Now.AddDays(1)
        ));
        driver.Manage().Cookies.AddCookie(new Cookie(
            name: "userdata_loggedIn",
            value: "true",
            domain: ".evernote.com",
            path: "/",
            expiry: DateTime.Now.AddDays(1)
        ));
    }
}