﻿using Battleship.src.Controllers;
using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Grids;
using Battleship.src.Controllers.Ships;
using Battleship.src.Controllers.UI.GameHud;
using Battleship.src.MainMenu;
using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Battleship.src
{

    public class GameControllers
    {

        // MULTIPLES PARTIDAS
        


        // Game Properties General
        public ShipBase inDragShip = null;
        public Grid MouseInGrid = null;

        //Controllers / Systems
        public GameManager GameManager;
        public TextureLoader TextureLoader;
        public MainMenuController MainMenuController;
        public GameHud GameHud;
        public GameNetworking GameNetworking;
        public ShipsSystem ShipsDeploy;
        public GameStatesSystem GameStatesSystem;

        //GameController
        public Board Board;
        public EnemyIA EnemyIA;

        //GameScene
        public Scene Scene;

        // Debug text entity
        TextEntity inDragShipText;
        TextEntity MouseGridText;


        //General Gameplay Variables
        // Grids
        public List<Grid> GridsList = new List<Grid>();
        public List<GridTiny> tinyBoardGrids = new List<GridTiny>();


        public List<Vector2> enemySelectedGrids = new List<Vector2>();
        public List<Vector2> playerSelectedGrids = new List<Vector2>();


        // Boards 
        public int[,] playerMatrix = new int[5, 5];
        public int[,] enemyMatrix = new int[5, 5];
        public Vector2 mousePosition;

        //Bullets
        public List<Vector2> enemyShipsPositions = new List<Vector2>();


        // Lista de selecciones temporales de la matriz
        public List<Vector2> playerShipsPositions = new List<Vector2>();


        public int playerCountShips = -999;
        public int enemyCountShips = -999;

        public List<Flag> flagList = new List<Flag>();

        //fonts
        public SpriteFont textFont;


        public void DestroyBoardGame()
        {
            foreach (Grid grid in GridsList)
            {
                grid.Destroy();
            }

            GridsList.Clear();
            foreach(GridTiny tiny in tinyBoardGrids)
            {
                tiny.Destroy();
            }
            tinyBoardGrids.Clear();
            foreach (Flag flag in flagList)
            {
                flag.Destroy();
            }
            flagList.Clear();
            foreach(ShipBase ship in ShipsDeploy.ShipsList)
            {
                ship.Destroy();
            }
            ShipsDeploy.ShipsList.Clear(); 
            playerShipsPositions.Clear();
            playerSelectedGrids.Clear();
        }
        public void restartGameplay()
        {

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    playerMatrix[i, j] = 0;
                    enemyMatrix[i, j] = 0;
                }
            }
            playerShipsPositions.Clear();
            enemyShipsPositions.Clear();

            playerCountShips = -999;
            enemyCountShips = -999;

        }
        public GameControllers(Scene Scene)
        {
            this.Scene = Scene;
            TextureLoader = new TextureLoader(Scene.Content);
            LoadTextures();

            MainMenuController = new MainMenuController(this);
            MainMenuController.MainMenuInitialize();


            Board = new Board(this);
            ShipsDeploy = new ShipsSystem(this);
            GameHud = new GameHud(this);
            GameStatesSystem = new GameStatesSystem(this);
            EnemyIA = new EnemyIA(this);

            GameNetworking = new GameNetworking(this);




        }

        // Función para establecer las posiciones en la matriz de jugador con un valor dado
        public int[,] SetMatrixValue(int[,] Matrix, List<Vector2> positions, int value)
        {
            var tempMatrix = Matrix;
            foreach (var item in positions)
            {
                tempMatrix[(int)item.X, (int)item.Y] = value;
            }
            return tempMatrix;
        }

        public void setTinyBoardShipsReady()
        {
            foreach(var ship in ShipsDeploy.ShipsList)
            {
                foreach(var pos in ship.inUsePositions)
                {
                    foreach(var tinyGrid in tinyBoardGrids)
                    {
                        if(tinyGrid._relativePosition == pos)
                        {
                            tinyGrid.currentColor = Color.Red;
                        }
                    }
                }
            }

        }

        public void PrintMatrix(int[,] Matrix)
        {
            int filas = Matrix.GetLength(0);
            int columnas = Matrix.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write(Matrix[j, i] + " ");
                }
                Console.WriteLine();
            }
        }

 

        public void LoadTextures()
        {
            /* Load Texture */
            TextureLoader.loadTexture("Sprites/Celda");
            TextureLoader.loadTexture("Sprites/GridEnemy");

            TextureLoader.loadTexture("Sprites/Ships/ship_PatrolBoat");
            TextureLoader.loadTexture("Sprites/Ships/ship_Destructor");
            TextureLoader.loadTexture("Sprites/Ships/ship_submarine");

            TextureLoader.loadTexture("Sprites/Flag");
            textFont = Scene.Content.Load<SpriteFont>("Fonts/rockinRecordFont");
        }



        public void Update()
        {
            Board.Update();
            GameHud.Update();
            MainMenuController.Update();
            GameNetworking.Update();

            mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);

            foreach(Grid grid in GridsList)
            {
                if (grid.Collider.Bounds.Contains(mousePosition))
                {
                    MouseInGrid = grid;
                    if (Input.LeftMouseButtonPressed)
                    {
                        Console.WriteLine(grid._relativePosition);
                    }
                }
            }


    
        }

      
    }
}
