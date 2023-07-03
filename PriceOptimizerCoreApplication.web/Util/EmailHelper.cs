using System;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using PriceOptimizerCoreApplication.web.Models;
using System.Net;
using System.Threading.Tasks;

namespace PriceOptimizerCoreApplication.web.Util
{
    public class EmailHelper
    {
        private string _host;
        private string _from;
        private string _alias;
        private string LogfilePath = "wwwroot/Applog/log.txt";
        public EmailHelper(IConfiguration iConfiguration, ILogger<EmailHelper> logger, Credential credential, Common common)
        {
            var smtpSection = iConfiguration.GetSection("SMTP");
            if (smtpSection != null)
            {
                _host = smtpSection.GetSection("Host").Value;
                _from = smtpSection.GetSection("From").Value;
                _alias = smtpSection.GetSection("Alias").Value;
          
            }
            _logger = logger;
            _credential = credential;
            _common = common;
        }
        string filePath = "wwwroot/ErrorLogs/Error.txt";
        private readonly ILogger<EmailHelper> _logger;
        private readonly Credential _credential;
        private readonly Common _common;
        public async Task<bool> SendEmailWelcomeAsync(Email emailModel, string ControllerName)
        {
            try
            {
                //await Utility.Log(LogfilePath, _host, _from, _alias, 1);
                var result = await SendEmailBody(emailModel, emailModel.To, ControllerName);
                if (result == true){
                    return true;
                }
                else{
                    return false;
                }
            }
            catch (Exception ex)
            {
                await ErrorLogAsync(ex, ControllerName, "SendEmailWelcomeAsync");
                return false;
            }
        }
        private async Task<bool> SendEmailBody(Email emailModel,string To,string ControllerName)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_host))
                {
                    if (_from == null || _from.Length == 0)
                    {
                        _from = _credential.GetEmail;
                    }
                    emailModel.FromEmail = _credential.GetEmail.Trim();
                    emailModel.FromPassword = _credential.GetPassword.Trim();
                    using (MailMessage mailMessage = new MailMessage(emailModel.FromEmail,To))
                    {
                        mailMessage.From = new MailAddress(emailModel.FromEmail, _alias);
                       // await Utility.Log(LogfilePath, _host, _from, _alias, 2);
                        // mailMessage.BodyEncoding = Encoding.UTF8;
                        mailMessage.To.Add(To);
                        mailMessage.Body = emailModel.Body;
                        mailMessage.Subject = emailModel.Subject;
                        mailMessage.IsBodyHtml = emailModel.IsBodyHtml;
                        mailMessage.Priority = MailPriority.High;
                        client.Port = 587;
                       // client.UseDefaultCredentials = false;
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(emailModel.FromEmail, emailModel.FromPassword);
                        client.Send(mailMessage);
                    }
                }
                return true; 
            }
            catch (Exception ex)
            {
                await ErrorLogAsync(ex, ControllerName, "SendEmailBody");
                return false;
            }
        }
        public async Task<bool> SendEmailAsync(Email emailModel,string ControllerName)
        {
            try
            {
                if (_host==null ||_host.Length==0){
                    _host = "smtp.gmail.com";
                }
                string to = emailModel.To;
                string subject = emailModel.Subject;
                string body = emailModel.Body;
              var result= await SendEmailBody(emailModel, _credential.GetEmail, ControllerName);
                if (result==true){
                    return true;
                }
                else{
                    return false;
                }
            }
            catch(Exception ex)
            {
               await  ErrorLogAsync(ex,ControllerName, "SendEmailAsync");
               return false;
            }
        }
        private async Task<bool> ErrorLogAsync(Exception ex,string ControllerName,string ActionName)
        {
            var path = _common.GetFilePath(filePath);
            await Util.Utility.LogErrorMessage(ex, path, ControllerName, ActionName );
            _logger.LogError(ex, ex.Message);
            return true; 
        }
    }
}
