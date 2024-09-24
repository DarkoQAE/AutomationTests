using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LoginAutomationTest
{
    [TestFixture]
    public class Test_InputDataValidation
    {
        ChromeDriver? driver;
        WebDriverWait? wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://crusader.bransys.com/#/");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Initialize WebDriverWait
        }

       [Test]
        public void VerifyEmptyFieldsValidation()
        {
     // Locate the Sign In button when fields are empty
     IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[not(@disabled)]")));
     ClassicAssert.IsTrue(signInButton.Displayed, "Sign In button should be enabled when both fields are filled.");
        }
        [Test]
        public void VerifyInvalidUsername()
        {
            // Enter an invalid username and valid password
            IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
            usernameField.SendKeys("invalidusername");

            IWebElement passwordField = wait.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
            passwordField.SendKeys("validpassword");

            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@class='primary v-btn v-btn--is-elevated v-btn--has-bg theme--light v-size--default']//span[contains(text(),'SIGN IN')]")));
            signInButton.Click();

            // Check for error message with the updated text
            IWebElement errorMessage = wait.Until(drv => drv.FindElement(By.XPath("//div[contains(text(),'Incorrect email/username or password')]")));
            ClassicAssert.IsTrue(errorMessage.Displayed, "Error message for invalid username is missing.");
        }

        [Test]
        public void VerifyInvalidPassword()
        {
            // Enter a valid username and invalid password
            IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
            usernameField.SendKeys("validusername");

            IWebElement passwordField = wait.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
            passwordField.SendKeys("invalidpassword");

            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@class='primary v-btn v-btn--is-elevated v-btn--has-bg theme--light v-size--default']//span[contains(text(),'SIGN IN')]")));
            signInButton.Click();

            // Check for error message with the updated text
            IWebElement errorMessage = wait.Until(drv => drv.FindElement(By.XPath("//div[contains(text(),'Incorrect email/username or password')]")));
            ClassicAssert.IsTrue(errorMessage.Displayed, "Error message for invalid password is missing.");
        }

        [Test]
        public void VerifyBothFieldsInvalid()
        {
            // Enter both invalid username and invalid password
            IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
            usernameField.SendKeys("invalidusername");

            IWebElement passwordField = wait.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
            passwordField.SendKeys("invalidpassword");

            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@class='primary v-btn v-btn--is-elevated v-btn--has-bg theme--light v-size--default']//span[contains(text(),'SIGN IN')]")));
            signInButton.Click();

            // Check for error message with the updated text
            IWebElement errorMessage = wait.Until(drv => drv.FindElement(By.XPath("//div[contains(text(),'Incorrect email/username or password')]")));
            ClassicAssert.IsTrue(errorMessage.Displayed, "Error message for both invalid fields is missing.");
        }

        [Test]
        public void VerifyValidCredentials()
        {
            // Enter a valid username and valid password
            IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
            usernameField.SendKeys("validusername");

            IWebElement passwordField = wait.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
            passwordField.SendKeys("validpassword");

            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@class='primary v-btn v-btn--is-elevated v-btn--has-bg theme--light v-size--default']//span[contains(text(),'SIGN IN')]")));
            signInButton.Click();

            // Validate successful login by checking redirected URL or specific element
            ClassicAssert.IsTrue(driver!.Url.Contains("dashboard"), "Login with valid credentials failed.");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}
