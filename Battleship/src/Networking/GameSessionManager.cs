using System;
using System.Collections.Generic;
using LiteNetLib;

namespace Battleship.src.Networking
{
    public class GameSessionManager
    {



        public class GameSession
        {
            public int GameId { get; }
            public NetPeer PlayerOne { get; }
            public NetPeer PlayerTwo { get; }
            public int[,] playerOneMatrix = new int[20, 20];
            public int[,] playerTwoMatrix = new int[20, 20];

            public GameSession(NetPeer playerOne, NetPeer playerTwo, int gameId, int[,] playerOneMatrix, int[,] playerTwoMatrix)
            {
                PlayerOne = playerOne;
                PlayerTwo = playerTwo;
                this.playerOneMatrix = playerOneMatrix;
                this.playerTwoMatrix = playerTwoMatrix;

                GameId = gameId;
                
                // Initialize game state and other properties.


            }
        }


        private Dictionary<int, GameSession> activeGames = new Dictionary<int, GameSession>();
        private int nextGameId = 1;
        public int[,] playerOneMatrix = new int[20, 20];
        public int[,] playerTwoMatrix = new int[20, 20];


        public int CreateNewGame(NetPeer playerOne, NetPeer playerTwo)
        {
            int gameId = nextGameId++;
            var gameSession = new GameSession(playerOne, playerTwo, gameId, playerOneMatrix, playerTwoMatrix);
            activeGames.Add(gameId, gameSession);
            return gameId;
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
