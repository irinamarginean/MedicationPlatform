using System.Threading.Tasks;

namespace BusinessLogicLayer.Consumer
{
    public interface IActivityHub
    {
        Task SendActivityNotification(string email, string message);
    }
}
