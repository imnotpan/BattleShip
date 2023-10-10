using Battleship.src.Scenes;
using System;


namespace Battleship.src.Networking
{
    public class GameNetworking
    {
        public Client Client;
        public Server Server;
        public GameControllers GameControllers;


        public clientSocket clientSocket;
        public ServerSocket serverSocket;
        public GameNetworking(GameControllers GameControllers) {
            this.GameControllers = GameControllers;

            Client = new Client(GameControllers);
            
            //clientSocket = new clientSocket();


            Server = new Server(GameControllers);
            //serverSocket = new ServerSocket();
        }

        public void Update()
        {
            Server.Update();
            Client.Update();
        }
    }
}
