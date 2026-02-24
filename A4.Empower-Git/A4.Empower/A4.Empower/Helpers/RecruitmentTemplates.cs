using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace A4.Empower.Helpers
{
    public class RecruitmentTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;

        static string emailTemplate;

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string ManagerInterviewSchedule(string managerName, string candidateName, string position, string date, string time,string url="")
        {

            var managerInterviewSchedule = ReadFile("./wwwroot/Templates/Recruitment/ManagerInterviewSchedule.html");

            string emailMessage = managerInterviewSchedule
                 .Replace("{candidateName}", candidateName)
                 .Replace("{managerName}", managerName)
                 .Replace("{position}", position)
                 .Replace("{date}", date)
                 .Replace("{time}", time)
                 .Replace("{pageUrl}",url);

            return emailMessage;
        }

        public static string CandidateReject(string candidateName)
        {

            var candidateReject = ReadFile("./wwwroot/Templates/Recruitment/CandidateReject.html");

            string emailMessage = candidateReject
                 .Replace("{candidateName}", candidateName);

            return emailMessage;
        }

        public static string CandidateSelected(string candidateName, string postion)
        {

            var candidateSelecte = ReadFile("./wwwroot/Templates/Recruitment/CandidateSelected.html");
            string emailMessage = candidateSelecte
                 .Replace("{candidateName}", candidateName)
                 .Replace("{postion}", postion);

            return emailMessage;
        }

        public static string CandidateLevelReject(string candidateName, string level, string position)
        {

            var candidateReject = ReadFile("./wwwroot/Templates/Recruitment/CandidateLevelReject.html");

            string emailMessage = candidateReject
                 .Replace("{candidateName}", candidateName)
                 .Replace("{level}", level)
                 .Replace("{position}", position);

            return emailMessage;
        }

        public static string JDRequirement(string clientName, string jobVacancyName)
        {

            var candidateReject = ReadFile("./wwwroot/Templates/Recruitment/JDRequiredTemplate.html");

            string emailMessage = candidateReject
                 .Replace("{clientName}", clientName)
                 .Replace("{jobVacancy}", jobVacancyName);
                 

            return emailMessage;
        }

        public static string CandidateLevelSelection(string candidateName, string level,string position)
        {

            var candidateSelecte = ReadFile("./wwwroot/Templates/Recruitment/CandidateLevelSelection.html");
            string emailMessage = candidateSelecte
                 .Replace("{candidateName}", candidateName)
                 .Replace("{level}", level)
                 .Replace("{position}", position);

            return emailMessage;
        }

        public static string CandidateInterviewSchedule(string candidateName, string position, string date, string time)
        {
            var candidateInterviewSchedule = ReadFile("./wwwroot/Templates/Recruitment/CandidateInterviewSchedule.html");
            string emailMessage = candidateInterviewSchedule
                 .Replace("{candidatename}", candidateName)
                 .Replace("{position}", position)
                 .Replace("{date}", date)
                 .Replace("{time}", time);
            return emailMessage;
        }

        public static string GetManagerFeedbackToHrEmail(string name, string hrName)
        {
            var getManagerFeedbackToHrEmail = ReadFile("./wwwroot/Templates/Recruitment/HRFeedbackFromManager.html");
            string emailMessage = getManagerFeedbackToHrEmail
                .Replace("{candidateName}", name)
                .Replace("{hrName}", hrName);

            return emailMessage;
        }

        public static string GetSelectedCandidateToCandidateEmail(string name)
        {
      
               var getManagerFeedbackToHrEmail = ReadFile("./wwwroot/Templates/Recruitment/CandidateSelected.html");

            string emailMessage = getManagerFeedbackToHrEmail
                .Replace("{candidateName}", name);

            return emailMessage;
        }

        public static string GetRejectedCandidateToCandidateEmail(string name)
        {
        
               var getRejectedCandidateToCandidateEmail = ReadFile("./wwwroot/Templates/Recruitment/CandidateReject.html");


            string emailMessage = getRejectedCandidateToCandidateEmail
                .Replace("{candidateName}", name);

            return emailMessage;
        }

        public static string GetHrJobApplicationEmail(string name, string hrName, string position, string appliedDate,string email="", string phoneNumber="")
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Recruitment/JobApplicationMailTemplate.html");


            string emailMessage = emailTemplate
                .Replace("{candidate}", name)
                .Replace("{hrName}", hrName)
                .Replace("{position}", position)
                .Replace("{appliedDate}", appliedDate)
                .Replace("{email}", email)
                .Replace("{phoneNumber}", phoneNumber);
                

            return emailMessage;
        }

        public static string GetCandidateJobApplicationEmail(string name, string position)
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Recruitment/CandidateJobApplication.html");


            string emailMessage = emailTemplate
                .Replace("{candidate}", name)
                .Replace("{position}", position);
            return emailMessage;
        }

        public static string GetHrManagerAcceptEmail(string hrName, string managerName, string date, string time, string venue)
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Recruitment/ManagerAcceptInterview.html");

            string emailMessage = emailTemplate
                .Replace("{hrName}", hrName)
                .Replace("{date}", date)
                .Replace("{time}", time)
                .Replace("{venue}", venue)
                .Replace("{managerName}", managerName);

            return emailMessage;
        }

        public static string GetHrManagerDeclineEmail(string hrName, string managerName, string date, string time, string venue, string comment)
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Recruitment/ManagerDeclineInterview.html");

            string emailMessage = emailTemplate
                .Replace("{hrName}", hrName)
                .Replace("{comment}", comment)
                .Replace("{date}", date)
                .Replace("{time}", time)
                .Replace("{venue}", venue)
                .Replace("{managerName}", managerName);

            return emailMessage;
        }

        public static string GetCandidateThankYouEmail(string name, string email)
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Recruitment/CandidateThankYou.html");


            string emailMessage = emailTemplate
                .Replace("{candidate}", name)
                .Replace("{candidateEmail}", email);

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
        public static string ContactUsEmail(string name, string hrName,string email, string contactNumber, string message, string date)
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Shared/UserContactUsDetailTemplate.html");


            string emailMessage = emailTemplate
                .Replace("{hrName}",hrName)
                .Replace("{name}", name)
                .Replace("{email}", email)
                .Replace("{conatctNumber}", contactNumber)
                .Replace("{message}", message)
                .Replace("{filledDate}",date);

            
            return emailMessage;
        }

        public static string CaseStudyEmail(string name, string hrName, string caseStudyName,string email, string contactNumber, string message, string date)
        {
            if (emailTemplate == null)
                emailTemplate = ReadFile("./wwwroot/Templates/Shared/CaseStudyDownloadDetailTemplate.html");


            string emailMessage = emailTemplate
                .Replace("{hrName}", hrName)
                .Replace("{caseStudyName}", caseStudyName)
                .Replace("{name}", name)
                .Replace("{email}", email)
                .Replace("{conatctNumber}", contactNumber)
                .Replace("{message}", message)
                .Replace("{filledDate}", date);

            
            return emailMessage;
        }
    }
}
