using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace A4.Empower.Helpers
{
    public class SalesMarketingTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string ClientMeetingSchedule(string clientName, DateTime dateTime, string subject, string venue,string agenda)
        {
            var clientMeetingSchedule = ReadFile("./wwwroot/Templates/SalesMarketing/ClientMeetingSchedule.html");
            string emailMessage = clientMeetingSchedule
                 .Replace("{clientName}", clientName)
                 .Replace("{dateTime}", dateTime.ToString())
                 .Replace("{subject}", subject)                 
                 .Replace("{venue}", venue)
                 .Replace("{agenda}", agenda);
            return emailMessage;
        }

        public static string EmployeeMeetingSchedule(string employeeName, DateTime dateTime, string subject, string venue, string clientName, string agenda)
        {
            var employeeMeetingSchedule = ReadFile("./wwwroot/Templates/SalesMarketing/EmployeeMeetingSchedule.html");           
            string emailMessage = employeeMeetingSchedule
                 .Replace("{employeeName}", employeeName)
                 .Replace("{dateTime}", dateTime.ToString())
                 .Replace("{subject}", subject)
                 .Replace("{venue}", venue)
                 .Replace("{clientName}", clientName)
                 .Replace("{agenda}", agenda);
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
