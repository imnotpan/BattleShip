using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Nez;
using Nez.UI;
using System.Reflection.Metadata;
using Battleship.src.Controllers.Ships;

namespace Battleship.src.Controllers
{

    public class GameManager
    {
        public int[,] playerMatrix = new int[10, 10]; // Matriz para almacenar las celdas
        public int[,] enemyMatrix = new int[10, 10]; // Matriz para almacenar las celdas
        public List<Grid> GridsList = new List<Grid>();
        public List<ShipBase> ShipList = new List<ShipBase>();


        // Lista de selecciones temporales de la matriz
        public List<Vector2> tempArray = new List<Vector2>();

        /* Mouse Controllers */
        MouseState previousMouseState;
        MouseState currentMouseState;
        public Vector2 mousePosition;
        private bool isLeftClickPressed = false;
        public Grid _MouseInGrid;


        // General Controller
        public Board _board;
        public TextureLoader _textureLoader;
        public Scene _game;

        /* Game States */
        public int gameState = 0;
        public ShipBase inDragShip;
        public bool shipsReady = true;
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


        public GameManager(Scene _game, TextureLoader _textureLoader) {

            this._textureLoader = _textureLoader;
            this._game = _game;

            InitializeMatrix(playerMatrix);
            InitializeMatrix(enemyMatrix);
            
            /* Tablero */
            _board = new Board(_game, _textureLoader, this);

        }

        public void Update()
        {
            /* Positions SHIPS */
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

            if (Input.IsKeyPressed(Keys.Space))
            {
                PrintMatrix(playerMatrix);
            }
            if (Input.IsKeyPressed(Keys.R))
            {
                foreach (var entity in _game.FindEntitiesWithTag(1))
                {
                    if (entity is ShipBase ship)
                    {
                        ship.StartShipInBoard();
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
