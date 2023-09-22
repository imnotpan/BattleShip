using Nez.Tweens;
using Nez;
using Microsoft.Xna.Framework;
using System;

namespace Battleship.src.Controllers.Ships
{
    internal class ShipDragAndDropSystem
    {
        ShipBase ShipBase;
        Grid FutureGridLinked;
    
        public ShipDragAndDropSystem(ShipBase _ship) 
        { 
            ShipBase = _ship;
        }

        public void OnDragStart()
        {
            ShipBase.isDragging = true;
            ShipBase._gameManager.inDragShip = ShipBase;
            ShipBase.startDragPosition = ShipBase.Position;

        }

        public void OnDragEnd(Vector2 mousePosition)
        {
            ShipBase.isDragging = false;
            ShipBase._gameManager.inDragShip = null;
            var collisionSystem = ShipBase.ShipCollisionSystem;
            var setInArray = ShipBase.ShipSetArrayPositions;

            if (!ShipBase.Collider.CollidesWithAny(out CollisionResult result))
            {
                returnToStartDragPosition();
                return;
            }
            if (collisionSystem.CollisionWithBoundsArray(ShipBase._gameManager._MouseInGrid, ShipBase.Rotation) &&
                                                        ShipBase._gameManager._MouseInGrid != null)
            {
                returnToStartDragPosition();
                return;
            }
            else
            {
                var FutureGridLinked = ShipBase._gameManager._MouseInGrid;
                /* Detections to future */
                
                var futurePositionArray = setInArray.PositionValuesList(ShipBase.Rotation, FutureGridLinked);
                

                if (ShipBase.ShipCollisionSystem.collisionDetection(futurePositionArray)){
                    returnToStartDragPosition();
                    return;
                }
                

                ShipBase.GridLinkedToShip = ShipBase._gameManager._MouseInGrid;

                foreach (var item in ShipBase.inUsePositions) {
                    ShipBase._gameManager.playerMatrix[(int)item.X, (int)item.Y] = 0;
                }

                foreach (var item in futurePositionArray)
                {
                    ShipBase._gameManager.playerMatrix[(int)item.X, (int)item.Y] = 2;
                }

                ShipBase.inUsePositions.Clear();
                ShipBase.inUsePositions = futurePositionArray;

                ShipBase.TweenLocalPositionTo(ShipBase._gameManager._MouseInGrid.Position, 0.05f)
                    .SetEaseType(EaseType.SineOut)
                    .Start();

            }
        }
        public void returnToStartDragPosition()
        {
            ShipBase.TweenLocalPositionTo(ShipBase.startDragPosition, 0.05f)
                .SetEaseType(EaseType.SineOut)
                .Start();
        }


    }
}
