using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using Nez.Tweens;
using System.Xml.Linq;
using System.Reflection.Metadata;

namespace Battleship.src.Controllers.Grids
{

    public class Grid : Entity
    {

        public Texture2D _texture;
        private Collider Collider { get; set; }

        public bool _onClick = false;
        private SpriteRenderer SpriteRenderer { get; set; }
        private GameManager GameManager;


        public Vector2 _relativePosition;

        /* Logic Properties */
        private bool isOnTempArray = false;
        public bool isInUse = false;
        private Color overColor = Color.Cyan;
        public Color currentColor = Color.White;

        public bool isDestroy = false;
        Effect doodleEffect;

        public Grid(Texture2D cellTexture, Vector2 position, Vector2 relativePosition, GameManager _gameManager)
        {
            GameManager = _gameManager;
            SpriteRenderer = new SpriteRenderer(cellTexture);
            SpriteRenderer.RenderLayer = 1;
            SpriteRenderer.Origin = new Vector2(cellTexture.Width / 2, cellTexture.Height / 2);
            Collider = new BoxCollider();

            _texture = cellTexture;
            Position = position;
            _relativePosition = relativePosition;

            AddComponent(Collider);
            AddComponent(SpriteRenderer);
            _gameManager.GridsList.Add(this);
            /*
            doodleEffect = GameManager.Scene.Content.Load<Effect>("Shaders/ScribbleEffect");
            doodleEffect.Parameters["doodleMaxOffset"].SetValue(new Vector2(0.005f, 0.005f));
            doodleEffect.Parameters["doodleNoiseScale"].SetValue(new Vector2(6f, 8f));
            doodleEffect.Parameters["doodleFrameCount"].SetValue(6);
            doodleEffect.Parameters["doodleFrameTime"].SetValue(0.2f);
            Material renderMaterial = new Material(doodleEffect);
            SpriteRenderer.Material = renderMaterial;
            */


        }


        public override void Update()
        {
            base.Update();
           // doodleEffect.Parameters["time"].SetValue(Time.TotalTime);

            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
            if(GameManager.GameState == "PREPARATION" || GameManager.GameState == "PLAYERTURN" )
            if (Collider.Bounds.Contains(mousePosition))
            {
                GameManager._MouseInGrid = this;
                if (SpriteRenderer.Color != overColor)
                {
                    SpriteRenderer.Color = overColor;
                }

                if (GameManager.GameState == "PLAYERTURN") ClickeableGridSystem(mousePosition);
            }
            else
            {
                if (SpriteRenderer.Color != currentColor)
                {
                    SpriteRenderer.Color = currentColor;
                }
            }
        }


        public void ClickeableGridSystem(Vector2 mousePosition)
        {
            if (!Input.LeftMouseButtonPressed)
            {
                return;
            }
            if (isDestroy)
            {
                return;
            }

            isOnTempArray = isOnTempArray == false ? true : false;
            _onClick = _onClick == false ? true : false;

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
                        if (GameManager.playerSelectedGrids.Count > GameManager.bulletCount) return;

                        isOnTempArray = true;
                        currentColor = Color.Yellow;

                        GameManager.playerSelectedGrids.Add(this);
                    }
                    else
                    {
                        isOnTempArray = false;
                        currentColor = Color.White;
                        GameManager.playerSelectedGrids.Remove(this);

                    }

                })
                .Start();
            })
            .Start();
        }
    }
}
