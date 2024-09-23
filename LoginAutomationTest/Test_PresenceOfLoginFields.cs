using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginAutomationTest
{
    [TestFixture]
    public class Test_PresenceOfLoginFields
    {
        ChromeDriver? driver;  // Marking driver as nullable

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();  // Initialize WebDriver in SetUp
            driver.Navigate().GoToUrl("https://crusader.bransys.com");
        }

        [Test]
        public void VerifyUsernameAndPasswordFieldsPresent()
        {
            // Validation of fields:  Email/Username i Password
            ClassicAssert.IsTrue(driver!.FindElement(By.XPath("//input[@id='input-204']")).Displayed, "Email/Username field is missing.");
            ClassicAssert.IsTrue(driver!.FindElement(By.XPath("//input[@id='input-207']")).Displayed, "Password field is missing.");
        }

        [Test]
        public void VerifySignInButtonIsPresent()
        {
            // Validation of field: Sign In button
            ClassicAssert.IsTrue(driver!.FindElement(By.XPath("//button[@type='submit']")).Displayed, "Sign In button is missing.");
        }

        [Test]
        public void VerifyForgotPasswordLinkIsPresent()
        {
            ClassicAssert.IsTrue(driver!.FindElement(By.XPath("//*[@id=\"app\"]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[2]/div[2]/button/span")).Displayed, "'Forgot Password' link is missing.");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}
