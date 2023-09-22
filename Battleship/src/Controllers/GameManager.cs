using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Nez;
using Nez.UI;
using Battleship.src.Controllers.Ships;

namespace Battleship.src.Controllers
{

    public class GameManager
    {
        public int[,] playerMatrix = new int[10, 10]; // Matriz para almacenar las celdas
        public int[,] enemyMatrix = new int[10, 10]; // Matriz para almacenar las celdas
        public List<Grid> GridsList = new List<Grid>();
        public List<ShipBase> ShipList = new List<ShipBase>();
        public List<Grid> selectedGrids = new List<Grid>();
        public List<GridTiny> tinyBoardGrids = new List<GridTiny>();

        // Lista de selecciones temporales de la matriz
        public List<Vector2> tempArray = new List<Vector2>();

        /* Mouse Controllers */
        public Vector2 mousePosition;
        public Grid _MouseInGrid;


        // General Controller
        public Board _board;
        public TextureLoader _textureLoader;
        public Scene _game;

        /* Game States */
        public int gameState = 0;
        public ShipBase inDragShip;
        public bool shipsReady = true;


        // Bullets
        public int bulletCount = 4;


        public GameManager(Scene _game, TextureLoader _textureLoader) {

            this._textureLoader = _textureLoader;
            this._game = _game;

            InitializeMatrix(playerMatrix);
            InitializeMatrix(enemyMatrix);
            
            /* Tablero */
            _board = new Board(_game, _textureLoader, this);

            Entity uiCanvas = _game.CreateEntity("ui-canvas");
            var UIC = uiCanvas.AddComponent(new UICanvas());
            UIC.RenderLayer = 999;

            var StartGameButton = UIC.Stage.AddElement(new TextButton("StartGame", Skin.CreateDefaultSkin()));
            StartGameButton.SetPosition(Constants.PIX_SCREEN_WIDTH/5.25f, Constants.PIX_SCREEN_HEIGHT / 2);
            StartGameButton.SetSize(60f, 20f);
            StartGameButton.OnClicked += StartGame;


            var RestartShipsButton = UIC.Stage.AddElement(new TextButton("Restart Ships", Skin.CreateDefaultSkin()));
            RestartShipsButton.SetPosition(Constants.PIX_SCREEN_WIDTH / 5.25f, Constants.PIX_SCREEN_HEIGHT / 2 + 30);
            RestartShipsButton.SetSize(60f, 20f);
            RestartShipsButton.OnClicked += RestartShips;


            var EndTurnButton = UIC.Stage.AddElement(new TextButton("End Turn", Skin.CreateDefaultSkin()));
            EndTurnButton.SetPosition(Constants.PIX_SCREEN_WIDTH / 5.25f, Constants.PIX_SCREEN_HEIGHT / 2 + 60);
            EndTurnButton.SetSize(60f, 20f);
            EndTurnButton.OnClicked += EndTurn;

        }

        private void EndTurn(Button button)
        {
            if(gameState == 2)
            {
                foreach(var tempValue in tempArray)
                {
                    enemyMatrix[(int)tempValue.X, (int)tempValue.Y] = 1;
                }

                foreach (var grid in selectedGrids)
                {
                    grid.DetectState();
                    grid.isDestroy = true;
                }

                tempArray.Clear();
                selectedGrids.Clear();
                PrintMatrix(enemyMatrix);   
                gameState = 2;
            }
        }

        private void StartGame(Button button)
        {
            foreach (var entity in _game.FindEntitiesWithTag(1))
            {
                if (entity is ShipBase ship)
                {
                    ship.SpriteRenderer.Color = new Color(0.25f, 0.25f, 0.25f, 0.25f);

                }
            }
            _board.setTinyBoard();
            gameState = 2;
        }

        private void RestartShips(Button button)
        {
            InitializeMatrix(playerMatrix);

            foreach (var entity in _game.FindEntitiesWithTag(1))
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
            /* GameStart */
            if (gameState == 0)
            {
                foreach (var entity in _game.FindEntitiesWithTag(1))
                {
                    if (entity is ShipBase ship)
                    {
                        if (!ship.isReady)
                        {
                            shipsReady = false;
                            break;
                        }
                        if (shipsReady)
                        {
                            gameState = 1;
                        }
                    }
                }
            }
        }

        public void PrintMatrix(int[,] Matrix)
        {
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(Matrix[j, i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

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
        public void printTempArray()
        {
            foreach (Vector2 tempValues in tempArray)
            {
                Console.Write(tempValues);
            }
            Console.WriteLine();
        }

        public void GenerateRandomBoard()
        {
            Dictionary<string, int> ships = new Dictionary<string, int>
            {
                {"Carrier", 5},
                {"Battleship", 4},
                {"Cruiser", 3},
                {"Submarine", 3},
                {"Destroyer", 2}
            };
            var random = Nez.Random.NextInt(10);
        }

    }
}

/* Game States
 * 0 -> Game Starting
 * 1 -> Ship positioning
 * 2 -> grid selection missile launch
 * 3 -> Attack
 * -------------
 * 5 -> Pause game ...
*/

/* MATRIX STATES 
 *  0 -> Nothing
 *  1 -> Bomb
 *  2 -> ShipExistsAtPosition
 */

/* UI */