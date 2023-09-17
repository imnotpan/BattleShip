using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Battleship.src.Controllers.Ships
{
    public class shipBattle : Entity
    {
        /* Components */
        private SpriteRenderer SpriteRenderer { get; set; }
        private Collider Collider { get; set; }

        public Texture2D _texture;

        /* Dragging controller */
        public bool isDragging;

        /* Rotation Properties */
        private float nextRotation;
        private bool isRotating;

        private GameManager _gameManager;

        public shipBattle(Texture2D shipTexture, Vector2 position, GameManager _gameManager) {
            this._gameManager = _gameManager;
            SpriteRenderer = new SpriteRenderer(shipTexture);
            SpriteRenderer.Origin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2 - 16);

            Collider = new BoxCollider();

            this._texture = shipTexture;
            this.Position = position;

            AddComponent(SpriteRenderer);
            AddComponent(Collider);
        }

        public void RotateShip()
        {
            nextRotation = MathHelper.ToDegrees(this.Rotation) + 90;
            if (!isRotating)
            {
                isRotating = true;
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

        public override void Update()
        {
            base.Update();
            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
            mouseController(mousePosition);
            if (isDragging) { this.Position = mousePosition; }

        }
        public void mouseController(Vector2 mousePosition)
        {

            if (Collider.Bounds.Contains(mousePosition))
            {
                if (Input.RightMouseButtonPressed)
                {
                    RotateShip();
                }
                else if (Input.LeftMouseButtonDown && !isDragging)
                {
                    OnDragStart();
                }
                else if (Input.LeftMouseButtonReleased && isDragging)
                {
                    OnDragEnd(mousePosition);
                }
            }

        }

        public void OnDragStart() { isDragging = true; }

        public void OnDragEnd(Vector2 mousePosition)
        {
            List<CollisionResult> objetosColisionando = new List<CollisionResult>();
            isDragging = false;

            if (Collider.CollidesWithAny( out CollisionResult result)){
                this.TweenLocalPositionTo(_gameManager._MouseInGrid.Position, 0.05f)
                  .SetEaseType(EaseType.SineOut)
                  .SetCompletionHandler((x) =>
                  {
                     Console.WriteLine(result.Collider);
                  })
                 .Start();
            }

        }

    }
}
