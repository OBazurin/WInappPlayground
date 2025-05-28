using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Wad.Tests
{
    public class Tests
    {
        private static WindowsDriver<WindowsElement>? _excelDriver;

        [SetUp]
        public void Setup()
        {
            var uri = new Uri("http://127.0.0.1:4723");
            AppiumOptions opt = new AppiumOptions();
            opt.PlatformName = "Windows";
            opt.AddAdditionalCapability("app", "C:\\Program Files\\Microsoft Office\\root\\Office16\\EXCEL.EXE");
            opt.AddAdditionalCapability("deviceName", "WindowsPC");
            opt.AddAdditionalCapability("automationName", "Windows");
            opt.AddAdditionalCapability("ms:waitForAppLaunch", 10);

            _excelDriver = new WindowsDriver<WindowsElement>(uri, opt);
            _excelDriver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            _excelDriver.FindElement(By.Name("Blank workbook")).Click();
            _excelDriver.FindElement(By.Name("A1")).Click();
            _excelDriver.FindElement(By.Name("A1")).SendKeys("=2+2");
            _excelDriver.FindElement(By.Name("A1")).SendKeys(Keys.Enter);
            var result = _excelDriver.FindElement(By.Name("A1")).Text;  
            Assert.AreEqual("4", result, "The result of the calculation should be 4.");
        }

        [TearDown]
        public void TearDown()
        {
            if (_excelDriver != null)
            {
                _excelDriver.Quit();
                _excelDriver.Dispose();
                var processes = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
        }
    }
}