using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace A4.Empower.Helpers
{
    public class AdminTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;
  
        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string ClientCreate(string employeeName, string password, string username, string url)
        {

            var clientCreate = ReadFile("./wwwroot/Templates/Admin/Client.html");
            string emailMessage = clientCreate
                 .Replace("{employeeName}", employeeName)
                 .Replace("{password}", password)
                 .Replace("{url}", url)
                 .Replace("{username}", username);
            return emailMessage;
        }

        public static string ReadFile(string FileName)
        {
            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string fileContent = reader.ReadToEnd();
                    if (fileContent != null && fileContent != "")
                    {
                        return fileContent;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
