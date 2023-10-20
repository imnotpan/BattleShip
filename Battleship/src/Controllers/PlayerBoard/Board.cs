using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Grids;
using Battleship.src.Controllers.Ships;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Battleship.src.Controllers
{
    public class Board
    {
        // Var's
        public const int BOARD_DIM = 5;
        private const int CELL_SPACE = 1;

        //General Controllers
        public TextureLoader TextureLoader;
        public Scene _Scene;
        public GameControllers GameControllers;


        public Board(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            TextureLoader = GameControllers.TextureLoader;
            _Scene = GameControllers.Scene;

            InitializeMatrix(GameControllers.playerMatrix);
            InitializeMatrix(GameControllers.enemyMatrix);
        }

        

        public void InitializeMatrix(int[,] Matrix)
        {
            for (int i = 0; i < BOARD_DIM; i++)
            {
                for (int j = 0; j < BOARD_DIM; j++)
                {
                    Matrix[i, j] = 0;
                }
            }
        }

        public void InitializeMainBoard()
        {
            var _gridTexture = TextureLoader._gameTextures["Celda"];
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
                var gridEntity = new Grid(_gridTexture, new(x, y), new((int)(i % BOARD_DIM), (int)(i / BOARD_DIM)));
                GameControllers.GridsList.Add(gridEntity);
                _Scene.AddEntity(gridEntity);
            }

        }

        public void InitializeTinyBoard()
        {
            var _gridTinyBoard = TextureLoader._gameTextures["GridEnemy"];
            var distanceCellTinyBoard = _gridTinyBoard.Width + CELL_SPACE;
            var boardWidthTiny = BOARD_DIM * distanceCellTinyBoard;
            var boardHeightTiny = BOARD_DIM * distanceCellTinyBoard;
            var startXTinyBoard = (Constants.PIX_SCREEN_WIDTH - boardWidthTiny) / 3f + 128;
            var startYTinyBoard = (Constants.PIX_SCREEN_HEIGHT - boardHeightTiny) / 2 + 7;

            const int cellCounts = BOARD_DIM * BOARD_DIM;

            for (int i = 0; i < cellCounts; i++)
            {
                var x = startXTinyBoard + (distanceCellTinyBoard * (i % BOARD_DIM));
                var y = startYTinyBoard + (distanceCellTinyBoard * (i / BOARD_DIM));
                var tinyGridEntity = new GridTiny(_gridTinyBoard, new(x, y), new((int)(i % BOARD_DIM), (int)(i / BOARD_DIM)));
                GameControllers.tinyBoardGrids.Add(tinyGridEntity);
                _Scene.AddEntity(tinyGridEntity);
            }
        }



        public void setGridsState(bool i)
        {
            foreach(Grid grid in GameControllers.GridsList)
            {
                grid.canClickOnGrid = i;
            }
        }



        public void Update()
        {

            foreach(Grid grid in GameControllers.GridsList)
            {
                
                if (grid.Collider.Bounds.Contains(GameControllers.mousePosition))
                {
                    if (grid.SpriteRenderer.Color != grid.overColor)
                    {
                        grid.SpriteRenderer.Color = grid.overColor;
                    }

                    if (grid.canClickOnGrid && !grid.isDestroy)
                    {
                        if (Input.LeftMouseButtonPressed)
                        {
                            Console.WriteLine("buttonPressed");

                            if (!grid.isOnTempArray && (GameControllers.playerSelectedGrids.Count <= Constants.TOTALBOMBCOUNT))
                            {
                                grid.isOnTempArray = true;
                                grid.currentColor = Color.Yellow;
                                GameControllers.playerSelectedGrids.Add(grid._relativePosition);
                                Console.WriteLine("BOMB AT POSITION:" + grid._relativePosition.ToString());

                            }else if (grid.isOnTempArray)
                            {
                                grid.isOnTempArray = false;
                                grid.currentColor = Color.White;

                                GameControllers.playerSelectedGrids.Remove(grid._relativePosition);

                            }
                            grid.GridClickAnim();
                        }
                    }
                }
                else
                {
                    if (grid.SpriteRenderer.Color != grid.currentColor)
                    {
                        grid.SpriteRenderer.Color = grid.currentColor;
                    }
                }
            }

        }
    }
}
