using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Security.Principal;
using Battleship.src.Controllers.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using static System.Formats.Asn1.AsnWriter;

namespace Battleship.src.Controllers
{
    public class Board
    {
        // Var's
        public const int BOARD_DIM = 10;
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
            Console.WriteLine(Constants.PIX_SCREEN_WIDTH);
            var startX = (Constants.PIX_SCREEN_WIDTH - boardWidth) / 1.5f;
            var startY = (Constants.PIX_SCREEN_HEIGHT - boardHeight) / 2 + 15;

            /* Create Main Board board */
            const int cellCounts = BOARD_DIM * BOARD_DIM;
            for (int i = 0; i < cellCounts; i++)
            {
                var x = startX + (distanceCell * (i % BOARD_DIM));
                var y = startY + (distanceCell * (i / BOARD_DIM));
                var gridEntity = new Grid(_gridTexture, new(x, y), new((int)(i % BOARD_DIM), (int)(i / BOARD_DIM)), _gameManager);
                
                _scene.AddEntity(gridEntity);
                _gameManager.GridsList.Add(gridEntity);
            }

            /* TinyBoard */
            var _gridTinyBoard = _textureLoader._gameTextures["GridEnemy"];
            var distanceCellTinyBoard = _gridTinyBoard.Width + CELL_SPACE;

            var startXTinyBoard = (Constants.PIX_SCREEN_WIDTH - boardWidth) / 3;
            var startYTinyBoard = (Constants.PIX_SCREEN_HEIGHT - boardHeight) / 2 + 5;
            for (int i = 0; i < cellCounts; i++)
            {
                var x = startXTinyBoard + (distanceCellTinyBoard * (i % BOARD_DIM));
                var y = startYTinyBoard + (distanceCellTinyBoard * (i / BOARD_DIM));
                var tinyGridEntity = new GridTiny(_gridTinyBoard, new(x, y), new((int)(i % BOARD_DIM), (int)(i / BOARD_DIM)), _gameManager);


                _scene.AddEntity(tinyGridEntity);
                _gameManager.tinyBoardGrids.Add(tinyGridEntity);


            }

            // Definir las posiciones de las naves en la parte derecha de la pantalla
            Vector2 positionBattleShip = new Vector2(Constants.PIX_SCREEN_WIDTH/2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCarrier = new Vector2(Constants.PIX_SCREEN_WIDTH/2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCruiser = new Vector2(Constants.PIX_SCREEN_WIDTH/2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionPatrolBoat = new Vector2(Constants.PIX_SCREEN_WIDTH/2, Constants.PIX_SCREEN_HEIGHT);

            // Crear y agregar las naves al escenario
            ShipBase shipBattleShip = new ShipBase(_textureLoader._gameTextures["ship_BattleShip"], positionBattleShip, _gameManager);
            _scene.AddEntity(shipBattleShip);

            ShipBase shipCarrier = new ShipBase(_textureLoader._gameTextures["ship_Carrier"], positionCarrier, _gameManager);
            _scene.AddEntity(shipCarrier);

            ShipBase shipPatrolBoat = new ShipBase(_textureLoader._gameTextures["ship_PatrolBoat"], positionPatrolBoat, _gameManager);
            _scene.AddEntity(shipPatrolBoat);

            ShipBase shipCruiser = new ShipBase(_textureLoader._gameTextures["ship_Cruiser"], positionCruiser, _gameManager);
            _scene.AddEntity(shipCruiser);

        }

        public void setTinyBoard()
        {
            foreach(var grid in _gameManager.tinyBoardGrids)
            {
                if (_gameManager.playerMatrix[(int)grid._relativePosition.X, (int)grid._relativePosition.Y] == 2)
                {
                    grid.shipInGrid = true;
                }
            }
        }

    }
}
