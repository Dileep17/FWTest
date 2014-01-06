using System;
using System.Configuration;
using DriverProvider.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pages.BasePage
{
    abstract class BasePage
    {
        public IWebDriver Driver {
            get { return DriverInitializer.Driver; }
        }

        public abstract void Open();

        public bool IsElementDisplayed(By element)
        {
            try
            {
                return Driver.FindElement(element).Displayed;
            }
            catch (NoSuchElementException e)
            {

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is wrong came to exception instead of NoSuchelementException -- IsElementDisplayed({0}) -- Exception >> {1}",element.ToString(),e.Message);
                return false;
            }
        }


        public bool IsElementNotDisplayed(By element)
        {
            try
            {
                Driver.FindElement(element);
                return false;
            }
            catch (NoSuchElementException e)
            {
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is wrong came to exception instead of NoSuchelementException -- IsElementNotDisplayed({0}) -- Exception >> {1}",element.ToString(),e.Message);
                return false;
            }

        }

        public bool WaitForElementPresent(By element,int waitTime = -1,int pollTime = -1)
        {
            pollTime = (pollTime == -1) ? 0 : pollTime;
            waitTime = GetWaitTime(waitTime);

            var webDriverWait = new WebDriverWait(new SystemClock(), Driver, TimeSpan.FromSeconds(waitTime),
                                                  TimeSpan.FromSeconds(pollTime));
            try
            {
                webDriverWait.Until(delegate { return IsElementDisplayed(element); });
            }
            catch (Exception)
            {

                return false;
            }
            return true;

        }

        private int GetWaitTime(int waitTime)
        {
            if (waitTime != -1)
            {
                return waitTime;
            }
            var waitTimeFromAppConfig = ConfigurationManager.AppSettings["WaitTime"];
            if (waitTimeFromAppConfig == null)
            {
                return 0;
            }
            return Int32.Parse(waitTimeFromAppConfig);
        }

        public bool WaitForElementNotPresent(By element, int waitTime = -1, int pollTime = -1)
        {
            pollTime = (pollTime == -1) ? 0 : pollTime;
            waitTime = GetWaitTime(waitTime);
            var webDriverWait = new WebDriverWait(new SystemClock(), Driver, TimeSpan.FromSeconds(waitTime),
                                                  TimeSpan.FromSeconds(pollTime));
            
            try
            {
                webDriverWait.Until(delegate { return IsElementNotDisplayed(element); });
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
