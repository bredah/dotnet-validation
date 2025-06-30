using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace WebShared;

public static class BrowserHelper {
    /// <summary>
    /// Capture a screenshot from current browser
    /// </summary>
    /// <param name="driver">Current driver</param>
    /// <returns>File path</returns>
    public static string TakeScreenshot (IWebDriver driver) {
        var fileName = String.Format (
            "{0}/{1}-{2}.png",
            Path.GetTempPath (),
            DateTime.UtcNow.ToString("yyyyMMddHHmmssfff",  CultureInfo.InvariantCulture),
            ((RemoteWebDriver) driver).Capabilities.GetCapability ("browserName"));
        var image = ((ITakesScreenshot) driver).GetScreenshot ();
        image.SaveAsFile (fileName);
        return fileName;
    }
}
