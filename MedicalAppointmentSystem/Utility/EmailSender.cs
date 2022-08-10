using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient("e5a54f09eb2de40fca50179697d41ab6", "d91d0059005affc936f60531873854fe")
            {

            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
           .Property(Send.FromEmail, "appoinment@gmail.com")
           .Property(Send.FromName, "Medical Appointment Scheduler")
           .Property(Send.Subject, subject)
           .Property(Send.HtmlPart, htmlMessage)
           .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", email}
                 }
               });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}
