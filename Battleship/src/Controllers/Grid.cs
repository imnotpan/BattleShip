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

        public bool _onClick = false;
        private SpriteRenderer SpriteRenderer { get; set; }
        private GameManager _gameManager;


        public Vector2 _relativePosition;
        
        /* Logic Properties */
        private bool isOnTempArray = false;
        public bool isInUse = false;
        private Color hoverColor = Color.BlueViolet; 
        private Color normalColor = Color.White;
        private Color selectedColor = Color.Red;

        // Bullet Colors
        private Color missedBulletColor = Color.DarkGoldenrod;
        private Color successBulletColor = Color.Green;

        public bool isDestroy = false;


        public Grid(Texture2D cellTexture, Vector2 position, Vector2 relativePosition, GameManager _gameManager)
        {
            this._gameManager = _gameManager;
            SpriteRenderer = new SpriteRenderer(cellTexture);
            SpriteRenderer.RenderLayer = 1;
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

            if (Collider.Bounds.Contains(mousePosition))
            {
                _gameManager._MouseInGrid = this;
                SpriteRenderer.Color = hoverColor;

                if (_gameManager.gameState == 2) ClickeableGridSystem(mousePosition);
            }
            else
            {
                if (!_onClick)
                {

                    SpriteRenderer.Color = normalColor;
                }
                else if(_onClick)
                {
                    SpriteRenderer.Color = selectedColor;
                }
            }
        }

        public void DetectState()
        {
            _onClick = false;
            isDestroy = true;
            if (_gameManager.enemyMatrix[(int)_relativePosition.X, (int)_relativePosition.Y] == 2)
            {
                SpriteRenderer.Color = successBulletColor;
                _gameManager.enemyMatrix[(int)_relativePosition.X, (int)_relativePosition.Y] = 1;

                return;
            }
            if (_gameManager.enemyMatrix[(int)_relativePosition.X, (int)_relativePosition.Y] == 0)
            {
                SpriteRenderer.Color = missedBulletColor;
                return;
            }
        }

        public void ClickeableGridSystem(Vector2 mousePosition)
        {
            if (Input.LeftMouseButtonPressed && !isDestroy)
            {
                if (_gameManager.tempArray.Count > _gameManager.bulletCount) return;
                isOnTempArray = (isOnTempArray == false) ? true : false;
                _onClick = (_onClick == false) ? true : false;
                _gameManager.selectedGrids.Add(this);

                this.TweenLocalScaleTo(new Vector2(1.25f, 1.25f), 0.05f)
                .SetEaseType(EaseType.ExpoOut)
                .SetCompletionHandler((x) =>
                {
                    this.TweenLocalScaleTo(new Vector2(1f, 1f), 0.05f)
                    .SetEaseType(EaseType.ExpoIn)
                    .SetCompletionHandler((x) =>
                    {
                            
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
        }
    }
}
