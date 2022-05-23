using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace WebDriverWaitExamples
{
    public class WaitTests
    {
        private WebDriver driver;

        private WebDriverWait wait;

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void TestWaitThreadSleep()
        {

            this.driver = new ChromeDriver();
            driver.Url = "http://www.uitestpractice.com/Students/Contact";

            driver.Manage().Window.FullScreen();

            Thread.Sleep(1000);
            var element = driver.FindElement(By.PartialLinkText("This is"));

            element.Click();

            Thread.Sleep(15000);
            var text_element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);

            driver.Url = "http://www.uitestpractice.com/Students/Contact";

            driver.Manage().Window.FullScreen();

            Thread.Sleep(1000);
            var element1 = driver.FindElement(By.PartialLinkText("This is"));

            element1.Click();

            Thread.Sleep(15000);
            var text_element1 = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);
        }


        [Test]
        public void TestWaitImplicit()
        {

            this.driver = new ChromeDriver();
            driver.Url = "http://www.uitestpractice.com/Students/Contact";

            driver.Manage().Window.FullScreen();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(15000);

            driver.FindElement(By.PartialLinkText("This is")).Click();

            var text_element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);

            driver.Url = "http://www.uitestpractice.com/Students/Contact";

            driver.Manage().Window.FullScreen();

            driver.FindElement(By.PartialLinkText("This is")).Click();            

            var text_element1 = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);
        }

        [Test]
        public void TestWaitExplicit()
        {

            this.driver = new ChromeDriver();
            this.wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(15000));

            driver.Url = "http://www.uitestpractice.com/Students/Contact";
            driver.Manage().Window.FullScreen();


            var text_element = wait.Until(d =>
            {
                return driver.FindElement(By.PartialLinkText("This is"));
            });

            text_element.Click();
            

            var text_element1 = wait.Until(d =>
            {
                return driver.FindElement(By.ClassName("ContactUs")).Text;
            });
            
            Assert.IsNotEmpty(text_element1);

        }

        [Test]
        public void TestWaitExpectedCondition()
        {

            this.driver = new ChromeDriver();
            this.wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(15000));

            driver.Url = "http://www.uitestpractice.com/Students/Contact";
            driver.Manage().Window.FullScreen();


            var text_element = wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("This is")));
              

            text_element.Click();

            var text_element1 = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ContactUs"))).Text;

            Assert.IsNotEmpty(text_element1);

        }

    }
}