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
        //public List<Grid> gridCells = new List<Grid>(); // Lista para almacenar las celdas
        //public List<shipBattle> boatsArray = new List<shipBattle>(); // Lista para almacenar las celdas

        // Lista de selecciones temporales de la matriz
        public List<Vector2> tempArray = new List<Vector2>();

        /* Mouse Controllers */
        MouseState previousMouseState;
        MouseState currentMouseState;
        public Vector2 mousePosition;
        private bool isLeftClickPressed = false;
        public Grid _MouseInGrid;


        // General Controller
        private Board _board;
        private TextureLoader _textureLoader;
        private Scene _game;

        /* MATRIX STATES 
         *  0 -> Destroy
         *  1 -> Avaiable
         *  2 -> ShipExistsAtPosition
         */


        public GameManager(Scene _game, TextureLoader _textureLoader) {

            this._textureLoader = _textureLoader;
            this._game = _game;

            /* Tablero */
            _board = new Board(_game, _textureLoader, this);

            InitializeMatrix(playerMatrix);
            InitializeMatrix(enemyMatrix);
        }

        public void PrintMatrix(int[,] Matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // Por ejemplo, inicializamos todas las celdas del jugador a 'false' (no golpeadas).
                    Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        private void InitializeMatrix(int[,] Matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // Por ejemplo, inicializamos todas las celdas del jugador a 'false' (no golpeadas).
                    playerMatrix[i, j] = 1;
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
