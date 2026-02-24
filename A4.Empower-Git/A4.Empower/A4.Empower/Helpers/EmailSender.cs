using MailKit.Net.Smtp;
using MimeKit;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace A4.Empower.Helpers
{
    public interface IEmailer
    {
        Task<(bool success, string errorMsg)> SendEmailAsync(MailboxAddress sender, MailboxAddress[] recepients, string subject, string body, SmtpConfig config = null, bool isHtml = true, IFormFileCollection attachment = null, List<MailboxAddress> ccEmails = null);
        Task<(bool success, string errorMsg)> SendEmailAsync(string recepientName, string recepientEmail, string subject, string body, SmtpConfig config = null, bool isHtml = true, IFormFileCollection attachment = null);
        Task<(bool success, string errorMsg)> SendEmailAsync(string senderName, string senderEmail, string recepientName, string recepientEmail, string subject, string body, SmtpConfig config = null, bool isHtml = true);
    }

    public class Emailer : IEmailer
    {
        private SmtpConfig _config;

        public Emailer(IOptions<SmtpConfig> config)
        {
            _config = config.Value;
        }

        public async Task<(bool success, string errorMsg)> SendEmailAsync(string recepientName, string recepientEmail,
            string subject, string body, SmtpConfig config = null, bool isHtml = true, IFormFileCollection attachment = null)
        {

            //var senderList = recepientEmail.Split(";");

            if (recepientEmail == null)
                return (false, "Recepient Email is empty");
            var senderList = recepientEmail.Split(";");
            var from = new MailboxAddress(_config.Name, _config.EmailAddress);


            if (senderList.Length <= 1)
            {
                var to = new MailboxAddress(recepientName, senderList.FirstOrDefault());
                return await SendEmailAsync(from, new MailboxAddress[] { to }, subject, body, config, isHtml, attachment);
            }
            else
            {
                var to = new MailboxAddress(recepientName, senderList.FirstOrDefault());
                var ccMails = senderList.Skip(1);
                var ccList = new List<MailboxAddress>();
                // MailboxAddress[] ccList;
                foreach (var email in ccMails)
                {
                    // Use TryParse to avoid constructing with a single-argument ctor (which doesn't exist)
                    if (!string.IsNullOrWhiteSpace(email) && MailboxAddress.TryParse(email.Trim(), out var parsed))
                    {
                        ccList.Add(parsed);
                    }
                }

                return await SendEmailAsync(from, new MailboxAddress[] { to }, subject, body, config, isHtml, attachment, ccList);
            }


        }

        public async Task<(bool success, string errorMsg)> SendEmailAsync(string senderName, string senderEmail,
            string recepientName, string recepientEmail,
            string subject, string body, SmtpConfig config = null, bool isHtml = true)
        {
            var from = new MailboxAddress(senderName, senderEmail);
            var to = new MailboxAddress(recepientName, recepientEmail);

            return await SendEmailAsync(from, new MailboxAddress[] { to }, subject, body, config, isHtml);
        }

        public async Task<(bool success, string errorMsg)> SendEmailAsync(MailboxAddress sender, MailboxAddress[] recepients, string subject, string body, SmtpConfig config = null, bool isHtml = true, IFormFileCollection attachment = null, List<MailboxAddress> ccEmails = null)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(sender);
            message.To.AddRange(recepients);
            if (ccEmails != null) message.Cc.AddRange(ccEmails);
            message.Subject = subject;

            if (isHtml)
            {
                var bodybuilder = new BodyBuilder { HtmlBody = body };
                if (attachment != null && attachment.Any())
                {
                    foreach (var item in attachment)
                    {
                        byte[] fileBytes;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            await item.CopyToAsync(memoryStream);
                            fileBytes = memoryStream.ToArray();
                            bodybuilder.Attachments.Add(item.FileName, fileBytes, ContentType.Parse(item.ContentType));
                        }
                    }
                }
                //if (attachment != null )
                //{
                //     byte[] fileBytes;
                //        using (MemoryStream memoryStream = new MemoryStream())
                //        {
                //            await attachment.CopyToAsync(memoryStream);
                //            fileBytes = memoryStream.ToArray();
                //            bodybuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                //        }
                    
                //}
                message.Body = bodybuilder.ToMessageBody();
            }
            else
            {
                message.Body = new TextPart("plain") { Text = body };
            }

            try
            {
                if (config == null)
                    config = _config;

                using (var client = new SmtpClient())
                {
                    if (!config.UseSSL)
                        client.ServerCertificateValidationCallback = (object sender2, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

                    await client.ConnectAsync(config.Host, config.Port, config.UseSSL).ConfigureAwait(false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (!string.IsNullOrWhiteSpace(config.Username))
                        await client.AuthenticateAsync(config.Username, config.Password).ConfigureAwait(false);

                    await client.SendAsync(message).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                Utilities.CreateLogger<Emailer>().LogError(LoggingEvents.SEND_EMAIL, ex, "An error occurred whilst sending email");
                return (false, ex.Message);
            }
        }


    }

    public class SmtpConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
