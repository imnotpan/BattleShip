using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Battleship.src.Controllers.Enemy
{
    public class EnemyIA
    {
        public bool isReady;
        private GameManager GameManager;
        private GameControllers GameControllers;

        private List<int> ships = new List<int>() {5, 4, 3, 3, 2};
        private List<int> listRotations = new List<int>() { 0, 1};
        private int BOARDDIM = 20;
        private int BOMBCOUNT = Constants.TOTALBOMBCOUNT;

        public EnemyIA(GameControllers GameController)
        {
            this.GameControllers = GameController;
            this.GameManager = GameController.GameManager;
        }

        public void StartShipsInBoard()
        {
            foreach (int shipSize in ships)
            {
                GenerateIAShip(shipSize);
            }
        }

        public void GenerateIAShip(int shipSize)
        {
            var randomPositionGridX = Nez.Random.NextInt(BOARDDIM);
            var randomPositionGridY = Nez.Random.NextInt(BOARDDIM);
            var orientation = Nez.Random.NextInt(listRotations.Count);
            var shipPosition = new Vector2(randomPositionGridX, randomPositionGridY);

            if (orientation == 0 && (shipPosition.Y + shipSize) < BOARDDIM)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    var posx = (int)shipPosition.X;
                    var posy = (int)shipPosition.Y+i;
                    if(GameControllers.enemyMatrix[posx, posy] == 2) { 
                        GenerateIAShip(shipSize);
                        return;
                    }
                    GameControllers.enemyMatrix[posx, posy] = 2;
                    GameControllers.enemyShipsPositions.Add(new Vector2(posx, posy));
                }
            } else if (orientation == 1 && (shipPosition.X - shipSize) >= 0)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    var posx = (int)shipPosition.X-i;
                    var posy = (int)shipPosition.Y;
                    if (GameControllers.enemyMatrix[posx, posy] == 2)
                    {
                        GenerateIAShip(shipSize);
                        return;
                    }
                    GameControllers.enemyMatrix[posx, posy] = 2;
                    GameControllers.enemyShipsPositions.Add(new Vector2(posx, posy));
                }
            }
           else
            {
                GenerateIAShip(shipSize);
                return;
            }

        }


        public List<Vector2> generateBombList()
        {
            var bombsList = new List<Vector2>();
            for(int i = 0; i <= BOMBCOUNT; i++)
            {
                bombsList.Add(bomb());
            }

            return bombsList;
        }

        public Vector2 bomb()
        {
            
            var tinyBoardGrids = GameControllers.tinyBoardGrids;
            var randomGridTiny = Nez.Random.NextInt(tinyBoardGrids.Count);
            var tinyGridRandom = tinyBoardGrids[randomGridTiny];

            while(GameControllers.playerMatrix[(int)tinyGridRandom._relativePosition.X, (int)tinyGridRandom._relativePosition.Y] == 1)
            {
                randomGridTiny = Nez.Random.NextInt(tinyBoardGrids.Count);
                tinyGridRandom = tinyBoardGrids[randomGridTiny];
            }
            return tinyGridRandom._relativePosition;
            
        }



    }
}
