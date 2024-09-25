using OpenQA.Selenium;

namespace BotDemo
{
    public class FaceBookBot
    {
        public static void RegisterOnFaceBook(IWebDriver driver, string email, string phone)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://www.facebook.com/r.php");

            // Enter data into the fields
            driver.FindElement(By.Name("firstname")).SendKeys("hshamat");
            driver.FindElement(By.Name("lastname")).SendKeys("Dodree");
            driver.FindElement(By.Name("reg_email__")).SendKeys(email); // Use email here
            driver.FindElement(By.Name("reg_passwd__")).SendKeys("YourPassword123");
            driver.FindElement(By.Name("birthday_day")).SendKeys("15");
            driver.FindElement(By.Name("birthday_month")).SendKeys("5");
            driver.FindElement(By.Name("birthday_year")).SendKeys("1990");
            driver.FindElement(By.CssSelector("input[name='sex'][value='2']")).Click(); // Select male

            // Submit the registration
            driver.FindElement(By.Name("websubmit")).Click();

            // Delay to complete the process
            Thread.Sleep(10000); // 10 seconds
        }
    }
}
