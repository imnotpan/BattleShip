using Microsoft.Xna.Framework;
using Nez;
using Nez.Tweens;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src.Controllers.Ships
{
    internal class ShipRotateSystem
    {
        public ShipBase ShipBase { get; set; }
        public ShipRotateSystem(ShipBase _ship) {
            ShipBase = _ship;
        }

        public void RotateShip()
        {
            
            var nextRotation = ShipBase.RotationDegrees + 90;
            var collisionSystem = ShipBase.ShipCollisionSystem;
            var setInArray = ShipBase.ShipSetArrayPositions;
            var GameControllers = ShipBase.GameControllers;


            if (nextRotation >= 90)
            {
                nextRotation = 270;
            }


            if (nextRotation >= 360)
            {
                nextRotation = 0;
            }


            if (!ShipBase.isRotating)
            {

                if (!collisionSystem.CollisionWithBoundsArray(ShipBase.GridLinkedToShip, nextRotation))
                {
                    var futurePositionArray = setInArray.PositionValuesList(nextRotation, ShipBase.GridLinkedToShip);

                    // Limpiar las posiciones anteriores
                    GameControllers.SetMatrixValue(GameControllers.playerMatrix, ShipBase.inUsePositions, 0);

                    if (collisionSystem.collisionDetection(futurePositionArray))
                    {
                        Console.WriteLine("Collision Detection");
                        // Restaurar las posiciones anteriores
                        GameControllers.SetMatrixValue(GameControllers.playerMatrix, ShipBase.inUsePositions, 2);

                        return;
                    }

                    ShipBase.isRotating = true;


                    ShipBase.inUsePositions.Clear();
                    ShipBase.inUsePositions = futurePositionArray;
                    GameControllers.SetMatrixValue(GameControllers.playerMatrix, ShipBase.inUsePositions, 2);

                    // Animación de rotación
                    ShipBase.TweenRotationDegreesTo(nextRotation, 0.25f)
                    .SetEaseType(EaseType.SineInOut)
                    .SetCompletionHandler((x) =>
                    {
                        if(ShipBase.RotationDegrees >= 360) {  ShipBase.RotationDegrees = 0; }
                        ShipBase.isRotating = false;
                    })
                    .Start();
                }
            }

         }
    }
}
