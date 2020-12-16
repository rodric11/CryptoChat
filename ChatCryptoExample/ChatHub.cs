using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SimpleCryptoChat.Models
{
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
    public class ChatHub : Hub
    {
        static public List<string> Users { get; set; } = new List<string>();
        public void Send(string name, dynamic message, dynamic key)
        {
            Clients.All.broadcastMessage(name, message, key);
        }

        public override Task OnConnected()
        {
            if (UserHandler.ConnectedIds.Count >= 2)
            {
                return null;
            }
            else
            {
                UserHandler.ConnectedIds.Add(Context.ConnectionId);
            }
            return base.OnConnected();

        }

        public override Task OnDisconnected(bool stopcaller)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopcaller);
        }
    }
}