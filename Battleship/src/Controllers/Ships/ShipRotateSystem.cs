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
            var nextRotation = MathHelper.ToDegrees(ShipBase.Rotation) + 90;
            var collisionSystem = ShipBase.ShipCollisionSystem;
            var setInArray = ShipBase.ShipSetArrayPositions;

            if (nextRotation >= 360)
            {
                nextRotation = 0;
            }
            if (!ShipBase.isRotating)
            {
                if (!collisionSystem.CollisionWithBoundsArray(ShipBase.GridLinkedToShip, MathHelper.ToRadians(nextRotation)))
                {
                    var futurePositionArray = setInArray.PositionValuesList(MathHelper.ToRadians(nextRotation), ShipBase.GridLinkedToShip);

                    // Limpiar las posiciones anteriores
                    ClearPlayerMatrix(ShipBase.inUsePositions);

                    if (collisionSystem.collisionDetection(futurePositionArray))
                    {
                        Console.WriteLine("Collision Detection");
                        // Restaurar las posiciones anteriores
                        SetPlayerMatrix(ShipBase.inUsePositions, 2);
                        return;
                    }

                    ShipBase.isRotating = true;

                    // Limpiar las posiciones anteriores
                    ClearPlayerMatrix(ShipBase.inUsePositions);
                    // Establecer las nuevas posiciones
                    SetPlayerMatrix(futurePositionArray, 2);

                    ShipBase.inUsePositions.Clear();
                    ShipBase.inUsePositions = futurePositionArray;

                    // Animación de escala
                    ShipBase.TweenLocalScaleTo(1.25f, 0.05f)
                        .SetEaseType(EaseType.SineOut)
                        .SetCompletionHandler((x) =>
                        {
                            ShipBase.TweenLocalScaleTo(1f, 0.1f)
                            .SetEaseType(EaseType.SineIn)
                            .Start();
                        })
                        .Start();

                    // Animación de rotación
                    ShipBase.TweenLocalRotationDegreesTo(nextRotation, 0.25f)
                        .SetEaseType(EaseType.SineInOut)
                        .SetCompletionHandler((x) =>
                        {
                            if (ShipBase.Rotation > 2 * Math.PI) { ShipBase.Rotation = 0; }
                            ShipBase.isRotating = false;
                        })
                        .Start();
                }
            }
            // Función para limpiar las posiciones en la matriz de jugador
            void ClearPlayerMatrix(List<Vector2> positions)
            {
                foreach (var item in positions)
                {
                    ShipBase.GameManager.playerMatrix[(int)item.X, (int)item.Y] = 0;
                }
            }

            // Función para establecer las posiciones en la matriz de jugador con un valor dado
            void SetPlayerMatrix(List<Vector2> positions, int value)
            {
                foreach (var item in positions)
                {
                    ShipBase.GameManager.playerMatrix[(int)item.X, (int)item.Y] = value;
                }
            }
        }
    }
}
