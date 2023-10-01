namespace AppHotel.Domain.ApplicationServiceContracts
{
    public interface INotifications
    {
        void EmailNotification(IEnumerable<string> listEmails);
    }
}
