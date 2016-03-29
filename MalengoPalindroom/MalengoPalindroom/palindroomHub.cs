using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace MalengoPalindroom
{
    public class palindroomHub : Hub
    {
        public void SendWoord(string woord)
        {
            Clients.All.broadcastWoord(woord);
        }
        public void SendAantal(long aantal)
        {
            Clients.All.broadcastAantal(aantal);
        }
        public void SendLengte(long lengte)
        {
            Clients.All.broadcastLengte(lengte);
        }
    }
}