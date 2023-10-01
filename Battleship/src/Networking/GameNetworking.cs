using Battleship.src.Scenes;
using System;


namespace Battleship.src.Networking
{
    public class GameNetworking
    {
        public Client Client;
        public Server Server;
        public GameControllers GameControllers;




        public GameNetworking(GameControllers GameControllers) {
            this.GameControllers = GameControllers;

            Client = new Client(GameControllers);
            Server = new Server(GameControllers);

        }

        public void Update()
        {
            Server.Update();
            Client.Update();
        }
    }
}
