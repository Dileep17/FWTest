using System;
using System.Configuration;

namespace DriverProvider.Driver
{
    class DriverPropertiesSetter
    {
        private const int ImplicitTimeOut = 5;

        DriverPropertiesSetter()
        {
            MaximizeWindow();
            SetImplicitTimeOut();
        }

        private void SetImplicitTimeOut()
        {
            var valueForAppConfig = ConfigurationManager.AppSettings["ImplicitTimeOut"];
            int waitTime = (valueForAppConfig == null) ? ImplicitTimeOut : Int32.Parse(valueForAppConfig);
            DriverInitializer.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(waitTime));
        }

        private void MaximizeWindow()
        {
            DriverInitializer.Driver.Manage().Window.Maximize();
        }



    }
}
