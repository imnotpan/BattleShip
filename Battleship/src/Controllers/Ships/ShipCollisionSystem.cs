using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Battleship.src.Controllers.Ships
{
    internal class ShipCollisionSystem
    {
        ShipBase ShipBase;
        public ShipCollisionSystem(ShipBase _ship) 
        {
            ShipBase = _ship;
        }
        public bool CollisionWithBoundsArray(Grid _grid, float _rotation)
        {

            /*  
             * VerticalDown     -> 0
             * HorizontalLeft   -> 1
             * VerticalUp       -> 2
             * HorizontalRight  -> 3
             * */
            var gridRelativePosition = _grid._relativePosition;
            var fromButtom = (int)(ShipBase.SpriteRenderer.Origin.Y / 32);
            var fromOrigin = (int)Math.Ceiling((ShipBase._texture.Height - ShipBase.SpriteRenderer.Origin.Y) / 32);
            var orientation = (int)Math.Ceiling(Mathf.Degrees(_rotation) / 90);
            var BOARDDIM = 10;


            if (orientation == 0 &&
                (gridRelativePosition.Y - fromButtom >= 0 &&
                (gridRelativePosition.Y + fromOrigin <= BOARDDIM)))
            {
                return false;
            }
            else if (orientation == 1 &&
                (gridRelativePosition.X - fromOrigin + 1 >= 0) &&
                (gridRelativePosition.X + fromButtom < BOARDDIM))
            {

                return false;
            }
            else if (orientation == 2 &&
                    (gridRelativePosition.Y - fromOrigin + 1 >= 0) &&
                    (gridRelativePosition.Y + fromButtom < BOARDDIM))
            {
                return false;
            }
            else if (orientation == 3 &&
                    (gridRelativePosition.X - fromButtom >= 0) &&
                    (gridRelativePosition.X + fromOrigin <= BOARDDIM))
            {

                return false;
            }
            return true;
        }
        public bool collisionDetection(List<Vector2> usePositions)
        {

            foreach (var positions in usePositions)
            {
                if (ShipBase._gameManager.playerMatrix[(int)positions.X, (int)positions.Y] == 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
