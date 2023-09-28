using Battleship.src.Controllers.Grids;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Battleship.src.Controllers.Ships
{
    public class ShipBase : Entity
    {
        /* Components */
        public SpriteRenderer SpriteRenderer { get; set; }
        public Collider Collider { get; set; }
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

        public int _shipRotation;
        public bool canMove = true;

        public GameControllers GameControllers;
        public GameManager GameManager;


        public ShipBase(Texture2D shipTexture, GameControllers GameControllers)
        {

            this.GameControllers = GameControllers;
            this.GameManager = GameControllers.GameManager;
            SetTag(1);
            this._texture = shipTexture;
            //this.Position = position;

            var textureHeight = _texture.Height;
            var yOffset = (textureHeight / 16) % 2 == 0 ? -8 : 0;

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
            this.LocalRotationDegrees = _shipRotation;

            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);

            if (canMove) { shipControllers(mousePosition); }
            if (GameControllers.inDragShip == this) { this.Position = mousePosition; }

        }


        public void shipControllers(Vector2 mousePosition)
        {
            // Game state Ship controller
            if (Collider.Bounds.Contains(mousePosition))
            {
                if (Input.LeftMouseButtonDown && GameControllers.inDragShip == null)
                {
                    ShipDragAndDropSystem.OnDragStart();
                    startDragPosition = mousePosition;
                    GameControllers.inDragShip = this;
                }
            }

            if (Input.LeftMouseButtonReleased && GameControllers.inDragShip == this)
            {
                ShipDragAndDropSystem.OnDragEnd(mousePosition, GameControllers.PlayerBoard);
                GameControllers.inDragShip = null;
            }
        }
    }
}
        
      

    

