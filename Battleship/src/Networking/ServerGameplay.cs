using Battleship.src.Controllers.Enemy;
using LiteNetLib;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Battleship.src.Networking
{
    public class ServerGameplay
    {
        public List<IPEndPoint> Players;
        public int[,] playerOneMatrix = new int[5, 5];
        public int[,] playerTwoMatrix = new int[5, 5];
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

        public bool SinglePlayer = true;
        public bool isGameSelected = false;
        public string turn = "PLAYER";


        public bool SelectPlayerOne = false;
        public bool SelectPlayerTwo = false;


        public ServerGameplay()
        {
            Players = new List<IPEndPoint>();
        }

        public void startBotGameplay()
        {
            SinglePlayer = true;
            GenerateIAShip(3);
            GenerateIAShip(2);
            GenerateIAShip(1);
            

        }

        public Vector3 GenerateBombIA()
        {
            while (true)
            {
                var xPos = Nez.Random.NextInt(5);
                var yPos = Nez.Random.NextInt(5);
                var acerto = 0;
                if (playerOneMatrix[xPos, yPos] == 1)
                {
                    continue;
                }

                if (playerOneMatrix[xPos, yPos] == 2)
                {
                    PlayerOneCountShips--;
                    acerto = 1;
                }

                playerOneMatrix[xPos, yPos] = 1;
                return new Vector3(xPos, yPos,acerto);
            }
        }

        public void GenerateIAShip(int shipSize)
        {
            var randomPositionGridX = Nez.Random.NextInt(5);
            var randomPositionGridY = Nez.Random.NextInt(5);
            List<int> listRotations = new List<int>() { 0, 1 };

            var orientation = Nez.Random.NextInt(listRotations.Count);
            var shipPosition = new Vector2(randomPositionGridX, randomPositionGridY);

            if (orientation == 0 && (shipPosition.Y + shipSize) < 5)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    var posx = (int)shipPosition.X;
                    var posy = (int)shipPosition.Y + i;
                    if (playerTwoMatrix[posx, posy] == 2)
                    {
                        GenerateIAShip(shipSize);
                        return;
                    }
                    playerTwoMatrix[posx, posy] = 2;
                }
            }
            else if (orientation == 1 && (shipPosition.X - shipSize) >= 0)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    var posx = (int)shipPosition.X - i;
                    var posy = (int)shipPosition.Y;
                    if (playerTwoMatrix[posx, posy] == 2)
                    {
                        GenerateIAShip(shipSize);
                        return;
                    }
                    playerTwoMatrix[posx, posy] = 2;
                }
            }
            else
            {
                GenerateIAShip(shipSize);
                return;
            }

            PlayerTwoCountShips = 6;
        }

        public void printMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " "); 
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();

        }



        public int attackPosition(int[] pos, IPEndPoint peer)
        {
            printMatrix(playerOneMatrix);
            printMatrix(playerTwoMatrix);
            Console.WriteLine(">>>>> JUGADA <<<<<<");

            if (SinglePlayer)
            {
                // Comprobacion contra IA
                if (Players[0].ToString() == peer.ToString())
                {
                    if (playerTwoMatrix[pos[0], pos[1]] == 2)
                    {
                        playerTwoMatrix[pos[0], pos[1]] = 1;
                        PlayerTwoCountShips--;
                        return 1;
                    }
                    else
                    {
                        playerTwoMatrix[pos[0], pos[1]] = 1;
                        return 0;
                    }
                }
            }

            if (!SinglePlayer)
            {
                if (Players[0].ToString() == peer.ToString())
                {
                    if (playerTwoMatrix[pos[0], pos[1]] == 2)
                    {
                        playerTwoMatrix[pos[0], pos[1]] = 1;
                        PlayerTwoCountShips--;
                        return 1;
                    }
                    else
                    {
                        playerTwoMatrix[pos[0], pos[1]] = 1;
                        return 0;
                    }
                }
                else if (Players[1].ToString() == peer.ToString())
                {
                    if (playerOneMatrix[pos[0], pos[1]] == 2)
                    {
                        playerOneMatrix[pos[0], pos[1]] = 1;
                        PlayerOneCountShips--;
                        return 1;
                    }
                    else
                    {
                        playerOneMatrix[pos[0], pos[1]] = 1;
                        return 0;

                    }
                }
            }


            return 0;
        }



        public void addShips(int[] p, int[] b, int[] s, int[,] Matrix )
        {


            Matrix[p[0], p[1]] = 2;


            if (b[2] == 0)
            {
                Matrix[b[0] , b[1]] = 2;
                Matrix[b[0] +1, b[1]] = 2;

            }
            if (b[2] == 1)
            {
                Matrix[b[0], b[1]] = 2;
                Matrix[b[0], b[1] + 1] = 2;
            }


            if (s[2] == 0)
            {
                Matrix[s[0], s[1]] = 2;
                Matrix[s[0] + 1, s[1]] = 2;
                Matrix[s[0] + 2, s[1]] = 2;

            }
            if (s[2] == 1)
            {
                Matrix[s[0], s[1]] = 2;
                Matrix[s[0], s[1] + 1] = 2;
                Matrix[s[0], s[1] + 2] = 2;
            }

            PlayerOneCountShips = 6;
            PlayerTwoCountShips = 6;


        }
    }
}
