using Nez.Tweens;
using Nez;
using Microsoft.Xna.Framework;
using System;
using Battleship.src.Controllers.Grids;

namespace Battleship.src.Controllers.Ships
{
    internal class ShipDragAndDropSystem
    {
        ShipBase ShipBase;
        Grid FutureGridLinked;
        Board Board;
    
        public ShipDragAndDropSystem(ShipBase _ship) 
        { 
            ShipBase = _ship;
        }

        public void OnDragStart()
        {
            ShipBase.startDragPosition = ShipBase.Position;
            ShipBase.isDragging = true;
        }

        public void OnDragEnd(Vector2 mousePosition)
        {
            
            var collisionSystem = ShipBase.ShipCollisionSystem;
            var setInArray = ShipBase.ShipSetArrayPositions;

            if (collisionSystem.CollisionWithBoundsArray(ShipBase.GameControllers.MouseInGrid, ShipBase.RotationDegrees) &&
                                                         ShipBase.GameControllers.MouseInGrid != null)
            {
                Console.WriteLine("COLLIDES WITH BOUNDS  ORIENTATION: " + ShipBase.RotationDegrees/9);
                returnToStartDragPosition();
                return;
            }
            else
            {
                var FutureGridLinked = ShipBase.GameControllers.MouseInGrid;
                var futurePositionArray = setInArray.PositionValuesList(ShipBase.RotationDegrees, FutureGridLinked);
                ShipBase.GameControllers.SetMatrixValue(ShipBase.GameControllers.playerMatrix, ShipBase.inUsePositions, 0);

                if (ShipBase.ShipCollisionSystem.collisionDetection(futurePositionArray))
                {
                    Console.WriteLine("COLLIDES WITH SHIP");
                    returnToStartDragPosition();
                    return;
                }

                ShipBase.GridLinkedToShip = ShipBase.GameControllers.MouseInGrid;
                ShipBase.GameControllers.SetMatrixValue(ShipBase.GameControllers.playerMatrix, futurePositionArray, 2);
                ShipBase.inUsePositions.Clear();
                ShipBase.inUsePositions = futurePositionArray;
                ShipBase.TweenLocalPositionTo(ShipBase.GridLinkedToShip.Position, 0.05f)
                        .SetEaseType(EaseType.SineOut)
                        .Start();
            }
        }
            

        public void returnToStartDragPosition()
        {
            ShipBase.GameControllers.SetMatrixValue(ShipBase.GameControllers.playerMatrix, ShipBase.inUsePositions, 2);

            ShipBase.TweenLocalPositionTo(ShipBase.startDragPosition, 0.05f)
                .SetEaseType(EaseType.SineOut)
                .Start();
            
        }


    }
}
