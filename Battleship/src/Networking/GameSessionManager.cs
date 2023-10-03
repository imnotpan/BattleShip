using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using LiteNetLib;
using Microsoft.Xna.Framework;

namespace Battleship.src.Networking
{

    public class GameSession
    {
        public int GameId { get; }

        public List<NetPeer> Players;
        public int[,] playerOneMatrix = new int[20, 20];
        public int[,] playerTwoMatrix = new int[20, 20];
        public int PlayerOneCountShips = 0;
        public int PlayerTwoCountShips = 0;
        public GameControllers GameControllers;


        //Controllers
        public bool shipsInPositionPeerOne = false;
        public bool shipsInPositionPeerTwo = false;

        public bool shipOneAttack = false;
        public bool shipTwoAttack = false;

        public Vector2 tempAttackPositionOne;
        public Vector2 tempAttackPositionTwo;
        public int isEffectiveShootOne = 0;
        public int isEffectiveShootTwo = 0;

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
            Console.WriteLine("[GAME SESSION]: " + Players.Count);

            if (Players.Count == 2)
            {
                var JSONA = GameControllers.GameDataJSON.ServerJSON("d", 1);
                GameControllers.GameNetworking.Server.SendDataToClient(peer, JSONA);


                GameControllers.GameNetworking.Server.server.DisconnectPeer(peer);
                disconnectPlayerFromSession(peer);
                Console.WriteLine("Max user per session reached");
                return;

            }

            if (Players.Count == 0)
            {

                Players.Add(peer);
                Console.WriteLine("[ GAME SESSION] Jugador añadido correctamente");

            }else if(Players.Count == 1)
            {
                Players.Add(peer);
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

        public void ShipsOnBoard(NetPeer peer, int[] p, int[] b, int[] s)
        {
            if (Players.Contains(peer))
            {
                var indexInArray = Players.IndexOf(peer);
                if(indexInArray == 0)
                {
                    AddShipsToBoard(p, playerOneMatrix, indexInArray);
                    AddShipsToBoard(b, playerOneMatrix, indexInArray);
                    AddShipsToBoard(s, playerOneMatrix, indexInArray);
                    shipsInPositionPeerOne = true;

                }
                else if(indexInArray == 1)
                {
                    AddShipsToBoard(p, playerTwoMatrix, indexInArray);
                    AddShipsToBoard(b, playerTwoMatrix, indexInArray);
                    AddShipsToBoard(s, playerTwoMatrix, indexInArray);
                    shipsInPositionPeerTwo = true;

                }

                if (PlayerOneCountShips == 6 && PlayerTwoCountShips == 6)
                {
                    foreach( var _peer in Players)
                    {
                        var JSONA = GameControllers.GameDataJSON.ServerJSON("b", 1);
                        GameControllers.GameNetworking.Server.SendDataToClient(_peer, JSONA);
                    }

                    PrintMatrix(playerTwoMatrix);
                    Console.WriteLine();
                    PrintMatrix(playerOneMatrix);

                    Console.WriteLine("[ GAME SESSION ] GAME STARTING");
                }

            }
        }


        public void GameLoop(NetPeer peer, int[] attackPosition)
        {
            if (Players.Contains(peer))
            {
               


                var indexInArray = Players.IndexOf(peer);
                if (indexInArray == 0)
                {
                    if (playerTwoMatrix[(int)attackPosition[0], (int)attackPosition[1]] == 2)
                    {
                        isEffectiveShootOne = 1;
                        PlayerTwoCountShips--;
                        if (PlayerTwoCountShips == 0)
                        {
                            // Player ONE WIN   
                            var JSONA = GameControllers.GameDataJSON.ServerJSON("w", 1);
                            GameControllers.GameNetworking.Server.SendDataToClient(Players[0], JSONA);


                            var JSONB = GameControllers.GameDataJSON.ServerJSON("w", 0);
                            GameControllers.GameNetworking.Server.SendDataToClient(Players[1], JSONB);

                        }
                    }

                    playerTwoMatrix[(int)attackPosition[0], (int)attackPosition[1]] = 1;
                    shipOneAttack = true;
                    tempAttackPositionOne = new Vector2((int)attackPosition[0], (int)attackPosition[1]);

                }
                else if(indexInArray == 1)
                {
                    if (playerOneMatrix[(int)attackPosition[0], (int)attackPosition[1]] == 2)
                    {
                        isEffectiveShootTwo = 1;
                        PlayerOneCountShips--;
                        if (PlayerOneCountShips == 0)
                        {
                            // Player Two WIN 

                            var JSONA = GameControllers.GameDataJSON.ServerJSON("w", 0);
                            GameControllers.GameNetworking.Server.SendDataToClient(Players[0], JSONA);

                            var JSONB = GameControllers.GameDataJSON.ServerJSON("w", 1);
                            GameControllers.GameNetworking.Server.SendDataToClient(Players[1], JSONB);
                        }

                    }

                    playerOneMatrix[(int)attackPosition[0], (int)attackPosition[1]] = 1;
                    tempAttackPositionTwo = new Vector2((int)attackPosition[0], (int)attackPosition[1]);
                    shipTwoAttack = true;
                }

                if(shipTwoAttack && shipOneAttack)
                {
                    var JSONA = GameControllers.GameDataJSON.ServerJSON("a",isEffectiveShootOne, tempAttackPositionOne);
                    GameControllers.GameNetworking.Server.SendDataToClient(Players[0], JSONA);

                    var JSONB = GameControllers.GameDataJSON.ServerJSON("a", isEffectiveShootTwo, tempAttackPositionTwo);
                    GameControllers.GameNetworking.Server.SendDataToClient(Players[1], JSONB);

                    shipOneAttack = false;
                    shipTwoAttack = false;
                }

             
             
            }
        }

        public void AddShipsToBoard(int[] shipsPosition, int[,] Matrix, int IndexInArray)
        {
            if (shipsPosition != null) {
                Matrix[(int)shipsPosition[0], (int)shipsPosition[1]] = 2;
                if(IndexInArray == 0)
                {
                    PlayerOneCountShips++;
                }
                if (IndexInArray == 0)
                {
                    PlayerTwoCountShips++;
                }
            }

        }
        public void PrintMatrix(int[,] Matrix)
        {
            int filas = Matrix.GetLength(0);
            int columnas = Matrix.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write(Matrix[j, i] + " ");
                }
                Console.WriteLine();
            }
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
