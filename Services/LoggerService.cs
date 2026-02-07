using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZastitaInformacija_19322.Services
{
    public class LoggerService
    {
        string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file_log.txt");

        public void LogToFile(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
