using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace A4.Empower.Helpers
{
    public class ExpenseBookingTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string EmployeeToManager(string managerName, string employeeName,string requestDate)
        {
            var requestToManager = ReadFile("./wwwroot/Templates/ExpenseBooking/EmployeeToManager.html");
            string emailMessage = requestToManager
                 .Replace("{employeeName}", employeeName)
                 .Replace("{requestDate}", requestDate)
                 .Replace("{managerName}", managerName);
            return emailMessage;
        }

        public static string ManagerToManager(string managerName, string employeeName, string requestDate)
        {
            var requestToManager = ReadFile("./wwwroot/Templates/ExpenseBooking/ManagerToManager.html");

            string emailMessage = requestToManager
                 .Replace("{employeeName}", employeeName)
                 .Replace("{requestDate}", requestDate)
                 .Replace("{managerName}", managerName);
            return emailMessage;
        }

        public static string ManagerToInviteEmployee(string inviteEmployee, string employeeName)
        {
            var requestToManager = ReadFile("./wwwroot/Templates/ExpenseBooking/ManagerToInviteEmployee.html");

            string emailMessage = requestToManager
                 .Replace("{employeeName}", employeeName)
                 .Replace("{inviteEmployee}", inviteEmployee);
            return emailMessage;
        }

        public static string InviteEmployeeToManager(string managerName, string employeeName,string status,string inviteName)
        {
            var requestToManager = ReadFile("./wwwroot/Templates/ExpenseBooking/InviteEmployeeToManager.html");

            string emailMessage = requestToManager
                 .Replace("{status}", status)
                 .Replace("{employeeName}", employeeName)
                 .Replace("{inviteName}", inviteName)
                 .Replace("{managerName}", managerName);
            return emailMessage;
        }

        public static string ManagerToEmployee(string employeeName, string status)
        {

            var approveReject = ReadFile("./wwwroot/Templates/ExpenseBooking/ApproverToEmployee.html");

            string emailMessage = approveReject
                 .Replace("{employeeName}", employeeName)
                 .Replace("{status}", status);
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
