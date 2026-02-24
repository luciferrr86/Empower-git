using A4.DAL.Entites;
using A4.Empower.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Helpers
{
    public class PerformanceTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string FeedbackMail(string empID, string employeeName, string description, string feedbackForName)
        {

            var feedbackMail = ReadFile("./wwwroot/Templates/Performance/RequestFeedback.html");
            string emailMessage = feedbackMail
                 .Replace("{employeeName}", employeeName)
                 .Replace("{description}", description)
                 .Replace("{feedbackForName}", feedbackForName);
            return emailMessage;
        }

        public static string Invitation(string empEmailID, string employeeName)
        {

            var invitation = ReadFile("./wwwroot/Templates/Performance/Invitation.html");

            string emailMessage = invitation
                 .Replace("{employeeName}", employeeName)
                 .Replace("{empEmailID}", empEmailID);
            return emailMessage;
        }

        public static string AccomplishmentNotification(string managerName)
        {

            var accomplishment = ReadFile("./wwwroot/Templates/Performance/ReviewNotificationOnAccomplishment.html");

            string emailMessage = accomplishment
                 .Replace("{managerName}", managerName);                
            return emailMessage;
        }

        public static string SendMail(string employeeName, int MailType)
        {
            string key = "";
            string subject = "";
            string emailMessage = "";
            switch (MailType)
            {
                case 3:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/Invitation.html");
                    subject = "Set goals for direct reportees";
                    key = "Invitation";
                    break;
                case 4:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/ReminderToRelease.html");
                    subject = "Reminder to release goal";
                    key = "ReminderToRelease";
                    break;
                case 5:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/ReminderToEnterGoal.html");
                    subject = "Reminder to enter accomplishments/development goals";
                    key = "ReminderToEnterGoal";
                    break;
                case 6:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/SignOffOnInitialRating.html");
                    subject = "Review Initial Rating";
                    key = "SignOffOnInitialRating";
                    break;
                case 7:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/PerformanceReviewNotification.html");
                    subject = "Annual Performance Review";
                    key = "PerformanceReviewNotification";
                    break;
                case 8:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/ReviewNotificationOnAccomplishment.html");
                    subject = "Review Accomplishments/Development goals";
                    key = "ReviewNotificationOnAccomplishment";
                    break;
                case 9:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/NotificationOnRating.html");
                    subject = "Annual Performance Review have been rated";
                    key = "NotificationOnRating";
                    break;
                case 10:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/RatingAcceptanceNotification.html");
                    subject = "Acceptance of review rating";
                    key = "RatingAcceptanceNotification";
                    break;
                case 11:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/NotificationtoPresidentSignOff.html");
                    subject = "President council signoff";
                    key = "NotificationtoPresidentSignOff";
                    break;
                case 12:
                    emailMessage = ReadFile("./wwwroot/Templates/Performance/NotificationOnProcessCompletion.html");
                    subject = "Completion of Annual Performance Review process";
                    key = "NotificationOnProcessCompletion";
                    break;
                default:
                    break;
            }
            if (employeeName != null)
            {

                emailMessage.Replace("{employeeName}", employeeName);
                return emailMessage;
            }
            return emailMessage;
        }

        public static string ExternalFeedbackMail(string empID, string employeeName, string description, string feedbackForName)
        {
            var externalFeedbackMail = ReadFile("./wwwroot/Templates/Performance/RequestFeedback.html");
            string emailMessage = externalFeedbackMail
                 .Replace("{employeeName}", employeeName)
                 .Replace("{description}", description)
                 .Replace("{feedbackForName}", feedbackForName);
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
