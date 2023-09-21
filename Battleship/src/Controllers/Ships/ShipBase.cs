using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Battleship.src.Controllers.Ships
{
    public class ShipBase : Entity
    {
        /* Components */
        private SpriteRenderer SpriteRenderer { get; set; }
        private Collider Collider { get; set; }
        private GameManager _gameManager;

        public Texture2D _texture;

        /* Dragging controller */
        public bool isDragging;
        public Grid GridLinkedToShip;

        /* Rotation Properties */
        private float nextRotation;
        private bool isRotating;
        public bool isReady;

        /* Ship Properties*/
        private Vector2 startDragPosition;
        public List<Vector2> inUsePositions = new List<Vector2>();


        public ShipBase(Texture2D shipTexture, Vector2 position, GameManager _gameManager) {
            SetTag(1);
            this._texture = shipTexture;
            this.Position = position;
            this._gameManager = _gameManager;

            var textureHeight = _texture.Height;
            var yOffset = (textureHeight / 32) % 2 == 0 ? -16 : 0;

            /* Componentes */
            SpriteRenderer = new SpriteRenderer(shipTexture);
            SpriteRenderer.Origin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2 + yOffset);
            AddComponent(SpriteRenderer);
            Collider = new BoxCollider();
            AddComponent(Collider);


        }

        public override void Update()
        {
            base.Update();
            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
            if (_gameManager.gameState == 0) StartShipInBoard();
            if (_gameManager.gameState == 1) ShipControllerSystem(mousePosition);

        }

        public void ShipControllerSystem(Vector2 mousePosition)
        {
            /* Game state Ship controller */
            if (isDragging) { this.Position = mousePosition; }
            if (Collider.Bounds.Contains(mousePosition))
            {
                if (Input.RightMouseButtonPressed)
                {
                    RotateShip();
                }
                else if (Input.LeftMouseButtonDown && !isDragging && _gameManager.inDragShip == null)
                {
                    OnDragStart();
                }
                else if (Input.LeftMouseButtonReleased && isDragging)
                {
                    OnDragEnd(mousePosition);
                }
            }
        }

        public void RotateShip()
        {
            nextRotation = MathHelper.ToDegrees(this.Rotation) + 90;
            if (nextRotation >= 360)
            {
                nextRotation = 0;
            }
            if (!isRotating)
            {
                if (!CollisionWithBoundsArray(GridLinkedToShip, MathHelper.ToRadians(nextRotation)))
                {
                    isRotating = true;
                    setinUsePositions(nextRotation);
                    this.TweenLocalScaleTo(1.25f, 0.05f)
                        .SetEaseType(EaseType.SineOut)
                        .SetCompletionHandler((x) =>
                        {
                            this.TweenLocalScaleTo(1f, 0.1f)
                            .SetEaseType(EaseType.SineIn)
                            .Start();
                        })
                            .Start();

                    this.TweenLocalRotationDegreesTo(nextRotation, 0.25f)
                        .SetEaseType(EaseType.SineInOut)
                        .SetCompletionHandler((x) =>
                        {
                            if (this.Rotation > 2 * Math.PI) { this.Rotation = 0; }
                            isRotating = false;

                        })
                        .Start();
                }
            }
        }
        public void OnDragStart() { 
            isDragging = true;
            _gameManager.inDragShip = this;
            startDragPosition = this.Position;
        }

        public void OnDragEnd(Vector2 mousePosition)
        {
            isDragging = false;
            _gameManager.inDragShip = null;
            if (!Collider.CollidesWithAny(out CollisionResult result))
            {
                returnToStartDragPosition();

                return;
            }

            if (CollisionWithBoundsArray(_gameManager._MouseInGrid, this.Rotation) && _gameManager._MouseInGrid != null)
            {
                returnToStartDragPosition();
            }
            else
            {
                GridLinkedToShip = _gameManager._MouseInGrid;

                if (!collisionDetection())
                {
                    returnToStartDragPosition();
                    return;
                }
                //StartShipInBoard();
                this.TweenLocalPositionTo(_gameManager._MouseInGrid.Position, 0.05f)
                    .SetEaseType(EaseType.SineOut)
                    .Start();
                setinUsePositions(this.Rotation);
            }
        }

        public void returnToStartDragPosition()
        {
            this.TweenLocalPositionTo(this.startDragPosition, 0.05f)
                .SetEaseType(EaseType.SineOut)
                .Start();
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
            var fromButtom = (int)(SpriteRenderer.Origin.Y / 32);
            var fromOrigin = (int)Math.Ceiling((_texture.Height - SpriteRenderer.Origin.Y) / 32);
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
                    (gridRelativePosition.X - fromButtom  >= 0) &&
                    (gridRelativePosition.X + fromOrigin <= BOARDDIM))
            {

                return false;
            }
            return true;
        }

        public void StartShipInBoard()
        {
            if (!isReady)
            {

                /* RandomPositioning */
                var _gridList = _gameManager.GridsList;
                int randomIndex = Nez.Random.NextInt(_gridList.Count);
                GridLinkedToShip = _gridList[randomIndex];



                /* Propiedades iniciales barco */
                this.Position = GridLinkedToShip.Position;

                if (CollisionWithBoundsArray(GridLinkedToShip, this.Rotation))
                {
                    StartShipInBoard();
                }

                /* LOGIC PROPERTIES */
                GridLinkedToShip.isInUse = true;
                setinUsePositions(this.Rotation);

                isReady = true;
            }

           
        }

        public void setinUsePositions(float _rotation)
        {
            var fromButtom = (int)(SpriteRenderer.Origin.Y / 32);
            var fromOrigin = (int)Math.Ceiling((_texture.Height - SpriteRenderer.Origin.Y) / 32);
            var orientation = (int)Math.Ceiling(Mathf.Degrees(_rotation) / 90);


            foreach (var shipPosition in inUsePositions)
            {
                _gameManager.playerMatrix[(int)shipPosition.X, (int)shipPosition.Y] = 0;
            }
            inUsePositions.Clear();

            var relativePosition = new Vector2(GridLinkedToShip._relativePosition.X, GridLinkedToShip._relativePosition.Y);
            var fromButtonPosition = relativePosition;
            var fromOriginPosition = relativePosition;
            inUsePositions.Add(relativePosition);


            if (orientation == 0)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition -= new Vector2(0, 1);
                    inUsePositions.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition += new Vector2(0, 1);
                    inUsePositions.Add(fromOriginPosition);
                }
            } else if (orientation == 1)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition += new Vector2(1, 0);
                    inUsePositions.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition -= new Vector2(1, 0);
                    inUsePositions.Add(fromOriginPosition);
                }
            }
            else if (orientation == 2)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition += new Vector2(0, 1);
                    inUsePositions.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition -= new Vector2(0, 1);
                    inUsePositions.Add(fromOriginPosition);
                }
            }
            else if (orientation == 3)
            {
                for (int i = 0; i < fromButtom; i++)
                {
                    fromButtonPosition -= new Vector2(1, 0);
                    inUsePositions.Add(fromButtonPosition);
                }

                for (int i = 1; i < fromOrigin; i++)
                {
                    fromOriginPosition += new Vector2(1, 0);
                    inUsePositions.Add(fromOriginPosition);
                }
            }

            /*
            if (!collisionDetection())
            {
                StartShipInBoard();
            } 
            */
            foreach (var shipPosition in inUsePositions)
            {
                _gameManager.playerMatrix[(int)shipPosition.X, (int)shipPosition.Y] = 2;
            }

        }
        public bool collisionDetection()
        {
            /*
            foreach (var matrixPosition in inUsePositions)
            {
                if (_gameManager.playerMatrix[(int)matrixPosition.X, (int)matrixPosition.Y] == 2)
                {
                    return true;
                }
            }
            */
            return false;
        }
    }

    
}
