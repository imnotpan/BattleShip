using System;
using System.Collections.Generic;
using LiteNetLib;

namespace Battleship.src.Networking
{

    public class GameSession
    {
        public int GameId { get; }

        public List<NetPeer> Players;
        public int[,] playerOneMatrix = new int[20, 20];
        public int[,] playerTwoMatrix = new int[20, 20];
        public int enemyCountShips = -999;
        public int playerCountShips = -999;
        public GameControllers GameControllers;


        public GameSession(int gameId, int[,] playerOneMatrix, int[,] playerTwoMatrix, GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            this.playerOneMatrix = playerOneMatrix;
            this.playerTwoMatrix = playerTwoMatrix;
            Players = new List<NetPeer>();

            GameId = gameId;
        }

        public void addPlayerToSession(NetPeer peer)
        {
            if(Players.Count < 2)
            {
                Players.Add(peer);
                Console.WriteLine("[ GAME SESSION] Jugador añadido correctamente");

            }

            if(Players.Count == 2)
            {
                var JSON = GameControllers.GameDataJSON.ServerJSON("c", 1);
                foreach(var _peer in Players)
                {
                    GameControllers.GameNetworking.Server.SendDataToClient(_peer, JSON);

                }

                Console.WriteLine("Two ships are ready ");
            }

        }

        public void disconnectPlayerFromSession(NetPeer peer)
        {
            Players.Remove(peer);
        }

    }


    public class GameSessionManager
    {
        private Dictionary<int, GameSession> activeGames = new Dictionary<int, GameSession>();
        private int nextGameId = 1;
        public int[,] playerOneMatrix = new int[20, 20];
        public int[,] playerTwoMatrix = new int[20, 20];


        public void CreateNewGame(GameControllers GameControllers)
        {
            int gameId = nextGameId++;
            var gameSession = new GameSession(gameId, playerOneMatrix, playerTwoMatrix, GameControllers);
            activeGames.Add(gameId, gameSession);
            Console.WriteLine("[ Session Manager ] Session Created: " + gameId);   
        }

        public void CreateNewGame(int _gameID, GameControllers GameControllers)
        {
            int gameId = _gameID;
            var gameSession = new GameSession(gameId, playerOneMatrix, playerTwoMatrix, GameControllers);
            activeGames.Add(gameId, gameSession);
            Console.WriteLine("[ Session Manager ] Session Created: " + gameId);
        }

        public void RemoveGame(int gameId)
        {
            if (activeGames.ContainsKey(gameId))
            {
                activeGames.Remove(gameId);
            }
        }

        public GameSession GetGame(int gameId)
        {
            return activeGames.ContainsKey(gameId) ? activeGames[gameId] : null;
        }

     
    }
}
