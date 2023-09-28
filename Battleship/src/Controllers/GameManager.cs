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



        //IA Grids
        public List<Grid> enemySelectedGrids = new List<Grid>();
        public List<Vector2> enemyShipsPositions = new List<Vector2>();

        // Lista de selecciones temporales de la matriz
        public List<Grid> playerSelectedGrids = new List<Grid>();
        public List<Vector2> playerShipsPositions = new List<Vector2>();

        /* Mouse Controllers */
        public Vector2 mousePosition;
        public Grid _MouseInGrid;

        // Enemy Boards
        public playerBoard PlayerBoard;
        public EnemyIA EnemyIA;

        // General Controller
        TextureLoader TextureLoader;
        GameControllers GameControllers;
        Scene _Scene;

        /* Game States */
        public string GameState = "MAINMENU";
        public ShipBase inDragShip;
        public bool shipsReady = true;


        // Bullets
        public int bulletCount = 4;

        // IP CONNECTION
        public string IPCONNECTION;


        public GameManager(GameControllers GameControllers) {

            this.GameControllers = GameControllers;
            this.TextureLoader = GameControllers.TextureLoader;
            this._Scene = GameControllers._Scene;


        }


        public void StartGame()
        {
            Console.WriteLine("[ x ] Game Starting");
            GameControllers.PlayerBoard.InitializeBoard(this);
            GameControllers.PlayerBoard.InitializeTinyBoard(this);

            // Probabilidad Inicial
            var startProbability = Nez.Random.NextInt(100);
            if (startProbability <= 50)
            {
                GameState = "PLAYERTURN";
                Console.Write("[ x ] Is Player Turn");
            }
            else
            {
                GameState = "ENEMYTURN";
                Console.Write("[ x ] Is Enemy Turn");
            }

            foreach (ShipBase ship in GameControllers.ShipsDeploy.ShipsList)
            {
                while (!ship.isReady)
                {
                    ship.StartShipInBoard(GameControllers.PlayerBoard);
                }
                if (ship.isReady)
                {
                    Console.WriteLine(ship.inUsePositions);

                }
            }





            /*
            foreach (var entity in _Scene.FindEntitiesWithTag(1))
            {
                if (entity is ShipBase ship)
                {
                    ship.SpriteRenderer.Color = new Color(0.25f, 0.25f, 0.25f, 0.25f);

                    foreach (var pos in ship.inUsePositions)
                    {
                        playerShipsPositions.Add(pos);
                    }
                }
            }
            /*
            
            GameState = "PREPARATION";
            */
            //setTinyBoard();
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

