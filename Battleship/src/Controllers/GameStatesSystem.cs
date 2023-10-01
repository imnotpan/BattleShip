

using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Grids;
using Battleship.src.Controllers.Ships;
using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Battleship.src.Controllers
{
    public class GameStatesSystem
    {

        GameControllers GameControllers;
        Board Board;
        GameDataJSON GameDataJSON;

        //MULTIPLAYER
        public bool MultiplayerMode = false;
        public string SIDE = "CLIENT";
        public bool isShipsServerPositions = false;
        public bool isShipClientPositions = false;

        public GameStatesSystem(GameControllers GameControllers) { 
        
            this.GameControllers = GameControllers;
            this.Board = GameControllers.Board;
            this.GameDataJSON = new GameDataJSON();
        }

        public void StartGame()
        {
            Console.WriteLine("[ x ] Game Starting");

            //Ships system and matrix
            GameControllers.Board.InitializeMainBoard();
            GameControllers.Board.InitializeTinyBoard();
            GameControllers.ShipsDeploy.deploy();

            // Se despliegan botones
            GameControllers.GameHud.RedyButton.AddOnScene(GameControllers.Scene);
            GameControllers.GameHud.middleTextEntity.AddOnScene(GameControllers.Scene);

            //Multiplayer Mode
            if (MultiplayerMode)
            {
                GameControllers.MainMenuController.DisableMenu(GameControllers.MainMenuController.MultiplayerMenuStack);

            }
        

        }

        public void multiplayerSavePosition(int[] shipsPosition)
        {
            GameControllers.enemyMatrix[shipsPosition[0], shipsPosition[1]] = 2;
            GameControllers.enemyShipsPositions.Add(new Vector2(shipsPosition[0], shipsPosition[1]));
            Console.WriteLine(" [ " + SIDE +  "] " + "X: " + shipsPosition[0] + " Y: " + shipsPosition[1]);

        }

        public void ShipsReady()
        {
            var ShipsSystem = GameControllers.ShipsDeploy;
            ShipsSystem.ShipsReadyOnBoard();
            GameControllers.setTinyBoardShipsReady();

            

            foreach(ShipBase ship in ShipsSystem.ShipsList)
            {

                var Vector3List = new List<Vector3>();
                foreach (Vector2 pos in ship.inUsePositions)
                {
                    Vector3List.Add(new Vector3(pos.X, pos.Y, 0));
                }
                var JSON = GameControllers.GameDataJSON.ClientJSON("b", 0, Vector3List);
                GameControllers.GameNetworking.Client.SendDataToServer(JSON);
            }
            Console.WriteLine("[ CLIENT ] SHIPS SENT");


            if (!MultiplayerMode)
            {
                GameControllers.EnemyIA.StartShipsInBoard(); // IA STARTING SHIP

            }


            /*
            GameControllers.setTinyBoardShipsReady();
            GameControllers.GameHud.RedyButton.setSceneState(false);

            GameControllers.GameHud.CircleEntityPlayer.AddEntityOnScene(GameControllers.Scene);
            GameControllers.GameHud.CircleEntityEnemy.AddEntityOnScene(GameControllers.Scene);

            GameControllers.playerCountShips = GameControllers.playerShipsPositions.Count;
            GameControllers.enemyCountShips = GameControllers.enemyShipsPositions.Count;

            GameControllers.PrintMatrix(GameControllers.enemyMatrix);

            */
            /*
            var startProbability = Nez.Random.NextInt(100);

            if (startProbability <= 50)
            {
                PlayerStartTurn();
                Console.Write("[ x ] Is Player Turn");
            }
            else
            {
                Console.Write("[ x ] Is Enemy Turn");
                EnemyTurn();
            }
            */
        }

        public void StartMultiplayerGame()
        {

        }

        public void PlayerStartTurn()
        {
            Board.setGridsState(true);
            GameControllers.GameHud.AttackButton.setSceneState(true);
        }

        public void PlayerEndTurn()
        {
            GameControllers.GameHud.AttackButton.setSceneState(false);
            foreach (Vector2 gridPos in GameControllers.playerSelectedGrids)
            {
                foreach(Grid grid in GameControllers.GridsList)
                {
                    if (grid._relativePosition == gridPos)
                    {
                        grid.currentColor = Color.Red;
                        grid.isDestroy = true;
                        foreach (Vector2 ship in GameControllers.enemyShipsPositions)
                        {

                            if (ship == grid._relativePosition)
                            {
                                var flagPosition = grid.Position;
                                var flagEntity = new Flag(GameControllers.TextureLoader._gameTextures["Flag"], flagPosition);
                                GameControllers.Scene.AddEntity(flagEntity);
                                GameControllers.enemyCountShips--;

                            }
                        }
                    }
                }
            }


         

            GameControllers.playerSelectedGrids.Clear();
            EnemyTurn();
        }

        public void EnemyTurn()
        {
            Board.setGridsState(false);

            // Deberia recibir la informacion de la IA
            var bombs = GameControllers.EnemyIA.generateBombList();
            Console.WriteLine("[ x ] Generated bombs: ");
            foreach(var bombPosition in bombs)
            {
                foreach(GridTiny grid in GameControllers.tinyBoardGrids)
                {
                    if(grid._relativePosition == bombPosition)
                    {
                        grid.currentColor = Color.Blue;
                        foreach (Vector2 ship in GameControllers.playerShipsPositions)
                        {
                            if (ship == grid._relativePosition)
                            {
                                GameControllers.playerCountShips--;

                            }
                        }
                    }
                }



                GameControllers.playerMatrix[(int)bombPosition.X, (int)bombPosition.Y] = 1;
            }

            GameControllers.enemySelectedGrids.Clear();
            PlayerStartTurn();
        }



        public void GameFinish()
        {

        }

      

    }
}
