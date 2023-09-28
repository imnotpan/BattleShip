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
            ShipBase.startDragPosition = ShipBase.Position;
            ShipBase.isDragging = true;
        }

        public void OnDragEnd(Vector2 mousePosition, playerBoard playerBoard)
        {
            
            var collisionSystem = ShipBase.ShipCollisionSystem;
            var setInArray = ShipBase.ShipSetArrayPositions;

            if (!ShipBase.Collider.CollidesWithAny(out CollisionResult result))
            {
                returnToStartDragPosition();
                return;
            }


            if (collisionSystem.CollisionWithBoundsArray(ShipBase.GameControllers.MouseInGrid, ShipBase.Rotation) &&
                                                        ShipBase.GameControllers.MouseInGrid != null)
            {
                returnToStartDragPosition();
                return;
            }
            else
            {
                var FutureGridLinked = ShipBase.GameControllers.MouseInGrid;

                var futurePositionArray = setInArray.PositionValuesList(ShipBase.Rotation, FutureGridLinked);


                if (ShipBase.ShipCollisionSystem.collisionDetection(futurePositionArray, playerBoard))
                {
                    returnToStartDragPosition();
                    return;
                }


                ShipBase.GridLinkedToShip = ShipBase.GameControllers.MouseInGrid;


                playerBoard.SetPlayerMatrix(playerBoard.playerMatrix, ShipBase.inUsePositions, 0);
                playerBoard.SetPlayerMatrix(playerBoard.playerMatrix, futurePositionArray, 2);



                ShipBase.inUsePositions.Clear();
                ShipBase.inUsePositions = futurePositionArray;

                ShipBase.TweenLocalPositionTo(ShipBase.GameControllers.MouseInGrid.Position, 0.05f)
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
