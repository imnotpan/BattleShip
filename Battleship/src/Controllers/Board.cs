using System;
using System.Buffers.Text;
using System.Collections.Generic;
using Battleship.src.Controllers.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace Battleship.src.Controllers
{
    public class Board
    {
        // Var's
        private const int BOARD_DIM = 10;
        private const int CELL_SPACE = 1;
        private const int BOARD_SPACING = 16;


        //General Controllers
        private TextureLoader _textureLoader;
        private Scene _scene;
        private GameManager _gameManager;

        public Board(Scene _scene, TextureLoader _textureLoader, GameManager _gameManager)
        {
            this._gameManager = _gameManager;
            this._textureLoader = _textureLoader;
            this._scene = _scene;

            InitializeBoard();
            
        }
        public void InitializeBoard()
        {
            /* Textures */
            var _gridTexture = _textureLoader._gameTextures["Celda"];

            var distanceCell = _gridTexture.Width + CELL_SPACE;

            /* Start at middle Screen */
            var boardWidth = BOARD_DIM * distanceCell;
            var boardHeight = BOARD_DIM * distanceCell;
            var startX = (Constants.PIX_SCREEN_WIDTH - boardWidth) / 2;
            var startY = (Constants.PIX_SCREEN_HEIGHT - boardHeight) / 2;

            /* Create board */
            const int cellCounts = BOARD_DIM * BOARD_DIM;
            for (int i = 0; i < cellCounts; i++)
            {
                //var x = (distanceCell * (i % BOARD_DIM)) + BOARD_SPACING;
                //var y = (distanceCell * (i / BOARD_DIM)) + BOARD_SPACING;
                var x = startX + (distanceCell * (i % BOARD_DIM));
                var y = startY + (distanceCell * (i / BOARD_DIM));
                var _grid = new Grid(_gridTexture, new(x, y), new((int)(i % BOARD_DIM), (int)(i / BOARD_DIM)), _gameManager);

                _scene.AddEntity(_grid);
            }

            /* Deploy ships */
            shipBattle ship = new shipBattle(_textureLoader._gameTextures["ship_BattleShip"], new Vector2(30, 30), _gameManager);
            _scene.AddEntity(ship);

            shipBattle shipA = new shipBattle(_textureLoader._gameTextures["ship_BattleShip"], new Vector2(120, 120), _gameManager);
            _scene.AddEntity(shipA);
        }
    }
}
