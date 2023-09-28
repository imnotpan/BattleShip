
using Battleship.src.Controllers.Grids;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;

namespace Battleship.src.Controllers.Ships
{
    internal class ShipSetArrayPositions
    {
        ShipBase ShipBase { get; set; }
        public ShipSetArrayPositions(ShipBase _ship)
        {
            ShipBase = _ship;
        }

        public List<Vector2> PositionValuesList(float _rotation, Grid gridLinked)
        {
            var positionList = new List<Vector2>();
            var fromButtom = (int)(ShipBase.SpriteRenderer.Origin.Y / 16);
            var fromOrigin = (int)Math.Ceiling((ShipBase._texture.Height - ShipBase.SpriteRenderer.Origin.Y) / 16);
            var orientation = (int)Math.Ceiling(_rotation / 90);

            var relativePosition = new Vector2(gridLinked._relativePosition.X,
                                                gridLinked._relativePosition.Y);
            var fromButtonPosition = relativePosition;
            var fromOriginPosition = relativePosition;
            positionList.Add(relativePosition);

            if (orientation == 0)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition -= new Vector2(0, 1);
                    positionList.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition += new Vector2(0, 1);
                    positionList.Add(fromOriginPosition);
                }
            }
            else if (orientation == 1)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition += new Vector2(1, 0);
                    positionList.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition -= new Vector2(1, 0);
                    positionList.Add(fromOriginPosition);
                }
            }
            else if (orientation == 2)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition += new Vector2(0, 1);
                    positionList.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition -= new Vector2(0, 1);
                    positionList.Add(fromOriginPosition);
                }
            }
            else if (orientation == 3)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition -= new Vector2(1, 0);
                    positionList.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition += new Vector2(1, 0);
                    positionList.Add(fromOriginPosition);
                }
            }

            return positionList;
        }

    }
}
