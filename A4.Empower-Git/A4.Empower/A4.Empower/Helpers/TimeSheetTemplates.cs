using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace A4.Empower.Helpers
{
    public class TimeSheetTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string TimesheetSubmitMail(string managerName, string managerEmail, string employeeName, string weeklyStartDate, string weekEndDate)
        {

            var fileSubmit = ReadFile("./wwwroot/Templates/TimeSheet/TimeSheetSubmit.html");
            string emailMessage = fileSubmit
                 .Replace("{managerName}", managerName)
                 .Replace("{employeeName}", employeeName)
                 .Replace("{weeklyStartTime}", weeklyStartDate)
                 .Replace("{weekEndDate}", weekEndDate);
            return emailMessage;
        }

        public static string TimesheetApproveSendMail(string mangerName, string employeeEmail, string employeeName, string weeklyStartDate, string weekEndDate)
        {

            var fileApprove = ReadFile("./wwwroot/Templates/TimeSheet/TimeSheetApprove.html");
            string emailMessage = fileApprove
                .Replace("{managerName}", mangerName)
                 .Replace("{employeeName}", employeeName)
                 .Replace("{weeklyStartDate}", weeklyStartDate)
                 .Replace("{weekEndDate}", weekEndDate);
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
