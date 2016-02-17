using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Threading.Tasks;

namespace SignalRGame
{
    public class Game
    {
        public static void StartGame()
        {
            _game = new Game();
            new Task(_game.Main).Start();
        }

        public static void EndGame()
        {
            _game.Stop();
            _ships.Clear();
            _handlers.Clear();
        }

        public static void AddGameShip(Ship ship)
        {
            _game.AddShip(ship);
        }

        public static void AddGameHandler(ClientHandler handler)
        {
            _game.AddHandler(handler);
        }

        public static Ship GetShipByName(string name)
        {
            return _game.GetShip(name);
        }

        public static int NumberOfShips
        {
            get { return _ships.Count; }
        }

        public static Arena Arena
        {
            get { return _arena; }
        }

        private readonly static Arena _arena = new Arena();
        internal static Game _game;

        private Game()
        {
            _stop = false;
            _handlers = new List<ClientHandler>();
            _ships =  new List<Ship>();
        }

        public void Stop()
        {
            _stop = true;
        }

        public void AddShip(Ship ship)
        {
            _ships.Add(ship);
        }

        private Ship GetShip(string name)
        {
            return _ships.First(x => x.Name == name);
        }

        public void AddHandler(ClientHandler handler)
        {
            _handlers.Add(handler);
        }

        public void Main()
        {
            while (!_stop)
            {
                var blasts = new List<Missile>();
                var ships = new List<Ship>(_ships);
                foreach (var ship in _ships)
                {
                    MoveShip(ship);
                    blasts.AddRange(ship.HitTest(ships));
                }
                _ships = ships;

                foreach ( var handler in _handlers)
                {
                    handler.Draw(_ships, blasts, _arena);
                }

                Thread.Sleep(30);
            }
        }

        private void MoveShip(Ship ship)
        {
            ship.Move();
            ship.MoveMissiles();
            ship.Decelerate();
        }

        private static bool _stop = false;
        private static List<Ship> _ships;
        private static List<ClientHandler> _handlers;
    }
}