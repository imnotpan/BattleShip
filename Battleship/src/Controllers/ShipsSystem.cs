using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Nez;
using Nez.Tweens;
using System;
using System.Collections.Generic;

namespace Battleship.src.Controllers.Ships
{
    public class ShipsSystem
    {

        TextureLoader TextureLoader;
        Scene _Scene;
        public GameControllers GameControllers;

        public List<ShipBase> ShipsList = new List<ShipBase>();

        public bool isDraggingShip = false;

        public ShipsSystem(GameControllers GameControllers) {

            this.GameControllers = GameControllers;
            this.TextureLoader = GameControllers.TextureLoader;
            this._Scene = GameControllers.Scene;

            // Definir las posiciones de las naves en la parte derecha de la pantalla
            Vector2 positionBattleShip = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCarrier = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCruiser = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionPatrolBoat = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);

            // Crear y agregar las naves al escenario

            /*
            ShipBase shipCarrier = new ShipBase(TextureLoader._gameTextures["ship_Carrier"], this);
            ShipsList.Add(shipCarrier);
            
            ShipBase shipCruiser = new ShipBase(TextureLoader._gameTextures["ship_Cruiser"], this);
            ShipsList.Add(shipCruiser);

            ShipBase shipBattleShip = new ShipBase(TextureLoader._gameTextures["ship_BattleShip"], this);
            ShipsList.Add(shipBattleShip);
            */

            // Patrol 1 GRID
            // Destructor 2 GRIDS
            // Submarine 3 GRIDS

            ShipBase shipPatrolBoat = new ShipBase(TextureLoader._gameTextures["ship_PatrolBoat"], this);
            ShipsList.Add(shipPatrolBoat);

            ShipBase shipDestructor = new ShipBase(TextureLoader._gameTextures["ship_Destructor"], this);
            ShipsList.Add(shipDestructor);
            ShipBase shipSubmarine = new ShipBase(TextureLoader._gameTextures["ship_submarine"], this);
            ShipsList.Add(shipSubmarine);

        }

        public void deploy()
        {
            
            foreach (ShipBase ship in ShipsList)
            {
                while (!ship.isReady)
                {
                   StartShipInBoard(ship);
                }
                if (ship.isReady)
                {
                    _Scene.AddEntity(ship);
                }
            }
            

        }
        public void canMove()
        {
            foreach (ShipBase ship in ShipsList)
            {
   
                if (!ship.canMove)
                {
                    ship.canMove = true;
                }
            }

        }

        // Linked to ship
        public void StartShipInBoard(ShipBase ship)
        {
            
            var CollisionSystem = ship.ShipCollisionSystem;
            var ArraySystem = ship.ShipSetArrayPositions;

            if (!ship.isReady)
            {
                // Se selecciona una casilla al azar
                var gridList = GameControllers.GridsList;
                int randomIndex = Nez.Random.NextInt(gridList.Count);
                ship.GridLinkedToShip = gridList[randomIndex];
                ship.LocalPosition = ship.GridLinkedToShip.LocalPosition;

                //  Se selecciona una rotacion al azar
                var listRotations = new List<int>() { 0, 90, 180, 270 };
                var randRotation = Nez.Random.NextInt(listRotations.Count);
                var tempRotation = listRotations[randRotation];

                // Se comprueba collision respecto a casilla
                if (CollisionSystem.CollisionWithBoundsArray(ship.GridLinkedToShip, tempRotation))
                {
                    return;
                }

                var listPositions = ArraySystem.PositionValuesList(tempRotation, ship.GridLinkedToShip);
                ship.inUsePositions.Clear();
                ship.inUsePositions = listPositions;
                Console.WriteLine("\n" + ship.Name);


                if (CollisionSystem.collisionDetection(ship.inUsePositions))
                {
                    return;
                }

                foreach (Vector2 i in ship.inUsePositions)
                {
                    GameControllers.playerShipsPositions.Add(i);
                }

                GameControllers.SetMatrixValue(GameControllers.playerMatrix, ship.inUsePositions, 2);


                ship.Position = ship.GridLinkedToShip.Position;

                // Solucion temporal
                ship.TweenRotationDegreesTo(tempRotation, 0.001f)
                    .SetEaseType(EaseType.Linear)
                    .SetCompletionHandler((x) =>
                    {
                        if (ship.RotationDegrees >= 360) { ship.RotationDegrees = 0; }
                        ship.RotationDegrees = tempRotation;
                        Console.WriteLine(ship.RotationDegrees);
                    })
                    .Start();

                ship.isReady = true;
            }
        }

        public void ShipsReadyOnBoard()
        {
            foreach (ShipBase ship in ShipsList)
            {

                if (ship.canMove)
                {
                    ship.canMove = false;
                    ship.SpriteRenderer.Color = new Color(0f, 0f, 0f, 0.25f);

                }
            }
        }

    }
}
    