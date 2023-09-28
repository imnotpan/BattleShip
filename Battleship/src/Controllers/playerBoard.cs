using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Grids;
using Battleship.src.Controllers.Ships;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Battleship.src.Controllers
{
    public class playerBoard
    {
        // Var's
        public const int BOARD_DIM = 20;
        private const int CELL_SPACE = 1;

        //General Controllers
        TextureLoader _textureLoader;
        Scene _Scene;
        GameControllers GameControllers;

        // Grids
        public List<Grid> GridsList = new List<Grid>();
        public List<GridTiny> tinyBoardGrids = new List<GridTiny>();

        // Boards 
        public int[,] playerMatrix = new int[20, 20]; 
        public int[,] enemyMatrix = new int[20, 20];

        public playerBoard(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            this._Scene = GameControllers._Scene;
            this._textureLoader = GameControllers.TextureLoader;


            InitializeMatrix(playerMatrix);
            InitializeMatrix(enemyMatrix);
        }


        public void InitializeBoard(GameManager GameManager)
        {
            /* Board */
            var _gridTexture = _textureLoader._gameTextures["Celda"];
            var distanceCell = _gridTexture.Width + CELL_SPACE;

            var boardWidth = BOARD_DIM * distanceCell;
            var boardHeight = BOARD_DIM * distanceCell;
            var startX = (Constants.PIX_SCREEN_WIDTH - boardWidth) / 3f - 10;
            var startY = (Constants.PIX_SCREEN_HEIGHT - boardHeight) / 2 + 7;
            
            const int cellCounts = BOARD_DIM * BOARD_DIM;
            for (int i = 0; i < cellCounts; i++)
            {
                var x = startX + (distanceCell * (i % BOARD_DIM));
                var y = startY + (distanceCell * (i / BOARD_DIM));
                var gridEntity = new Grid(_gridTexture, new(x, y), new((int)(i % BOARD_DIM), (int)(i / BOARD_DIM)), GameManager);
                GridsList.Add(gridEntity);
                _Scene.AddEntity(gridEntity);
            }
        }        

        public void InitializeTinyBoard(GameManager GameManager)
        {
            /* TinyBoard */
            var _gridTinyBoard = _textureLoader._gameTextures["GridEnemy"];
            var distanceCellTinyBoard = _gridTinyBoard.Width + CELL_SPACE;
            var boardWidthTiny = BOARD_DIM * distanceCellTinyBoard;
            var boardHeightTiny = BOARD_DIM * distanceCellTinyBoard;
            var startXTinyBoard = (Constants.PIX_SCREEN_WIDTH - boardWidthTiny) / 1.15f;
            var startYTinyBoard = (Constants.PIX_SCREEN_HEIGHT - boardHeightTiny) / 1.45f + 10;

            const int cellCounts = BOARD_DIM * BOARD_DIM;

            for (int i = 0; i < cellCounts; i++)
            {
                var x = startXTinyBoard + (distanceCellTinyBoard * (i % BOARD_DIM));
                var y = startYTinyBoard + (distanceCellTinyBoard * (i / BOARD_DIM));
                var tinyGridEntity = new GridTiny(_gridTinyBoard, new(x, y), new((int)(i % BOARD_DIM), (int)(i / BOARD_DIM)), GameManager);
                tinyBoardGrids.Add(tinyGridEntity);
                _Scene.AddEntity(tinyGridEntity);
            }
        }

        public void InitializeMatrix(int[,] Matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    playerMatrix[i, j] = 0;
                }
            }
            Console.WriteLine("[ x ] Matrix Initialize");
        }



        // Función para establecer las posiciones en la matriz de jugador con un valor dado
        public int[,] SetPlayerMatrix(int[,] Matrix, List<Vector2> positions, int value)
        {
            var tempMatrix = Matrix;
            foreach (var item in positions)
            {
                tempMatrix[(int)item.X, (int)item.Y] = value;
            }
            return tempMatrix;
        }


    }
}
