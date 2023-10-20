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
        public GameDataJSON GameDataJSON;

        public GameNetworking(GameControllers GameControllers) {
            this.GameControllers = GameControllers;
            GameDataJSON = new GameDataJSON();

            //Socket Connection
            clientSocket = new clientSocket(GameDataJSON, GameControllers);
            serverSocket = new ServerSocket(GameDataJSON, GameControllers);
        }

        public void Update()
        {
 
        }
    }
}
