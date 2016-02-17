using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR;

namespace SignalRGame
{
    public class ClientHandler : PersistentConnection
    {
        protected override Task OnConnectedAsync(HttpContextBase context, string clientId)
        {
            Game.AddGameHandler(this);
            return base.OnConnectedAsync(context, clientId);
        }

        internal void Draw(List<Ship> ships, List<Missile> blasts, Arena arena)
        {
            Connection.Broadcast(new DrawInfo
                                     {
                                         Ships = ships.ToArray(),
                                         Blasts = blasts.ToArray(),
                                         Arena = arena
                                     });
        }
    }

    public class DrawInfo
    {
        public Ship[] Ships;
        public Missile[] Blasts;
        public Arena Arena;
    }
}