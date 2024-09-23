using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LoginAutomationTest
{
    [TestFixture]
    public class Test_InvalidErrorMessage
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

        // Test 1: Verify that an error message is displayed when the Email/Username field is empty
        [Test]
        public void VerifyEmptyUsernameField()
        {
            // Leave the username field empty and enter a password
            IWebElement passwordField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
            passwordField.SendKeys("validpassword");

            // Check if Sign In button is disabled
            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[contains(@class,'v-btn--disabled')]")));
            ClassicAssert.IsTrue(signInButton.Displayed, "Sign In button should be disabled when the username field is empty.");
        }

        // Test 2: Verify that an error message is displayed when the Password field is empty
        [Test]
        public void VerifyEmptyPasswordField()
        {
            // Enter a valid username and leave the password field empty
            IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
            usernameField.SendKeys("validusername");

            // Check if Sign In button is disabled
            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[contains(@class,'v-btn--disabled')]")));
            ClassicAssert.IsTrue(signInButton.Displayed, "Sign In button should be disabled when the password field is empty.");
        }

        // Test 3: Verify that the Sign In button remains disabled when both fields are invalid
        [Test]
        public void VerifyBothFieldsInvalid()
        {
            // Enter invalid username and invalid password
            IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
            usernameField.SendKeys("invalidusername");

            IWebElement passwordField = wait.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
            passwordField.SendKeys("invalidpassword");

            // Ensure the Sign In button is clickable only if both fields are filled
            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@type='submit' and not(@disabled)]")));
            ClassicAssert.IsTrue(signInButton.Displayed, "Sign In button should be enabled when both fields are filled.");
        }

        // Test 4: Verify that the system displays appropriate error messages when login attempts exceed the allowed number
        [Test]
        public void VerifyErrorMessageAfterExceedingLoginAttempts()
        {
            // Simulate multiple failed login attempts, refreshing the page after each attempt
            for (int i = 0; i < 5; i++)
            {
                IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
                usernameField.Clear();
                usernameField.SendKeys("invalidusername");

                IWebElement passwordField = wait.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
                passwordField.Clear();
                passwordField.SendKeys("invalidpassword");

                IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@type='submit' and not(@disabled)]")));
                signInButton.Click();

                // Refresh the page after each attempt
                driver.Navigate().Refresh();
            }

            // Check for error message after exceeding allowed login attempts
            IWebElement errorMessage = wait.Until(drv => drv.FindElement(By.XPath("//div[contains(text(),'Too many failed login attempts')]")));
            ClassicAssert.IsTrue(errorMessage.Displayed, "Error message for exceeding allowed login attempts is missing.");
        }

        // Test 5: Verify that a generic "Invalid credentials" message is shown for incorrect credentials
        [Test]
        public void VerifyGenericInvalidCredentialsMessage()
        {
            // Enter incorrect credentials
            IWebElement usernameField = wait!.Until(drv => drv.FindElement(By.XPath("//input[@id='input-204']")));
            usernameField.SendKeys("randomusername");

            IWebElement passwordField = wait.Until(drv => drv.FindElement(By.XPath("//input[@id='input-207']")));
            passwordField.SendKeys("randompassword");

            IWebElement signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@type='submit' and not(@disabled)]")));
            signInButton.Click();

            // Check for the specific error message
            IWebElement errorMessage = wait.Until(drv => drv.FindElement(By.XPath("//div[contains(text(),'Incorrect email/username or password')]")));
            ClassicAssert.IsTrue(errorMessage.Displayed, "Error message 'Incorrect email/username or password' is missing.");

            // Verify that the sign-in button is now disabled after an invalid attempt
            signInButton = wait.Until(drv => drv.FindElement(By.XPath("//button[@class='primary v-btn v-btn--disabled v-btn--has-bg theme--light v-size--default' and @disabled='disabled']")));
            ClassicAssert.IsTrue(signInButton.Displayed, "Sign In button should be disabled.");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}