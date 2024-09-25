using BotDemo;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebDriver driver = null;
            try
            {
                // Create a single WebDriver instance and use it for all operations
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize(); // Maximize the browser window

                // Read data from the Excel file
                var data = ExcelReader.ReadExcelData("B:\\Fr_Farid\\c#\\Console\\sampls\\BotDemo\\BotDemo\\Book1.xlsx");

                // Check the data after reading
                if (data == null || data.Count == 0)
                {
                    Console.WriteLine("No data found in the Excel file.");
                    return;
                }

                // Perform registration for each record in the Excel file using the same browser
                foreach (var record in data)
                {
                    string email = record.email;
                    string phone = record.phone;

                    // Print the data for review
                    Console.WriteLine($"Processing: Email = {email}, Phone = {phone}");

                    // Call the bot to register the user on Facebook using the same WebDriver
                    FaceBookBot.RegisterOnFaceBook(driver, email, phone);

                    // Wait for the confirmation code page to appear
                    WaitForNextPage(driver);

                    // Optional delay between each operation
                    Thread.Sleep(5000); // Delay of 5 seconds, for example
                }

                Console.WriteLine("All records processed successfully.");
            }
            catch (Exception ex)
            {
                // Print the error message if an exception occurs
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (driver != null)
                    driver.Quit();
            }
        }

        // Method to wait until the next page (confirmation code page) appears
        public static void WaitForNextPage(IWebDriver driver)
        {
            // Wait for an element that indicates you are on the next page (e.g., code input field)
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));

            try
            {
                // Adjust By.Id or By.Name based on the element that appears on the confirmation code page
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("code")));
                Console.WriteLine("Confirmation code page is displayed.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("The confirmation code page did not appear within the expected time.");
            }
        }
    }
}
