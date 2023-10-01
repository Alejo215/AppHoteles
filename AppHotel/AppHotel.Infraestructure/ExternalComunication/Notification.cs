using AppHotel.Api.Settings;
using AppHotel.Domain.ApplicationServiceContracts;
using Azure.Communication.Email;
using Azure;
using Microsoft.Extensions.Options;

namespace AppHotel.Infraestructure.ExternalComunication
{
    public class Notifications : INotifications
    {

        Notification _notification;
        public Notifications(IOptions<Notification> notification)
        {
            _notification = notification.Value;
        }

        public void EmailNotification(IEnumerable<string> listEmails)
        {
            EmailClient emailClient = new EmailClient(_notification.Connection);
            EmailContent emailContent = new EmailContent("Se ha creado su reserva");
            emailContent.PlainText = "Se ha creado su reserva";
            List<EmailAddress> emailAddresses = new();
            foreach (string email in listEmails)
            {
                emailAddresses.Add(new EmailAddress(email));
            }
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("DoNotReply@958a9d33-49be-4578-8ff3-3d252b325c21.azurecomm.net", emailRecipients, emailContent);
            emailClient.Send(WaitUntil.Started, emailMessage, CancellationToken.None);
        }
    }
}
