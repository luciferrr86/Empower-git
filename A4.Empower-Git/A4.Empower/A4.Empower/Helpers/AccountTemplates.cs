using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;


namespace A4.Empower.Helpers
{
    public static class AccountTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;

        static string emailTemplate;

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string ForgotPassword(string name, string link)
        {

            var forgotPassword = ReadFile("./wwwroot/Templates/Account/ForgotPassword.html");
            string emailMessage = forgotPassword
                .Replace("{user}", name)
                .Replace("{link}", link);

            return emailMessage;
        }

        public static string ChangePassword(string name, string email, string userName, string newpassword, DateTime testDate)
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Account/ChangePassword.html");


            string emailMessage = emailTemplate
                .Replace("{user}", name)
                .Replace("{email}", email)
                .Replace("{userName}", userName)
                .Replace("{password}", newpassword)
                .Replace("{testDate}", testDate.ToString());

            return emailMessage;
        }

        public static string GetEmployeeEmail(string name, string email, string password, string url)
        {

            var getEmployeeEmail = ReadFile("./wwwroot/Templates/Account/EmployeeAccount.html");
            string emailMessage = getEmployeeEmail
                .Replace("{user}", name)
                .Replace("{userName}", email)
                .Replace("{password}", password)
                .Replace("url", url);

            return emailMessage;
        }

        public static string GetCandidateEmail(string name, string email, string password)
        {

            var getCandidateEmail = ReadFile("./wwwroot/Templates/Account/candidateAccount.html");
            string emailMessage = getCandidateEmail
                .Replace("{user}", name)
                .Replace("{userName}", email)
                .Replace("{password}", password);

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
