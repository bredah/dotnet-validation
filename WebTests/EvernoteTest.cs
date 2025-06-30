using OpenQA.Selenium;
using Shouldly;
using WebTests.Components;
using WebTests.Pages;

namespace WebTests;

public class EvernoteTest: IClassFixture<BaseTest>
{
    private readonly IWebDriver? _driver;
    private LoginPage LoginPage => new(_driver!);
    private HomePage HomePage => new(_driver!);
    private MenuComponent MenuComponent => new(_driver!);
    private NoteComponent NoteComponent => new(_driver!);

    private string AccountPassword => Environment.GetEnvironmentVariable("ACCOUNT_PASSWORD") ?? "";
    private string AccountEmail => Environment.GetEnvironmentVariable("ACCOUNT_EMAIL") ?? "";

    public EvernoteTest(BaseTest baseTest)
    {
        _driver = baseTest.Driver;
        HomePage.Open();
    }
    
    [Fact]
    public void Login_WithValidCredentials_ShouldSucceed()
    {
        LoginPage.Open();
        LoginPage.Login(AccountEmail, AccountPassword);
        MenuComponent.IsLoggedIn().ShouldBe(true);
    }
}