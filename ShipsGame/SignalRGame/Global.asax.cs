using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using SignalR.Routing;

namespace SignalRGame
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapConnection<NewPlayerHandler>("playerconnection", "playerconnection/{*operation}");
            RouteTable.Routes.MapConnection<KeyPressHandler>("keypressconnection", "keypressconnection/{*operation}");
            RouteTable.Routes.MapConnection<ClientHandler>("gameconnection", "gameconnection/{*operation}");

            Game.StartGame();
        }
    }
}