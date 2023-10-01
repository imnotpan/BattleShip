using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Nez;
using Nez.UI;
using Battleship.src.Controllers.Ships;
using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Grids;

namespace Battleship.src.Controllers
{

    public class GameManager
    {




        // Enemy Boards
        public Board Board;
        public EnemyIA EnemyIA;

        // General Controller
        GameControllers GameControllers;

        // Game States
        public string GameState = "MAINMENU";


        // Bullets
        public int bulletCount = 4;


        public GameManager(GameControllers GameControllers) {
            this.GameControllers = GameControllers;
        }

        public void EndTurn()
        {
            /*
            if(GameState == "PLAYERTURN")
            {
                foreach (var grid in playerSelectedGrids)
                {
                    enemyMatrix[(int)grid._relativePosition.X, (int)grid._relativePosition.Y] = 1;

                    if (enemyShipsPositions.Contains(grid._relativePosition))
                    {
                        var entityFlag = new Flag(TextureLoader._gameTextures["Flag"], grid.Position);
                        _Scene.AddEntity(entityFlag);
                        enemyShipsPositions.Remove(grid._relativePosition);
                    }
                    grid.isDestroy = true;
                    grid.currentColor = Color.Red;
                }
                GameState = "ENEMYTURN";
                playerSelectedGrids.Clear();
            }

            if (GameState == "PLAYERTURN" || GameState == "ENEMYTURN")
            {
               
                if (playerShipsPositions.Count == 0)
                {
                    Console.WriteLine("Enemy WIN");
                    GameState = "PLAYERWIN";
                }

                if (enemyShipsPositions.Count == 0)
                {
                    Console.WriteLine("Player WIN");
                    GameState = "ENEMYWIN";

                }
            }
            */
        }

        public void EnemyTurn()
        {
            /*
             // Enemy Bombs
            var IAGridSelection = EnemyIA.generateBombList();
            foreach (var grid in IAGridSelection)
            {
                playerMatrix[(int)grid._relativePosition.X, (int)grid._relativePosition.Y] = 1;
                if (playerShipsPositions.Contains(grid._relativePosition))
                {
                    playerShipsPositions.Remove(grid._relativePosition);
                }
                grid.currentColor = Color.Red;
            }
            IAGridSelection.Clear();
            GameState = "PLAYERTURN";
            */
        }

     

        public void RestartShips(Button button)
        {
            /*
            InitializeMatrix(playerMatrix);

            foreach (var entity in _Scene.FindEntitiesWithTag(1))
            {
                if (entity is ShipBase ship)
                {
                    ship.isReady = false;
                    ship.StartShipInBoard();
                }
            }
            */
        }

     

        public void setTinyBoard()
        {
            /*
            foreach (var grid in GameControllers.PlayerBoard.tinyBoardGrids)
            {
                if (playerMatrix[(int)grid._relativePosition.X, (int)grid._relativePosition.Y] == 2)
                {
                    grid.currentColor = Color.Blue;
                }
            }
            */
        }

       
    }
}

