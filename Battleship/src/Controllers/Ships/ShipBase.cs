using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using System;
using System.Collections.Generic;


namespace Battleship.src.Controllers.Ships
{
    public class ShipBase : Entity
    {
        /* Components */
        public SpriteRenderer SpriteRenderer { get; set; }
        public Collider Collider { get; set; }
        public GameManager _gameManager;

        public Texture2D _texture;

        /* Dragging controller */
        public bool isDragging;
        public Grid GridLinkedToShip;

        /* Rotation Properties */
        public bool isRotating;
        public bool isReady;

        /* Ship Properties*/
        public Vector2 startDragPosition;
        public List<Vector2> inUsePositions = new List<Vector2>();

        /* Systems */
        internal ShipRotateSystem ShipRotateSystem { get; set; }
        internal ShipDragAndDropSystem ShipDragAndDropSystem { get; set; }
        internal ShipCollisionSystem ShipCollisionSystem { get; set; }
        internal ShipSetArrayPositions ShipSetArrayPositions { get; set; } 

        public ShipBase(Texture2D shipTexture, Vector2 position, GameManager _gameManager) {
            SetTag(1);
            this._texture = shipTexture;
            this.Position = position;
            this._gameManager = _gameManager;

            var textureHeight = _texture.Height;
            var yOffset = (textureHeight / 32) % 2 == 0 ? -16 : 0;

            /* Componentes */
            SpriteRenderer = new SpriteRenderer(shipTexture);
            SpriteRenderer.RenderLayer = -1;
            SpriteRenderer.Origin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2 + yOffset);
            Collider = new BoxCollider();

            AddComponent(SpriteRenderer);
            AddComponent(Collider);

            /* Systems */
            ShipRotateSystem = new ShipRotateSystem(this);
            ShipDragAndDropSystem = new ShipDragAndDropSystem(this);
            ShipCollisionSystem = new ShipCollisionSystem(this);
            ShipSetArrayPositions = new ShipSetArrayPositions(this);
        }

        public override void Update()
        {
            base.Update();
            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
            if (_gameManager.gameState == 0) StartShipInBoard();
            if (_gameManager.gameState == 1) shipControllers(mousePosition);
  
        }
        public void shipControllers(Vector2 mousePosition)
        {
            /* Game state Ship controller */
            if (isDragging) { Position = mousePosition; }
            if (Collider.Bounds.Contains(mousePosition))
            {
                if (Input.RightMouseButtonPressed)
                {
                    ShipRotateSystem.RotateShip();
                }

                else if (Input.LeftMouseButtonDown && !isDragging && _gameManager.inDragShip == null)
                {
                    ShipDragAndDropSystem.OnDragStart();
                }
                else if (Input.LeftMouseButtonReleased && isDragging)
                {
                    ShipDragAndDropSystem.OnDragEnd(mousePosition);
                }
            }
        }
      
        public void StartShipInBoard()
        {
            if (!isReady)
            {
                var gridList = _gameManager.GridsList;
                int randomIndex = Nez.Random.NextInt(gridList.Count);
                GridLinkedToShip = gridList[randomIndex];
                this.LocalPosition = GridLinkedToShip.LocalPosition;

                var listRotations = new List<int>() { 0, 90, 180, 360};
                var randRotation = Nez.Random.NextInt(listRotations.Count);
                this.LocalRotationDegrees = listRotations[randRotation];

                // Comprobaciones
                if(ShipCollisionSystem.CollisionWithBoundsArray(GridLinkedToShip, this.LocalRotation))
                {
                    StartShipInBoard();
                    return;
                }

                var listPositions = ShipSetArrayPositions.PositionValuesList(this.LocalRotation, GridLinkedToShip);
                inUsePositions.Clear();
                inUsePositions = listPositions;


                foreach (var i in inUsePositions)
                {
                    Console.Write(i.ToString() + " ");
                }
                Console.WriteLine();
                
                if (ShipCollisionSystem.collisionDetection(inUsePositions))
                {
                    StartShipInBoard();
                    return;
                }

                ShipSetArrayPositions.matrixInShipPosition(2);

                isReady = true;
            }           
        }
      
    }

    
}
