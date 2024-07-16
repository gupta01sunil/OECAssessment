using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;
using TechTalk.SpecFlow;

namespace RL.AutomatedTests.Steps.Plan;

[Binding]
public class PlanSteps
{
    private readonly ScenarioContext _context;
    private readonly string _urlBase = "http://localhost:3001";
    private readonly TimeSpan _waitDurration = new(0, 0, 1);

    public PlanSteps(ScenarioContext context)
    {
        _context = context;
    }

    [Given("I'm on the start page")]
    public async Task ImOnTheStartPage()
    {
        var driver = _context.Get<IWebDriver>("driver");
        driver.Navigate().GoToUrl(_urlBase);
        var wait = new WebDriverWait(driver, _waitDurration);
        wait.Until(ExpectedConditions.UrlContains(_urlBase));
    }

    [When("I click on start")]
    public async Task IClickOnStart()
    {
        var driver = _context.Get<IWebDriver>("driver");
        driver.FindElement(By.Id("start")).Click();
        var wait = new WebDriverWait(driver, _waitDurration);
        wait.Until(ExpectedConditions.UrlMatches(_urlBase + "/plan"));
    }

    [Then("I'm on the plan page")]
    public async Task ImOnThePlanPage()
    {
        var driver = _context.Get<IWebDriver>("driver");
        var wait = new WebDriverWait(driver, _waitDurration);
        wait.Until(ExpectedConditions.UrlMatches(@"/plan/(\d+)"));
        Thread.Sleep(10000);
        driver.Url.Should().MatchRegex(@"/plan/(\d+)");
    }

    [Given("Select 2 procedures on the plan")]
    public async Task SelectingTwoProcedures()
    {
        var driver = _context.Get<IWebDriver>("driver");
        var wait = new WebDriverWait(driver, _waitDurration);
        driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div/div[2]/div/div/div/div/div[1]/div/div[1]/div")).Click();
        driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div/div[2]/div/div/div/div/div[1]/div/div[2]/div")).Click();
        
    }

    [When("Select a user from the user list")]
    public async Task SelectUserFromList()
    {
        var driver = _context.Get<IWebDriver>("driver");
        driver.FindElement(By.Id("react-select-3-placeholder")).Click();
        Actions mouseActions = new Actions(driver);
        mouseActions.MoveToElement(driver.FindElement(By.Id("react-select-3-placeholder"))).MoveByOffset(2, 3).Click();
    }

    [Then("Refresh the page and Validate Users")]
    public async Task RefreshAndValidate()
    {
        var driver = _context.Get<IWebDriver>("driver");
        driver.Navigate().Refresh();
        var actualtext = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div/div[2]/div/div/div/div/div[2]/div/div[1]/div[2]/div/div[1]/div[1]/div[1]")).Text;
        Assert.IsTrue(actualtext.Equals("Nick Morrison"), "User Name is not showing.");
    }
}