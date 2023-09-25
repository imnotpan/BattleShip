using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Nez;
using Nez.UI;
using Battleship.src.Controllers.Ships;
using Battleship.src.Controllers.Enemy;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Battleship.src.Controllers.Grids;

namespace Battleship.src.Controllers
{

    public class GameManager
    {
        public int[,] playerMatrix = new int[10, 10]; // Matriz para almacenar las celdas
        public int[,] enemyMatrix = new int[10, 10]; // Matriz para almacenar las celdas
        public List<ShipBase> ShipList = new List<ShipBase>();

        //IA Grids
        public List<GridTiny> tinyBoardGrids = new List<GridTiny>();
        public List<Grid> enemySelectedGrids = new List<Grid>();
        public List<Vector2> enemyShipsPositions = new List<Vector2>();

        // Lista de selecciones temporales de la matriz
        public List<Grid> playerSelectedGrids = new List<Grid>();
        public List<Grid> GridsList = new List<Grid>();
        public List<Vector2> shipsPositionsPlayer = new List<Vector2>();

        /* Mouse Controllers */
        public Vector2 mousePosition;
        public Grid _MouseInGrid;

        // Enemy Boards
        public playerBoard PlayerBoard;
        public EnemyIA EnemyIA;

        // General Controller
        public TextureLoader TextureLoader;
        public Scene Scene;

        /* Game States */
        public string GameState = "MAINMENU";
        public ShipBase inDragShip;
        public bool shipsReady = true;


        // Bullets
        public int bulletCount = 4;

        // Starting



        public GameManager(Scene _game, TextureLoader _textureLoader) {

            this.TextureLoader = _textureLoader;
            this.Scene = _game;

            InitializeMatrix(playerMatrix);
            InitializeMatrix(enemyMatrix);

            /* Tablero */
            PlayerBoard = new playerBoard(_game, _textureLoader, this);
            EnemyIA = new EnemyIA(this);
        }

        public void EndTurn()
        {

            if(GameState == "PLAYERTURN")
            {
                foreach (var grid in playerSelectedGrids)
                {
                    enemyMatrix[(int)grid._relativePosition.X, (int)grid._relativePosition.Y] = 1;

                    if (enemyShipsPositions.Contains(grid._relativePosition))
                    {
                        var entityFlag = new Flag(TextureLoader._gameTextures["Flag"], grid.Position);
                        Scene.AddEntity(entityFlag);
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
               
                if (shipsPositionsPlayer.Count == 0)
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
        }
        public void EnemyTurn()
        {

            /* EnemyBombs */
            var IAGridSelection = EnemyIA.generateBombList();
            foreach (var grid in IAGridSelection)
            {
                playerMatrix[(int)grid._relativePosition.X, (int)grid._relativePosition.Y] = 1;
                if (shipsPositionsPlayer.Contains(grid._relativePosition))
                {
                    shipsPositionsPlayer.Remove(grid._relativePosition);
                }
                grid.currentColor = Color.Red;
            }
            IAGridSelection.Clear();
            GameState = "PLAYERTURN";
            
        }

        public void StartGame()
        {
            var startProbability = Nez.Random.NextInt(100);
            if (startProbability <= 50)
            {
                GameState = "PLAYERTURN";
                Console.Write("PLAYERTURN");
            }
            else
            {
                GameState = "ENEMYTURN";
                Console.Write("ENEMYTURN");
            }

            foreach (var entity in Scene.FindEntitiesWithTag(1))
            {
                if (entity is ShipBase ship)
                {
                    ship.SpriteRenderer.Color = new Color(0.25f, 0.25f, 0.25f, 0.25f);

                    foreach (var pos in ship.inUsePositions)
                    {
                        shipsPositionsPlayer.Add(pos);
                    }
                }
            }

            PlayerBoard.setTinyBoard();
        }

        public void RestartShips(Button button)
        {
            InitializeMatrix(playerMatrix);

            foreach (var entity in Scene.FindEntitiesWithTag(1))
            {
                if (entity is ShipBase ship)
                {
                    ship.isReady = false;
                    ship.StartShipInBoard();
                }
            }
        }

        public void Update()
        {
            // Game Preparation

            if (GameState == "STARTING")
            {
                foreach (var entity in Scene.FindEntitiesWithTag(1))
                {
                    if (entity is ShipBase ship)
                    {
                        while (!ship.isReady)
                        {
                            ship.StartShipInBoard();
                        }
                    }
                }
                GameState = "PREPARATION";
            } 
            if(GameState == "ENEMYTURN")
            {
                EnemyTurn();
            }
        }

        private void InitializeMatrix(int[,] Matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    playerMatrix[i, j] = 0;
                }
            }
        }


    }
}

