using OfficeOpenXml;

namespace BotDemo
{
    public class ExcelReader
    {
        public static List<(string email, string phone)> ReadExcelData(string filePath)
        {
            var data = new List<(string email, string phone)>();

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set license context

                var fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists)
                {
                    Console.WriteLine("File does not exist.");
                    return data;
                }

                using (var package = new ExcelPackage(fileInfo))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        Console.WriteLine("No worksheets found in the file.");
                        return data;
                    }

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming it's the first sheet

                    int rowCount = worksheet.Dimension?.Rows ?? 0;

                    for (int row = 2; row <= rowCount; row++) // Start from the second row to skip headers
                    {
                        string email = worksheet.Cells[row, 1].Value?.ToString().Trim();
                        string phone = worksheet.Cells[row, 2].Value?.ToString().Trim();

                        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(phone))
                        {
                            data.Add((email, phone));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the data: {ex.Message}");
                // You can also log the error or take other actions as needed
            }

            return data;
        }
    }
}
