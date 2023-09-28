using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;

namespace Battleship.src.Controllers.Ships
{
    public class ShipsSystem
    {

        TextureLoader TextureLoader;
        Scene _Scene;
        public List<ShipBase> ShipsList = new List<ShipBase>();

        public bool isDraggingShip = false;

        public ShipsSystem(GameControllers GameControllers) {

            this.TextureLoader = GameControllers.TextureLoader;
            this._Scene = GameControllers._Scene;



            // Definir las posiciones de las naves en la parte derecha de la pantalla
            Vector2 positionBattleShip = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCarrier = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCruiser = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionPatrolBoat = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);

            // Crear y agregar las naves al escenario
            ShipBase shipBattleShip = new ShipBase(TextureLoader._gameTextures["ship_BattleShip"], GameControllers);
            ShipsList.Add(shipBattleShip);

            ShipBase shipCarrier = new ShipBase(TextureLoader._gameTextures["ship_Carrier"], GameControllers);
            ShipsList.Add(shipCarrier);

            ShipBase shipPatrolBoat = new ShipBase(TextureLoader._gameTextures["ship_PatrolBoat"], GameControllers);
            ShipsList.Add(shipPatrolBoat);

            ShipBase shipCruiser = new ShipBase(TextureLoader._gameTextures["ship_Cruiser"], GameControllers);
            ShipsList.Add(shipCruiser);

        }

        public void deploy(GameControllers GameControllers)
        {
            foreach (ShipBase ship in ShipsList)
            {
                while (!ship.isReady)
                {
                   StartShipInBoard(ship, GameControllers.PlayerBoard);
                }
                if (ship.isReady)
                {
                    GameControllers._Scene.AddEntity(ship);
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

            Console.WriteLine("[ x ] Ships can move");
        }

        // Linked to ship
        public void StartShipInBoard(ShipBase ship, playerBoard playerBoard)
        {
            var CollisionSystem = ship.ShipCollisionSystem;
            var ArraySystem = ship.ShipSetArrayPositions;

            if (!ship.isReady)
            {
                // Se selecciona una casilla al azar
                var gridList = playerBoard.GridsList;
                int randomIndex = Nez.Random.NextInt(gridList.Count);
                ship.GridLinkedToShip = gridList[randomIndex];
                ship.LocalPosition = ship.GridLinkedToShip.LocalPosition;

                //  Se selecciona una rotacion al azar
                var listRotations = new List<int>() { 0, 90, 180, 270 };
                var randRotation = Nez.Random.NextInt(listRotations.Count);
                ship._shipRotation = listRotations[randRotation];

                // Se comprueba collision respecto a casilla
                if (CollisionSystem.CollisionWithBoundsArray(ship.GridLinkedToShip,ship._shipRotation))
                {
                    return;
                }



                var listPositions = ArraySystem.PositionValuesList(ship._shipRotation, ship.GridLinkedToShip);
                ship.inUsePositions.Clear();
                ship.inUsePositions = listPositions;
                Console.WriteLine("\n" + ship.Name);


                if (CollisionSystem.collisionDetection(ship.inUsePositions, playerBoard))
                {
                    return;
                }

                playerBoard.SetPlayerMatrix(playerBoard.playerMatrix, ship.inUsePositions, 2);

                foreach (var position in listPositions)
                {
                    Console.WriteLine(position.ToString());
                }
                ship.Position = ship.GridLinkedToShip.Position;
                ship.isReady = true;
            }
        }

    }
}
    