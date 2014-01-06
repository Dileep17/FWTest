using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverProvider.Driver;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Tests
{
    class TestFailure
    {
        private static Dictionary<string, string> _failures = new Dictionary<string, string>(); 
        public static void Failure(string message)
        {

          string screenshotpath = TakeScreenShot();
          _failures.Add(message,screenshotpath);
        }

        private static string TakeScreenShot()
        {
            string screenShotPath = GetScreenShotPath();
            var driver = DriverInitializer.Driver as ITakesScreenshot;
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(screenShotPath,ImageFormat.Png);
            return null;
        }

        private static string GetScreenShotPath()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var path = directory.Parent.FullName;
            Console.WriteLine(path);
            return path;
        }

        public static void Clear()
        {
            _failures.Clear();
        }

        public static void Publish()
        {
            if (_failures.Count > 0)
            {
                int i = 0;
                foreach (var failure in _failures)
                {
                    i++;
                    Console.WriteLine("{0}. Error Message \n {1} \n Screen Shot at {2}",i,failure.Key,failure.Value);
                }
                Assert.Fail(" Total number of Failures {0}",_failures.Count);
            }
        }
    }
}
