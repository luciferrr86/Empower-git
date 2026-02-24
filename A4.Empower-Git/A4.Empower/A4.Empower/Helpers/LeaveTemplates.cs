using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace A4.Empower.Helpers
{
    public class LeaveTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string LeaveApplicationToManager(string employeeName, string startDate, string endDate, string managerName)
        {

            var leaveApplicationToManager = ReadFile("./wwwroot/Templates/Leave/LeaveApplicationToManager.html");

            string emailMessage = leaveApplicationToManager
                 .Replace("{employeeName}", employeeName)
                 .Replace("{managerName}", managerName)
                 .Replace("{startDate}", startDate)
                 .Replace("{endDate}", endDate);

            return emailMessage;
        }

        public static string LeaveApplicationToHr(string employeeName, string hrName, string startDate, string endDate, string managerName)
        {

            string leaveApplicationTohr = ReadFile("./wwwroot/Templates/Leave/LeaveApplicationToHr.html");

            string emailMessage = leaveApplicationTohr
                 .Replace("{employeeName}", employeeName)
                 .Replace("{hrName}", hrName)
                 .Replace("{managerName}", managerName)
                 .Replace("{startDate}", startDate)
                 .Replace("{endDate}", endDate);

            return emailMessage;
        }

        public static string LeaveApproveRejectedToEmployee(string employeeName, string managerName, string startDate, string endDate, string status)
        {

            var leaveApproveRejectEmployee = ReadFile("./wwwroot/Templates/Leave/LeaveApprovedRejectedToEmployee.html");
            string emailMessage = leaveApproveRejectEmployee
              .Replace("{employeeName}", employeeName)
              .Replace("{managerName}", managerName)
              .Replace("{startDate}", startDate)
              .Replace("{endDate}", endDate)
              .Replace("{status}", status);
            return emailMessage;
        }

        public static string LeaveApproveRejectedToHr(string hrName, string employeeName, string startDate, string endDate, string managerName, string status)
        {

            var leaveApproveRejectHr = ReadFile("./wwwroot/Templates/Leave/LeaveApprovedRejectedToHr.html");
            string emailMessage = leaveApproveRejectHr
              .Replace("{employeeName}", employeeName)
              .Replace("{hrName}", hrName)
              .Replace("{startDate}", startDate)
              .Replace("{endDate}", endDate)
              .Replace("{managerName}", managerName)
              .Replace("{status}", status);
            return emailMessage;
        }

        public static string LeaveRetractedToHr(string hrName, string employeeName, string startDate, string endDate)
        {

            var leaveRetractedToHr = ReadFile("./wwwroot/Templates/Leave/LeaveRetractMailToHR.html");
            string emailMessage = leaveRetractedToHr
              .Replace("{employeeName}", employeeName)
              .Replace("{hrName}", hrName)
              .Replace("{startDate}", startDate)
              .Replace("{endDate}", endDate);
            return emailMessage;
        }

        public static string LeaveRetractedToManager(string employeeName, string startDate, string endDate, string managerName)
        {

            var leaveRetractedToManager = ReadFile("./wwwroot/Templates/Leave/LeaveRetractMailToManager.html");
            string emailMessage = leaveRetractedToManager
              .Replace("{managerName}", managerName)
              .Replace("{employeeName}", employeeName)
              .Replace("{fromDate}", startDate)
              .Replace("{toDate}", endDate);
            return emailMessage;
        }

        public static string LeaveCancelledToHr(string hrName, string employeeName, string startDate, string endDate)
        {

            var leaveCancelledToHr = ReadFile("./wwwroot/Templates/Leave/LeaveCancelMailToHR.html");
            string emailMessage = leaveCancelledToHr
                 .Replace("{employeeName}", employeeName)
              .Replace("{hrName}", hrName)
              .Replace("{startDate}", startDate)
              .Replace("{endDate}", endDate);
            return emailMessage;
        }

        public static string LeaveCancelledToManager(string employeeName, string startDate, string endDate, string managerName)
        {

            var leaveCancelledToManager = ReadFile("./wwwroot/Templates/Leave/LeaveCancelMailToManager.html");
            string emailMessage = leaveCancelledToManager
                   .Replace("{managerName}", managerName)
              .Replace("{employeeName}", employeeName)
              .Replace("{fromDate}", startDate)
              .Replace("{toDate}", endDate);
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
                //Log
                throw ex;
            }
            return null;
        }

    }
}
