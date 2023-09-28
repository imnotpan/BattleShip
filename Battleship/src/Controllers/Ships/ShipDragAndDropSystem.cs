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
    
        public ShipDragAndDropSystem(ShipBase _ship) 
        { 
            ShipBase = _ship;
        }

        public void OnDragStart()
        {
            /*
            ShipBase.isDragging = true;
            ShipBase.GameManager.inDragShip = ShipBase;
            ShipBase.startDragPosition = ShipBase.Position;
            */
        }

        public void OnDragEnd(Vector2 mousePosition)
        {
            /*
            ShipBase.isDragging = false;
            ShipBase.GameManager.inDragShip = null;
            var collisionSystem = ShipBase.ShipCollisionSystem;
            var setInArray = ShipBase.ShipSetArrayPositions;

            if (!ShipBase.Collider.CollidesWithAny(out CollisionResult result))
            {
                returnToStartDragPosition();
                return;
            }
            if (collisionSystem.CollisionWithBoundsArray(ShipBase.GameManager._MouseInGrid, ShipBase.Rotation) &&
                                                        ShipBase.GameManager._MouseInGrid != null)
            {
                returnToStartDragPosition();
                return;
            }
            else
            {
                var FutureGridLinked = ShipBase.GameManager._MouseInGrid;
                
                var futurePositionArray = setInArray.PositionValuesList(ShipBase.Rotation, FutureGridLinked);
                

                if (ShipBase.ShipCollisionSystem.collisionDetection(futurePositionArray)){
                    returnToStartDragPosition();
                    return;
                }
                

                ShipBase.GridLinkedToShip = ShipBase.GameManager._MouseInGrid;

                foreach (var item in ShipBase.inUsePositions) {
                    ShipBase.GameManager.playerMatrix[(int)item.X, (int)item.Y] = 0;
                }

                foreach (var item in futurePositionArray)
                {
                    ShipBase.GameManager.playerMatrix[(int)item.X, (int)item.Y] = 2;
                }

                ShipBase.inUsePositions.Clear();
                ShipBase.inUsePositions = futurePositionArray;

                ShipBase.TweenLocalPositionTo(ShipBase.GameManager._MouseInGrid.Position, 0.05f)
                    .SetEaseType(EaseType.SineOut)
                    .Start();

            }
            */
        }

        public void returnToStartDragPosition()
        {
            /*
            ShipBase.TweenLocalPositionTo(ShipBase.startDragPosition, 0.05f)
                .SetEaseType(EaseType.SineOut)
                .Start();
            */
        }


    }
}
