using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace DriverProvider.Driver
{
    public class DriverInitializer
    {
        private static IWebDriver _webDriver;

        public static IWebDriver Driver
        {
            get { return _webDriver; }
            private set
            {
                _webDriver = _webDriver ?? (_webDriver = DriverInitialize());
            }
        }

        private static readonly Dictionary<string, Func<IWebDriver>> _driverInitializerMethods = new Dictionary<string, Func<IWebDriver>>
            {
                {"firefox" , FireFoxDriverInitializer}
            };


        private static IWebDriver DriverInitialize()
        {
            string browser = ConfigurationManager.AppSettings["browser"];
            return _driverInitializerMethods[browser]();
        }

        private static IWebDriver FireFoxDriverInitializer()
        {
            //TODO: create a FireFox Profile

            // create a Firefox Driver
            IWebDriver driver = new FirefoxDriver();
            return driver;
        }
    }
}
