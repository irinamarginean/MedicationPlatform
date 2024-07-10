using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Consumer
{
    public class ActivityHub : Hub
    {
        public async Task SendActivityNotification(string email, string message)
        {
            await Clients.All.SendAsync("messageReceived", email, message);
        }
    }
}
