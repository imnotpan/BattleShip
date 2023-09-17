using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using Nez.Tweens;
using System.Xml.Linq;

namespace Battleship.src.Controllers
{

    public class Grid : Entity
    {

        public Texture2D _texture;
        private Collider Collider { get; set; }

        public bool _onClick = true;
        private SpriteRenderer SpriteRenderer { get; set; }

        private Vector2 _relativePosition;
        private bool isOnTempArray = false;
        private GameManager _gameManager;

        public Grid(Texture2D cellTexture, Vector2 position, Vector2 relativePosition, GameManager _gameManager)
        {
            this._gameManager = _gameManager;
            SpriteRenderer = new SpriteRenderer(cellTexture);
            SpriteRenderer.Origin = new Vector2(cellTexture.Width / 2, cellTexture.Height / 2);
            Collider = new BoxCollider();

            this._texture = cellTexture;
            this.Position = position;
            this._relativePosition = relativePosition;

            AddComponent(Collider);
            AddComponent(SpriteRenderer);
        }


        public override void Update()
        {
            base.Update();
            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
            mouseController(mousePosition);
        }

        public void mouseController(Vector2 mousePosition)
        {
            if (Collider.Bounds.Contains(mousePosition))
            {
                _gameManager._MouseInGrid = this;
                if (Input.LeftMouseButtonPressed)
                {
                    SpriteRenderer.Color = (SpriteRenderer.Color == Color.White) ? Color.Red : Color.White;
                    isOnTempArray = (isOnTempArray == false) ? true : false;

                    _onClick = false;
                    this.TweenLocalScaleTo(new Vector2(1.25f, 1.25f), 0.05f)
                    .SetEaseType(EaseType.ExpoOut)
                    .SetCompletionHandler((x) =>
                    {
                        this.TweenLocalScaleTo(new Vector2(1f, 1f), 0.05f)
                        .SetEaseType(EaseType.ExpoIn)
                        .SetCompletionHandler((x) =>
                        {
                            
                            _onClick = true;
                            if (isOnTempArray)
                            {
                                isOnTempArray = true;
                                _gameManager.tempArray.Add(_relativePosition);
                            }
                            else
                            {
                                isOnTempArray = false;
                                _gameManager.tempArray.Remove(_relativePosition);
                            }
                            _gameManager.printTempArray();
                            
                        })
                        .Start();
                    })
                    .Start();
                }
                else if (Input.LeftMouseButtonDown)
                {

                }
                else if (Input.LeftMouseButtonReleased)
                {

                }
           
            }

        }
    }
}
