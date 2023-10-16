

using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Grids;
using Battleship.src.Controllers.Ships;
using Battleship.src.Controllers.UI.GameHud;
using Battleship.src.Controllers.UI.GameLoopButtons;
using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;

namespace Battleship.src.Controllers
{
    public class GameStatesSystem
    {

        GameControllers GameControllers;
        Board Board;

        //MULTIPLAYER
        public bool MultiplayerMode = false;
        public string SIDE = "CLIENT";
        public bool isShipsServerPositions = false;
        public bool isShipClientPositions = false;

        public GameStatesSystem(GameControllers GameControllers) { 
        
            this.GameControllers = GameControllers;
            this.Board = GameControllers.Board;
        }




        public void StartGame()
        {
            
            //Ships system and matrix
            GameControllers.Board.InitializeMainBoard();
            GameControllers.Board.InitializeTinyBoard();
            GameControllers.ShipsDeploy.deploy();

            //GameHUD
            GameControllers.GameHud.Initialize();
            GameControllers.GameHud.setRedyViewHUD();

            // Se desabilitan los Grids
            GameControllers.Board.setGridsState(false);


        }





        public void BackToMainMenu()
        {
            GameControllers.DestroyBoardGame();
            GameControllers.MainMenuController.MainMenuInitialize();
            GameControllers.GameHud.DisableGameHud();

            GameControllers.ShipsDeploy.createShips();
        }

        //MULTIPLAYER
        public void playerOneSetPositions(int[] shipsPosition)  // ENEMY SHIPS
        {
            Console.WriteLine(" [ " + SIDE + "] " + "X: " + shipsPosition[0] + " Y: " + shipsPosition[1]);
            if (shipsPosition != null)
            {
                GameControllers.enemyMatrix[shipsPosition[0], shipsPosition[1]] = 2;
                GameControllers.enemyShipsPositions.Add(new Vector2(shipsPosition[0], shipsPosition[1]));
            }
        }



        public void ShipsReady()
        {
            
            var ShipsSystem = GameControllers.ShipsDeploy;
            ShipsSystem.ShipsReadyOnBoard();
            GameControllers.setTinyBoardShipsReady();
            GameControllers.GameHud.RedyButton.setSceneState(false);

            var mainShipPositions = new List<Vector3>();

            foreach (ShipBase ship in ShipsSystem.ShipsList)
            {
                var mainPosition = new Vector2(0,0);
                var jsonOrientation = ship.RotationDegrees/90;
                if(jsonOrientation == 3 )
                {
                    jsonOrientation = 1;
                }
                if(jsonOrientation == -1)
                {
                    jsonOrientation = 1;
                }


                if(jsonOrientation == 0)
                {
                    jsonOrientation = 1; 
                } else if(jsonOrientation == 1)
                {
                    jsonOrientation = 0;
                }


                if(ship.inUsePositions.Count > 2)
                {
                    mainPosition = ship.inUsePositions[1];
                }
                else
                {
                    mainPosition = ship.inUsePositions[0];
                }
                mainShipPositions.Add(new Vector3(mainPosition.X, mainPosition.Y, jsonOrientation));
            }


            var msg = GameControllers.GameNetworking.GameDataJSON.ClientJSON("b", 0, mainShipPositions);
            GameControllers.GameNetworking.clientSocket.sendData(msg);

        }

        public void StartAttackTurn()
        {
            GameControllers.playerSelectedGrids.Clear();
            GameControllers.Board.setGridsState(true);
            GameControllers.GameHud.setAttackViewHUD();
        }


        public void PlayerEndTurn()
        {
            GameControllers.Board.setGridsState(false);
            GameControllers.GameHud.AttackButton.setSceneState(false);

            if (GameControllers.playerSelectedGrids.Count > Constants.TOTALBOMBCOUNT)
            {
                foreach (Vector2 gridPos in GameControllers.playerSelectedGrids)
                {
                    var msg = GameControllers.GameNetworking.GameDataJSON.ClientJSON("a", 0, null, new Vector2(gridPos.X, gridPos.Y));
                    GameControllers.GameNetworking.clientSocket.sendData(msg);
                }
                GameControllers.GameHud.AttackButton.setSceneState(false);
            }

        }

        public void EnemyTurn()
        {

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
            //PlayerStartTurn();
        }


    }
}
