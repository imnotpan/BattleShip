using Battleship.src.Scenes;
using System;


namespace Battleship.src.Networking
{
    public class GameNetworking
    {
        public Client Client;
        public Server Server;

        public GameNetworking(GameScene GameScene) {

            Client = new Client();
            Server = new Server();

        }

        public void HostServer()
        {
            Server.Start();
        }

        public void ConnectToServer() 
        {
            Client.Connection();
            Console.WriteLine("Connected Success");
        }

        public void Update()
        {
            if (Server.server.IsRunning)
            {
                Server.Update();
            }
        }

    }
}
