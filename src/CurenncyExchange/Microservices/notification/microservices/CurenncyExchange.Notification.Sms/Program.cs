using CurenncyExchange.Notification.Email.Service;

public class Program
{
    static  void Main(string[] args)
    {
         new SmsNotificationService().Recieve();
    }
}
