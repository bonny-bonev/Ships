using System;
using System.Linq;
using System.Threading.Tasks;

using SignalR;

namespace SignalRGame
{
    public class NewPlayerHandler : PersistentConnection
    {
        protected override Task OnReceivedAsync(string clientId, string data)
        {
            int colourIndex = Game.NumberOfShips;
            if (Game.NumberOfShips > colours.Length - 1)
                colourIndex = Game.NumberOfShips % colours.Length;

            var colour = colours[colourIndex];
            var ship = new Ship() { Colour = colour, Name = data, X = 50, Y = 50 };
            Game.AddGameShip(ship);

            return Connection.Broadcast(ship);
        }
        
        private readonly string[] colours = new string[] { "red", "black", "blue", "yellow" };
    }
}